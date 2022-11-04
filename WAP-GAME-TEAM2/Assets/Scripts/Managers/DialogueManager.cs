using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    
    
    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private EventManager theEvent;
    
    [SerializeField] private Text text;

    private List<string> listSentences;

    private int count; // 대화 진행 상황
    public bool talking;
    
    private bool keyActivated = false;
    public bool nextDialogue;


    void Start()
    {
        theEvent = EventManager.instance;
        count = 0;
        text.text = "";
        listSentences = new List<string>();
        nextDialogue = false;
    }

    public void ShowText(string[] _sentences)
    {
        talking = true;
        PlayerController.instance.IsPause = true;

        for (int i = 0; i < _sentences.Length; i++)
        {
            listSentences.Add(_sentences[i]);
        }

        StartCoroutine(StartTextCoroutine());
    }
    
    public void ShowText(string _sentences)
    {
        talking = true;
        PlayerController.instance.IsPause = true;
        
        listSentences.Add(_sentences);
        StartCoroutine(StartTextCoroutine());
    }
    
    

    IEnumerator StartTextCoroutine()
    {
        nextDialogue = false;
        keyActivated = true;
        for (int i = 0; i < listSentences[count].Length; i++)
        {
            text.text += listSentences[count][i]; // 한글자 씩 출력.
            yield return new WaitForSeconds(0.03f);
        }
    }
    public void ExitDialogue()
    {
        text.text = "";
        count = 0;
        listSentences.Clear();
        talking = false;
        if (!theEvent.isEventIng)
            PlayerController.instance.IsPause = false;
        nextDialogue = true;
    }


    void Update()
    {
        if (talking && keyActivated)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                keyActivated = false;
                count++;
                text.text = "";
                
                if (count == listSentences.Count)
                {
                    StopAllCoroutines();
                    ExitDialogue();
                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine(StartTextCoroutine());
                }
            }
        }
    }

}
