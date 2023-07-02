using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class MyGameManager : MySingle<MyGameManager>
{

    void Start()
    {
        //MyLoadDataManager.Instance.LoadDialogueList();
        DialogueManager.Instance.InitDialogueGroup("301102");
    }

}
