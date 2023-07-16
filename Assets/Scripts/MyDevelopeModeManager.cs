using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDevelopeModeManager : MySingle<MyDevelopeModeManager>
{
    [Header("������ģʽ���������ã���Fʹ��")]
    public bool IsDevelopMode;
    [SerializeField]
    private EObjectiveType TestObjectiveType;
    [SerializeField]
    private string TestObjectiveName;
    [SerializeField]
    private int TestAmount;
    // Start is called before the first frame update
    void Start()
    {
        MyGameManager.Instance.Regist(KeyCode.Q, () =>
        {
            if (IsDevelopMode)
            {
                MyGameManager.Instance.UpdateQuestsState(TestObjectiveType, TestObjectiveName, TestAmount);
            }
        });
    }

    // Update is called once per frame

}