using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 负责挂载在对象上，以便交互
/// </summary>
public class Trigger_Item : MonoBehaviour
{
    public string _itemId;
    public string _itemName;
    private MyItemBase _itemBase;
    public MyItemBase GetItemData { get { return _itemBase; } }
    private void Start()
    {
        if ( !string.IsNullOrEmpty(_itemId))
        {
            UpdateItem(MyLoadDataManager.Instance.LoadItemBase(_itemId));
        }
    }
    public void UpdateItem(MyItemBase itemBase)
    {
        _itemId = itemBase._itemId;
        _itemName = itemBase._itemName;
    }
}
