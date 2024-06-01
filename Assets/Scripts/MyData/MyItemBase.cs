using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    武器,
    唯一性任务道具
}
/// <summary>
/// 基类负责记录不会更改的数据
/// </summary>
[System.Serializable]
public class MyItemBase
{
    public string _itemId;
    public string _itemName;
    public string _itemDes;
    public ItemType _itemType;
    public int _itemCount_max;
    private string _iconPath;

}
/// <summary>
/// 继承自基类 包含部分可能会修改的数据
/// </summary>
public class MyItem: MyItemBase
{
    public int _itemCount_cur;

}
