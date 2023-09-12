using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    武器,
    唯一性任务道具
}
public class MyItemBase
{
    public string _itemId;
    public string _itemName;
    public string _itemDes;
    public ItemType _itemType;
    public int _itemCount;
}
