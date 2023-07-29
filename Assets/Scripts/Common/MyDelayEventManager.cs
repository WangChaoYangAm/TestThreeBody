using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MyDelayEventManager : MySingle<MyDelayEventManager>
{
    private List<Action> _actionList = new List<Action>();
    private Dictionary<string, DelayAction> _dicAction = new Dictionary<string, DelayAction>();

    private void Update()
    {
        List<string> list = _dicAction.Keys.ToList();
        //移除已经完成或为null的键值对
        for (int i = 0; i < list.Count; i++)
        {
            DelayAction act = _dicAction[list[i]];

            //触发是否结束
            if (act != null)
            {
                act.CheckEnd();
            }
            act = _dicAction[list[i]];
            if (act == null || act.GetIsEnd)
                _dicAction.Remove(list[i]);
        }
    }
    public void Register(bool isRegister, string key, DelayAction delayAction)
    {
        if (isRegister)
        {
            if (_dicAction.ContainsKey(key))
            {
                _dicAction[key] = delayAction;
            }
            else
            {
                _dicAction.Add(key, delayAction);
            }
        }
        else
        {
            if (_dicAction.ContainsKey(key))
            {
                _dicAction.Remove(key);
            }
            else
            {
                Debug.LogError("移除了一个未注册的事件：" + key);
            }
        }
    }

}
public class MyDelayActionName
{
    public const string DIALOPGUE = "DIALOPGUE";
}
public class DelayAction
{
    float _delayTime;
    float _timer;
    bool _isEnd;
    Action OnDelayAction;

    public bool GetIsEnd { get { return _isEnd; } }

    public DelayAction(float delayTime, Action delayAction)
    {
        _isEnd = false;
        _delayTime = delayTime;
        OnDelayAction = delayAction;
        _timer = Time.time;
    }

    public void CheckEnd()
    {
        if (_isEnd) return;
        if (Time.time - _timer > _delayTime)
        {
            _isEnd = true;
            OnDelayAction?.Invoke();
        }
    }

}