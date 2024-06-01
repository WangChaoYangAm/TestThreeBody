using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyFacade
{
    //static Dictionary<string, List<Imsg>> _dicImsgObj = new Dictionary<string, List<Imsg>>();
    static Dictionary<string, List<Imsg>> _dicImsgObj = new Dictionary<string, List<Imsg>>();
    public static void Register(string name,Imsg imsg)
    {
        if(!_dicImsgObj.ContainsKey(name))
        _dicImsgObj.Add(name,new List<Imsg>());
        _dicImsgObj[name].Add(imsg);

    }
    public static void SendMsg(string myCommand, MyResponseData data)
    {
        foreach (var item in _dicImsgObj)
        {
            if (item.Key == myCommand)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    item.Value[i].RecieveMsg(data);
                }
                
            }
        }
    }
}
/// <summary>
/// �������������ݶ�Ҫת���������ʽ
/// </summary>
public class MyResponseData
{
    /// <summary>
    /// �����������Ϊ������
    /// </summary>
    public string _actionName;
    /// <summary>
    /// ���������ݵ�����
    /// </summary>
    public object _data;
    /// <summary>
    /// �ص���������Ҫ�ش���������object����ʽ���ͻ�ȥ
    /// </summary>
    public Action<object> _action;
}