using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 负责任务的获取及更新
/// </summary>
public class MyQuestManager : MySingle<MyQuestManager>
{
    private MyQuestBase _curQuest;
    private List<MyQuestBase> _mQuestList;
    private Dictionary<string, List<MyQuestBase>> _dicPathQuests = new Dictionary<string, List<MyQuestBase>>();//不同路径的文件excel对应的任务list
    //Action中的string对应任务目标名称，int为对应的数量
    public Dictionary<EObjectiveType, Action<string, int>> _dicObservers = new Dictionary<EObjectiveType, Action<string, int>>();

    /// <summary>
    /// 获取数据,传入的是任务id，id的不同位数代表不同的信号，对应相应的文件夹/文件
    /// </summary>
    /// <param name="fileName">文件名</param>
    public void LoadData(string fileName)
    {
        //TODO
        _mQuestList = MyLoadDataManager.Instance.LoadQuestList(fileName);
    }

    /// <summary>
    /// 获取已加载的任务list中的指定id的quest
    /// </summary>
    /// <param name="questID"></param>
    /// <returns></returns>
    public MyQuestBase GetQuests(string questID = null)
    {
        if (questID == null) return _mQuestList.Count > 0 ? _mQuestList[0] : null;//不传入id则默认返回第一个任务
        MyQuestBase quest = _mQuestList.Find(t => t._questId == questID);
        if (quest == null) Debug.LogError("未加载到任务id为" + questID);
        return quest;
    }
    public void NextQuests()
    {
        //TODO 其实这里没有区分是哪个listGroup,错误的，因为id没改，所以后面还需要改，最好根据任务id来
        string nextId = _curQuest._nextQuestId;
        MyQuestBase quest = _mQuestList.Find(t => t._questId == nextId);
        SetCurQuests(quest);
    }
    #region 设置当前任务状态
    public void SetCurQuests(string questId)
    {
        var quest = GetQuests(questId);
        if (quest != null)
        {
            _curQuest = quest;
        }
    }
    public void SetCurQuests(MyQuestBase quest)
    {
        if (quest == null)
        {
            Debug.LogError("设置的任务不能为null,疑似存在错误");
            return;
        }
        _curQuest = quest;
        if (_curQuest._questStatus != EQuestStatus.Receiving) { _curQuest.Accept(); }
        ((UIQuestsWindow)UIManager.Instance.LoadWindow(EWindowUI.UIQuests)).UpdateTextPanel(_curQuest);

    }
    #endregion
    #region 观察者
    /// <summary>
    /// 任务状态观察者注册 严进严出 唯一控制
    /// </summary>
    /// <param name="isAdd"></param>
    /// <param name="type"></param>
    /// <param name="action"></param>
    public void AddAction(bool isAdd, EObjectiveType type, Action<string, int> action)
    {
        if (!_dicObservers.ContainsKey(type)) _dicObservers.Add(type, null);
        //需要严格保证quest的观察者注册 唯一注册，唯一注销，不然容易出现一个行为对该方法多次操作
        if (isAdd)
        {
            _dicObservers[type] += action;
        }
        else
        {
            _dicObservers[type] -= action;
        }
    }
    public void UpdateQuestsObserver(EObjectiveType objectiveType, string objectiveNmae, int objectiveAmount)
    {
        if (_dicObservers.ContainsKey(objectiveType) && _dicObservers[objectiveType] != null)
        {
            _dicObservers[objectiveType](objectiveNmae, objectiveAmount);
        }
    }
    #endregion
}
