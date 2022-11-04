using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public string key_sound;
    private AudioManager theAudio;
    private InventoryManager theInven;
    public Menu theMenu;

    private List<Item> inventoryItemList;
    private InventorySlot[] slots; // 인벤토리 슬롯들

    public Text Description_Text; // 부연 설명
    
    public Transform tf; // slot 부모객체

    private int selectedItem; // 선택된 아이템 (인벤토리 슬롯의 인덱스)
    private void OnEnable()
    {
        theAudio = AudioManager.instance;
        theInven = InventoryManager.instance;
        inventoryItemList = theInven.InventoryItemList;
        slots = tf.GetComponentsInChildren<InventorySlot>();
        ShowItems();
    }

    void Update()
    {
        // 돌아가기
        if (Input.GetKeyDown(KeyCode.X))
        {
            theAudio.PlaySFX(key_sound);
            theMenu.OtherActivated = false;
            gameObject.SetActive(false);
        }
        
        // 가지고 있는 아이템이 없으면 리턴
        if (inventoryItemList.Count == 0) return; 
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectedItem > 1)
                selectedItem -= 2;
            else
                selectedItem = inventoryItemList.Count - 1 - selectedItem;
            theAudio.PlaySFX(key_sound);
            SelectedItem();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectedItem < inventoryItemList.Count - 2)
                selectedItem += 2;
            else
                selectedItem %= 2;
            theAudio.PlaySFX(key_sound);
            SelectedItem();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (selectedItem > 0)
                selectedItem--;
            else
                selectedItem = inventoryItemList.Count - 1;
            theAudio.PlaySFX(key_sound);
            SelectedItem();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (selectedItem < inventoryItemList.Count - 1)
                selectedItem++;
            else
                selectedItem = 0;
            theAudio.PlaySFX(key_sound);
            SelectedItem();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            theAudio.PlaySFX(key_sound);
        }
        
    }
    
    private void RemoveSlot()
    {
        // 슬롯에 있는 내용들 제거
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false);
        }
    }

    private void ShowItems()
    {
        RemoveSlot();
        selectedItem = 0;

        for (int i = 0; i < inventoryItemList.Count; i++) // InventoryList 내역에 있는 것들을 slot에 추가
        {
             slots[i].gameObject.SetActive(true);
             slots[i].AddItem(inventoryItemList[i]);
        }
        SelectedItem();
    }

    private void SelectedItem()
    {
        if (inventoryItemList.Count > 0)
        {
            slots[selectedItem].FocusPanel.SetActive(true);
            Description_Text.text = inventoryItemList[selectedItem].itemDescription;
        }
    }

}
