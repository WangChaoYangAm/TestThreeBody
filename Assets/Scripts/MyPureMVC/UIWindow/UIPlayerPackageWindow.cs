using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerPackageWindow : MyBaseWindow, Imsg
{
    [SerializeField]
    private GameObject _cellItem;
    [SerializeField]
    private RectTransform _content;
    public Dictionary<string, MyItemBase> _dicMyPackageItem = new Dictionary<string, MyItemBase>();
    private void Awake()
    {
        Bind();
    }
    public void Bind()
    {
        MyFacade.Register(MyCommand.INIT_GAME_UI_DATA, this);
        MyFacade.Register(MyCommand.ADD_ITEM, this);
    }

    public void RecieveMsg(string command, object data)
    {
        switch (command)
        {
            case MyCommand.INIT_GAME_UI_DATA:
                MyLoadDataManager.Instance.LoadItemBaseList();
                InitData();
                break;
            case MyCommand.ADD_ITEM:
                OnAddItem((MyItemBase)data, 1);
                break;
        }
    }

    public void RemoveBind()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
    }
    /// <summary>
    /// 初始化数据
    /// </summary>
    void InitData()
    {
        //初始化背包已有的道具
        //TODO 由于还没有做存档数据的存储和解析 所以暂时直接生成

        //更新UI
        //for (int i = 0; i < 10; i++)
        //{
        //    Transform cell = Instantiate(_cellItem, _content).transform;
        //    cell.localScale = Vector3.one;
        //    cell.localPosition = Vector3.zero;
        //}
    }
    void OnAddItem(MyItemBase myItem, int count)
    {
        Transform cell = Instantiate(_cellItem, _content).transform;
        cell.localScale = Vector3.one;
        cell.localPosition = Vector3.zero;
        //cell.GetComponentInChildren<Text>().text = myItem._itemName;
    }
    void OnRemoveItem(MyItemBase myItem, int count)
    {

    }
}
