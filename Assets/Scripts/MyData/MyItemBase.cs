using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    ����,
    Ψһ���������
}
public class MyItemBase
{
    public string _itemId;
    public string _itemName;
    public string _itemDes;
    public ItemType _itemType;
    public int _itemCount;
}