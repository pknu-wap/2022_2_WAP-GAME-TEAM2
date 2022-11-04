using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    // 아이템 타입을 단 하나만 있는 것과 여러개 가질 수 있는 것으로 나눔
    public enum ItemType
    {
        Single,
        Multiple
    }

    public string fileName;
    public string itemName;
    public string itemDescription;
    public int itemCount;
    public Sprite itemIcon;
    public ItemType itemType;
    
    public Item(string _fileName, string _itemName, string _itemDescription, ItemType _itemType, int _itemCount = 1)
    {
        fileName = _fileName;
        itemName = _itemName;
        itemDescription = _itemDescription;
        itemType = _itemType;
        itemCount = _itemCount;
        itemIcon = Resources.Load("Icon/" + _fileName, typeof(Sprite)) as Sprite;
    }

}
