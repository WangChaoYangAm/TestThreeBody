using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.UIElements;

public class UIDialogueWindow : MyBaseWindow, Imsg
{
    [SerializeField]
    private RectTransform _rootDialogue;
    [SerializeField]
    private GameObject _preCellDialogue;
    [SerializeField]
    private Text _textField;
    private MyDialogueBase _dialogueBase;
    public Action<EDialogueFunc, string> OnClickAction;
    public Action OnEndAction;
    private Dictionary<KeyCode, Action> _dicKeycodeAction = new Dictionary<KeyCode, Action>();

    private void Awake()
    {
        Bind();
    }

    public void Bind()
    {
        //MyFacade.Register(MyCommand.OPEN_VIEW,this);
        //MyFacade.Register(MyCommand.HIDE_VIEW,this);
    }

    public void RemoveBind()
    {
    }
    public void RecieveMsg(string command, object data)
    {

    }
    public void Init(Action EndAction, Action<EDialogueFunc, string> ClickAction)
    {
        ObjectPool.Instance.SetPrefab(_preCellDialogue);
        OnEndAction = EndAction;
        OnClickAction = ClickAction;
        _dicKeycodeAction.Add(KeyCode.Alpha1, null);
        _dicKeycodeAction.Add(KeyCode.Alpha2, null);
        _dicKeycodeAction.Add(KeyCode.Alpha3, null);
        _dicKeycodeAction.Add(KeyCode.Alpha4, null);
        MyGameManager.Instance.Regist(KeyCode.Alpha1, () => KeycodeAction(KeyCode.Alpha1));
        MyGameManager.Instance.Regist(KeyCode.Alpha2, () => KeycodeAction(KeyCode.Alpha2));
        MyGameManager.Instance.Regist(KeyCode.Alpha3, () => KeycodeAction(KeyCode.Alpha3));
        MyGameManager.Instance.Regist(KeyCode.Alpha4, () => KeycodeAction(KeyCode.Alpha4));
    }

    public void UpdateDialogue(MyDialogueBase dialogueBase)
    {
        if (dialogueBase == null) return;
        if (!this.gameObject.activeSelf)//确保UI界面是打开的
        {
            ShowWindow(true);
        }
        _dialogueBase = dialogueBase;
        //获取已有的子对象并回收
        var childs = MyFind.FindAllChild(_rootDialogue);
        for (int i = 0; i < childs.Count; i++)
        {
            ObjectPool.Instance.RecycleObj(childs[i].gameObject);
            childs[i].gameObject.SetActive(false);
        }
        _textField.text = string.Format("{0}:{1}", _dialogueBase._npcName, _dialogueBase._dialogueText);
        //重新生成对话选项 更新文本 注册方法
        var optionList = _dialogueBase._dialogueOptionList;
        for (int i = 0; i < optionList.Count; i++)
        {
            GameObject go = ObjectPool.Instance.GetObject(_preCellDialogue.name);
            go.SetActive(true);
            go.transform.SetParent(_rootDialogue);
            var option = optionList[i];
            go.GetComponent<ButtonCell>().Init(option._btnDes);
            KeyCode keyCode = KeyCode.None;
            if (Enum.TryParse<KeyCode>(string.Format("Alpha{0}", i + 1), out keyCode))
            {
                if (_dicKeycodeAction.ContainsKey(keyCode))
                {
                    _dicKeycodeAction[keyCode] = () =>
                    {
                        OnClickAction?.Invoke(option._function, option._para);
                        _dicKeycodeAction[keyCode] = null;
                    };
                }
                else
                {
                    Debug.LogErrorFormat("错误注册的按键为Alpha{0}", i + 1);
                }
            }
            else
            {
                Debug.LogErrorFormat("解析到错误的keycode:Alpha{0}", i + 1);
            }
            //MyGameManager.Instance.Regist(Enum.Parse<KeyCode>(string.Format("Alpha{0}", i + 1)),
            //    () =>
            //    {
            //        OnClickAction?.Invoke(option._function, option._para);
            //    });
        }
        OnEndAction?.Invoke();
    }
    public void ClearTextField()
    {
        _textField.text = string.Empty;
    }
    private void KeycodeAction(KeyCode keyCode)
    {
        _dicKeycodeAction[keyCode]?.Invoke();
    }


}
