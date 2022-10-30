using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 커스텀 클래스는 Inspector창에 보이지 않는다.
[System.Serializable]
public class Sound
{
    public string name;         // 사운드 이름

    public AudioClip clip;      // 사운드 파일
    private AudioSource source;
    
    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float pitch;
    
    public bool loop;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
        source.volume = volume;
        source.pitch = pitch;
    }

    public void SetVolume()
    {
        source.volume = volume;
    }
    
    public void SetPitch()
    {
        source.pitch = pitch;
    }

    public void Play()
    {
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void SetLoop(bool state)
    {
        source.loop = state;
    }

}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    public Sound[] sounds;

    private int nowPlayBGM;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject soundObject = new GameObject("사운드 파일 이름 : " + i + " = " + sounds[i].name);
            sounds[i].SetSource(soundObject.AddComponent<AudioSource>());
            // Hierachy에 soundObject가 들어가도록
            soundObject.transform.SetParent(this.transform);
        }
        //PlayBGM("Title");
    }

    public void PlayBGM(string _name)
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                nowPlayBGM = i;
                sounds[i].Play();
                return;
            }
        }
    }
    
    public void PlaySFX(string _name)
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].Play();
                return;
            }
        }
    }

    public void SetVolume(string _name, float volume)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].volume = volume;
                sounds[i].SetVolume();
                return;
            }
        }
    }

    public void SetPitch(string _name, float _pitch)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].pitch = _pitch;
                sounds[i].SetPitch();
                return;
            }
        }
    }

    public void Stop()
    {
        sounds[nowPlayBGM].Stop();
    }

    public void SetLoop(string _name, bool state)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].SetLoop(state);
                return;
            }
        }
    }
    
}
