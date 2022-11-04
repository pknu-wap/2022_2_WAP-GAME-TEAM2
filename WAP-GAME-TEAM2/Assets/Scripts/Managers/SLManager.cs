using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.IO;

public class SLManager : MonoBehaviour
{
    private class PlayerData
    {
        public Vector2 player_Pos;
        public int now_PlayingBGM;
        public string cur_SceneName;
        public List<Tuple<string ,int>> inventoryItemList; // 가지고 있는 아이템의 이름과 갯수를 저장.
        public bool[] event_Switches;

        public PlayerData(Vector2 _playerPos, int _nowPlayingBGM, string _curSceneName, List<Tuple<string ,int>> _inventoryItemList,
            bool[] _eventSwitches)
        {
            player_Pos = _playerPos;
            now_PlayingBGM = _nowPlayingBGM;
            cur_SceneName = _curSceneName;
            inventoryItemList = _inventoryItemList;
            event_Switches = _eventSwitches;
        }
    }
    
    public static SLManager instance;

    #region  SingleTon

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    #endregion

    public void Save()
    {
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };
        
        Vector2 playerPos = PlayerController.instance.transform.position;
        int nowPlayingBGM = AudioManager.instance.nowPlayBGM;
        string curSceneName = SceneManager.GetActiveScene().name;
        bool[] eventSwitches = EventManager.instance.switches;
        
        List<Item> inventoryItemList = InventoryManager.instance.InventoryItemList;
        List<Tuple<string, int>> invenItemTupleList = new List<Tuple<string, int>>();
        // 가지고 있는 아이템을 아이템 제목과 갯수 값만 invenItemTupleList에 저장.
        for (int i = 0; i < inventoryItemList.Count; i++)
        {
            invenItemTupleList.Add(new Tuple<string, int>(inventoryItemList[i].itemName, inventoryItemList[i].itemCount));
        }
        
        
        PlayerData data = new PlayerData(playerPos, nowPlayingBGM, curSceneName, invenItemTupleList, eventSwitches);
        string jdata = JsonConvert.SerializeObject(data, settings);
        File.WriteAllText(Application.dataPath + "/Save.json", jdata);
    }

    public void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/Save.json");
        PlayerData data = JsonConvert.DeserializeObject<PlayerData>(jdata);
        PlayerController.instance.transform.position = data.player_Pos;
        AudioManager.instance.nowPlayBGM = data.now_PlayingBGM;
        
        List<Item> invenItemList = InventoryManager.instance.InventoryItemList; // 현재 플레이어가 소유한 아이템 리스트
        Dictionary<string, Item> ItemList = InventoryManager.instance.ItemDictionary; // 아이템 리스트
        List<Tuple<string, int>> dataItemList = data.inventoryItemList; // json에서 읽어온 플레이어가 소유한 아이템 리스트
        for (int i = 0; i < dataItemList.Count; i++)
        {
            // 플레이어가 가지고 있던 아이템의 이름(문자열)으로 아이템 리스트에 있는 아이템을 찾아서 현재 아이템 리스트에 삽입.
            Item item = ItemList[dataItemList[i].Item1]; 
            invenItemList.Add(item);
        }

        for (int i = 0; i < data.event_Switches.Length; i++)
        {
            EventManager.instance.switches[i] = data.event_Switches[i]; 
        }
    }
}
