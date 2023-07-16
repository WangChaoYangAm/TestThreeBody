using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.UIElements;

public class UIDialogueWindow : MyBaseWindow
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
    public void Init(Action EndAction, Action<EDialogueFunc, string> ClickAction)
    {
        ObjectPool.Instance.SetPrefab(_preCellDialogue);
        OnEndAction = EndAction;
        OnClickAction = ClickAction;
    }

    public void UpdateDialogue(MyDialogueBase dialogueBase)
    {
        if (dialogueBase == null) return;
        if (!this.gameObject.activeSelf)//确保UI界面是打开的
        {
            ShowWindow(false);
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
            go.GetComponent<ButtonCell>().Init(option._btnDes, () => { OnClickAction?.Invoke(option._function, option._para); });
        }
        OnEndAction?.Invoke();
    }
    public void ClearTextField()
    {
        _textField.text = string.Empty;
    }

}
