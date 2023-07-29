using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyDelayEventManager : MySingle<MyDelayEventManager>
{
    private List<Action> _actionList = new List<Action>();
    public Dictionary<string, DelayAction> _dicAction = new Dictionary<string, DelayAction>();

    private void Update()
    {
        foreach (var item in _dicAction)
        {
            //移除已经完成的时间
            if (item.Value == null || item.Value.GetIsEnd)
            {
                _dicAction.Remove(item.Key);
            }
            else
            {
                //触发是否结束
                item.Value.CheckEnd();
            }
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
        CheckEnd(_delayTime == 0);
    }
    public void CheckEnd(bool isNow = false)
    {

        if (Time.time - _timer > _delayTime || isNow)
        {
            OnDelayAction?.Invoke();
            _isEnd = true;
        }
    }

}