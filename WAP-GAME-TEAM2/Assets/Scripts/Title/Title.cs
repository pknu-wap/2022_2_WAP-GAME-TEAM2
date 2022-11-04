using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private AudioManager theAudio;
    private FadeManager theFade;

    [SerializeField] private Animator newGameAnim;
    void Start()
    {
        theFade = FadeManager.instance;
        theAudio = AudioManager.instance;
        theFade.FadeIn();
    }

    public void NewGame()
    {
        StartCoroutine(NewGameCoroutine());
    }

    

    public void OnButton()
    {
        AudioManager.instance.PlaySFX("Cursor");
    }

    IEnumerator NewGameCoroutine()
    {
        newGameAnim.SetTrigger("Start");
        theAudio.PlaySFX("Cursor");
        theFade.FadeOut(0.05f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("3-1");
    }
}
