using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDataBase
{
    /// <summary>
    /// 单条对话
    /// </summary>
    public class ConfigDialogue_Single
    {
        public string _btnDes;
        public EDialogueFunc _function;
        public string _para;

        public bool HaveFunction
        {
            get { return _function != EDialogueFunc.None; }
        }
    }
    public enum EModule
    {
        UIManager,
        QuestsManager,
        DialogueManager
    }
    public enum EWindowUI
    {
        UIDialogue
    }
}

