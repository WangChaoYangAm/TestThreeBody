using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyFacade
{
    //static Dictionary<string, List<Imsg>> _dicImsgObj = new Dictionary<string, List<Imsg>>();
    static Dictionary<string, List<Imsg>> _dicImsgObj = new Dictionary<string, List<Imsg>>();
    public static void Register(string name, Imsg imsg)
    {
        if (!_dicImsgObj.ContainsKey(name))
            _dicImsgObj.Add(name, new List<Imsg>());
        _dicImsgObj[name].Add(imsg);

    }
    public static void SendMsg(string command, object data)
    {
        if (_dicImsgObj.TryGetValue(command, out var listMsgs))
        {
            for (int i = 0; i < listMsgs.Count; i++)
            {
                listMsgs[i].RecieveMsg(command, data);
            }
        }
    }
}
/// <summary>
/// 所有命令传输的数据都要转换成这个格式
/// </summary>
public class MyResponseData
{
    /// <summary>
    /// 命令、操作、行为的名称
    /// </summary>
    public string _actionName;
    /// <summary>
    /// 命令所传递的数据
    /// </summary>
    public object _data;
    /// <summary>
    /// 回调，并将需要回传的数据以object的形式发送回去
    /// </summary>
    public Action<object> _action;
}
