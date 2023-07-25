using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDelayEventManager : MonoBehaviour
{
    private List<Action> _actionList = new List<Action>();
    public Dictionary<string,Action> _dic
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Register(bool isRegister, Action action)
    {
        
    }
}
