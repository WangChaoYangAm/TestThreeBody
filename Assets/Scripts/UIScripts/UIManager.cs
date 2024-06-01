using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MySingle<UIManager>, Imsg
{
    public Dictionary<string, MyBaseWindow> _allWindowDic = new Dictionary<string, MyBaseWindow>();
    [SerializeField]
    private Transform _rootNormal, _rootFixed, _rootPop, _rootTop;

    public void Bind()
    {
        MyFacade.Register(MyCommand.OPEN_VIEW, this);
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

    public void RecieveMsg(MyResponseData data)
    {
        switch (data._actionName)
        {
            case MyCommand.OPEN_VIEW:
            case MyCommand.HIDE_VIEW:
                LoadWindow((EWindowUI)data._data).ShowWindow(data._actionName == MyCommand.OPEN_VIEW);
                break;
        }
    }

    public void RemoveBind()
    {
        throw new System.NotImplementedException();
    }
}
