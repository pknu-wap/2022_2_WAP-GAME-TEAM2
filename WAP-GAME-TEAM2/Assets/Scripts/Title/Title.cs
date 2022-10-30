using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private AudioManager theAudio;
    private FadeManager theFade; 
    void Start()
    {
        theFade = FadeManager.instance;
        theAudio = AudioManager.instance;
        theFade.FadeIn();
    }

    public void NewGame()
    {
        theAudio.PlaySFX("Cursor");
        theFade.FadeOut();
        SceneManager.LoadScene("4Floor");
    }

    

    public void OnButton()
    {
        AudioManager.instance.PlaySFX("Cursor");
    }
}
