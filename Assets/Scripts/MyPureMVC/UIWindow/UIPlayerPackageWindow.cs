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
    /// ��ʼ������
    /// </summary>
    void InitData()
    {
        //��ʼ���������еĵ���
        //TODO ���ڻ�û�����浵���ݵĴ洢�ͽ��� ������ʱֱ������

    }
    void OnAddItem(MyItemBase myItem)
    {

    }
    void OnRemoveItem(MyItemBase myItem)
    {

    }
}