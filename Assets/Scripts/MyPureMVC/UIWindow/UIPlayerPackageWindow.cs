using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerPackageWindow : MyBaseWindow, Imsg
{
    public Dictionary<string, MyItemBase> _dicMyPackageItem = new Dictionary<string, MyItemBase>();
    public void Bind()
    {
        MyFacade.Register(this._windowName, this);
    }

    public void RecieveMsg(MyResponseData data)
    {
        switch (data._actionName)
        {
            case "":
                break;
        }
    }

    public void RemoveBind()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        InitData();
    }
    /// <summary>
    /// 初始化数据
    /// </summary>
    void InitData()
    {
        //初始化背包已有的道具
        //TODO 由于还没有做存档数据的存储和解析 所以暂时直接生成

    }
    void OnAddItem(MyItemBase myItem)
    {

    }
    void OnRemoveItem(MyItemBase myItem)
    {

    }
}
