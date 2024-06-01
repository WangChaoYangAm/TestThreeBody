using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MySingle<UIManager>, Imsg
{
    public Dictionary<string, MyBaseWindow> _allWindowDic = new Dictionary<string, MyBaseWindow>();
    [SerializeField]
    private Transform _rootNormal, _rootFixed, _rootPop, _rootTop;
    [SerializeField]
    private string _questStartId;
    protected override void Awake()
    {
        base.Awake();
        Bind();
    }
    public void Bind()
    {
        MyFacade.Register(MyCommand.INIT_GAME_UI, this);
        MyFacade.Register(MyCommand.OPEN_VIEW, this);
        MyFacade.Register(MyCommand.HIDE_VIEW, this);
    }
    void InitWindows()
    {

        ////预加载对话界面,暂定无对话，初次加载直接隐藏对话UI
        //UIManager.Instance.LoadWindow(EWindowUI.UIDialogue).ShowWindow(false);
        MyFacade.SendMsg(MyCommand.OPEN_VIEW, new OpenViewConfig() { _eWindowUI = EWindowUI.UIQuests });
        MyFacade.SendMsg(MyCommand.INIT_DATA_QUEST, _questStartId);

        //初始化所有不需要传入数据的UI的数据初始化
        MyFacade.SendMsg(MyCommand.OPEN_VIEW, new OpenViewConfig() { _eWindowUI = EWindowUI.UIPlayerPackage });
        MyFacade.SendMsg(MyCommand.INIT_GAME_UI_DATA, null);

        //隐藏需要隐藏的UI
        MyFacade.SendMsg(MyCommand.HIDE_VIEW, new OpenViewConfig() { _eWindowUI = EWindowUI.UIPlayerPackage });

    }

    public MyBaseWindow LoadWindow(EWindowUI eWindowUI)
    {
        string windowNmae = eWindowUI.ToString();
        if (_allWindowDic.ContainsKey(windowNmae))
        {
            return _allWindowDic[windowNmae];
        }
        string path = "UIPrefabs/" + windowNmae;
        MyBaseWindow windowBase = MyLoad.LoadGamepbject(path).GetComponent<MyBaseWindow>();
        if (windowBase == null) Debug.LogError("未加载到的窗口为" + windowNmae);//MyBaseWindow子类能否获取不一定
        else
        {
            switch (windowBase._windowType)
            {
                case EWindowType.Normal:
                    windowBase.transform.SetParent(_rootNormal);
                    break;
                case EWindowType.Fixed:
                    windowBase.transform.SetParent(_rootFixed);
                    break;
                case EWindowType.Pop:
                    windowBase.transform.SetParent(_rootPop);
                    break;
                case EWindowType.Top:
                    windowBase.transform.SetParent(_rootTop);
                    break;
            }
            var rect = windowBase.GetComponent<RectTransform>();
            rect.anchoredPosition3D = Vector3.zero;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            rect.localScale = Vector3.one;
            _allWindowDic.Add(windowNmae, windowBase);
        }
        return windowBase;
    }

    public void RecieveMsg(string command, object data)
    {
        switch (command)
        {
            case MyCommand.INIT_GAME_UI:
                InitWindows();
                break;
            case MyCommand.OPEN_VIEW:
                var configOpenView = (OpenViewConfig)data;
                LoadWindow(configOpenView._eWindowUI).ShowWindow(true);
                break;
            case MyCommand.HIDE_VIEW:
                //LoadWindow((EWindowUI)data._data).ShowWindow(data._actionName == MyCommand.OPEN_VIEW);
                var configHideView = (OpenViewConfig)data;
                LoadWindow(configHideView._eWindowUI).ShowWindow(false);
                break;
        }
    }

    public void RemoveBind()
    {
        //throw new System.NotImplementedException();
    }
}
