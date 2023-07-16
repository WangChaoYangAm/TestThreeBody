using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDevelopeModeManager : MySingle<MyDevelopeModeManager>
{
    [Header("开发者模式触发任务用，按F使用")]
    private EObjectiveType TestObjectiveType;
    private string TestObjectiveName;
    private int TestAmount;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            MyGameManager.Instance.UpdateQuestsState(TestObjectiveType, TestObjectiveName, TestAmount);
        }
    }
}
