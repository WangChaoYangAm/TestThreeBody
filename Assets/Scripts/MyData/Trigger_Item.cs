using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������ڶ����ϣ��Ա㽻��
/// </summary>
public class Trigger_Item : MonoBehaviour
{
    public string _itemId;
    public string _itemName;
    private MyItemBase _itemBase;

    private void Start()
    {
        if (_itemBase == null && !string.IsNullOrEmpty(_itemId))
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