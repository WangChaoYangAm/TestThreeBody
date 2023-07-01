using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDataBase;
using QFramework;

public class MyGameManager : MySingle<MyGameManager>
{

    void Start()
    {
        MyLoadDataManager.Instance.LoadDialogueList("");
    }

}
