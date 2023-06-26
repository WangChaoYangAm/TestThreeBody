using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIDialogueWindow : MyBaseWindow
{
    [SerializeField]
    private RectTransform _rootDialogue;
    [SerializeField]
    private GameObject _preCellDialogue;
    //[SerializeField]
    //private Button _btnLeft, _btnRight;
    //[SerializeField]
    //private Text _textLeft, _textRight;
    private MyDialogueBase _dialogueBase;
    public Action<EDialogueFunc, string> OnClickAction;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateDialogue(MyDialogueBase dialogueBase)
    {
        _dialogueBase = dialogueBase;
        //获取已有的子对象并回收
        var childs = MyFind.FindAllChild(_rootDialogue);
        for (int i = 0; i < childs.Count; i++)
        {
            ObjectPool.Instance.RecycleObj(childs[i].gameObject);
            childs[i].gameObject.SetActive(false);
        }
        //重新生成对话 更新文本 注册方法
        var optionList = _dialogueBase._dialogueOptionList;
        for (int i = 0; i < optionList.Count; i++)
        {
            GameObject go = ObjectPool.Instance.GetObject(_preCellDialogue.name);
            go.SetActive(true);
            go.GetComponent<ButtonCell>().Init(optionList[i]._btnDes, () => { OnClickAction?.Invoke(optionList[i]._function, optionList[i]._para); });
        }

    }


}
