using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPackageManager : MySingle<MyPackageManager>
{
    //private UIPackageWindow _packageWindow;
    public Dictionary<string, int> _dicItem = new Dictionary<string, int>();
    // Start is called before the first frame update
    void Start()
    {
        //_packageWindow = (UIPackageWindow)UIManager.Instance.LoadWindow(EWindowUI.UIPackage);
        MyGameManager.Instance.Regist(KeyCode.B, OpenPackage);
    }

    public void AddItem(bool isAdd, string itemId, int count)
    {
        if (_dicItem.ContainsKey(itemId))
        {
            if (isAdd)
            {
                _dicItem[itemId] += count;
            }
            else
            {
                _dicItem[itemId] -= count;
            }

        }
        else
        {
            if (isAdd)
            {
                _dicItem.Add(itemId, count);
            }
            else
            {
                Debug.Log("道具数量不足" + itemId);
            }
        }

    }
    void OpenPackage()
    {
        MyFacade.SendMsg(MyCommand.OPEN_VIEW, new MyResponseData { _data = EWindowUI.UIPackage });
    }
}
