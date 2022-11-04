using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private Animator _anim;
    private AudioManager theAudio;
    private PlayerController thePlayer;
    public string appear_sound;
    public string key_sound;

    public GameObject bg_Panel; // 배경 패널
    public GameObject[] panels; // 활성화 시킬 패널 

    public GameObject[] menus_Tab; // 활성화시킬 인벤토리, 통화, 메시지 탭 오브젝트
    
    private int selectedTab; // 선택된 탭
        
    private bool menuActivated; // 메뉴 활성화 여부
    private bool otherActivated; // 인벤토리, 통화, 메시지 탭 활성화 여부
    public bool OtherActivated { set => otherActivated = value; }

    private void Start()
    {
        _anim = GetComponent<Animator>();
        theAudio = AudioManager.instance;
        thePlayer = PlayerController.instance;
    }

    private void Update()
    {
        if (otherActivated) return;
        if (Input.GetKeyDown(KeyCode.X))
        {
            menuActivated = !menuActivated;

            if (menuActivated)
            {
                theAudio.PlaySFX(appear_sound);
                bg_Panel.SetActive(true);
                thePlayer.IsPause = true;
                _anim.SetBool("Appear", true);
                selectedTab = 0;
                panels[selectedTab].SetActive(true);
            }

            else
            {
                theAudio.PlaySFX(appear_sound);
                bg_Panel.SetActive(false);
                thePlayer.IsPause = false;
                _anim.SetBool("Appear", false);
                panels[selectedTab].SetActive(false);
            }
        }

        // 메뉴가 활성화 되어있을때 아래 코드 실행
        if (!menuActivated) return; 
        
        // 메뉴에서 화살표 키 위, 아래를 누르면 각각 위, 아래에 있는 텍스트로 포커싱 이동
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            theAudio.PlaySFX(key_sound);
            panels[selectedTab].SetActive(false);
            if (selectedTab == 3)
                selectedTab = 0;
            else
                selectedTab++;
            panels[selectedTab].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            theAudio.PlaySFX(key_sound);
            panels[selectedTab].SetActive(false);
            if (selectedTab == 0)
                selectedTab = 3;
            else 
                selectedTab--;
            panels[selectedTab].SetActive(true);
        }

        // Z키로 결정
        if (Input.GetKeyDown(KeyCode.Z))
        {
            theAudio.PlaySFX(key_sound);
            otherActivated = true;
            menus_Tab[selectedTab].SetActive(true);
        }
    }
}
