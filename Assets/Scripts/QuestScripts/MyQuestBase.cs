using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 任务状态
/// </summary>
public enum EQuestStatus
{
    NotAccept,
    Ongoing,
    Success,
    Failed,
    NotSubmit,
    Submited
}
public enum EObjectiveType
{
    None,
    EnterArea,
    ExitArea,
    Dialogue,
    Collect,
    Kill
}
[System.Serializable]
public class MyQuestBase
{
    public string _questId;
    public string _questName;
    public string _questDes;
    public string _nextQuestId;
    public string _objectiveId;//目标id
    public EObjectiveType _objectiveType;//目标类型
    public EQuestStatus _questStatus;
    private int _curAmount;
    public bool IsSubmit
    {
        get
        {
            return _questStatus == EQuestStatus.Submited;
        }
    }
    public int GetCurAmount
    {
        get { return _curAmount; }
    }
    public void InitQuest()
    {
        _questStatus = EQuestStatus.NotAccept;
        _curAmount = 0;
    }
    public virtual void Accept()
    {
        OnChangeQuestStatus(EQuestStatus.NotAccept);
    }
    public virtual void OnChangeQuestStatus(EQuestStatus status)
    {
        switch (status)
        {
            case EQuestStatus.NotAccept:
                InitQuest();
                _questStatus = EQuestStatus.Ongoing;
                MyQuestManager.Instance.AddAction(true, _objectiveType, OnEndOperate);
                break;
            case EQuestStatus.Success:
                MyQuestManager.Instance.AddAction(false, _objectiveType, OnEndOperate);
                break;
            case EQuestStatus.Failed:
                MyQuestManager.Instance.AddAction(false, _objectiveType, OnEndOperate);
                break;

            case EQuestStatus.Submited:
                if (_questStatus != EQuestStatus.Success && _questStatus != EQuestStatus.Failed)
                    Debug.LogError("要提交的任务，状态存在问题，请检查");
                _questStatus = EQuestStatus.Submited;
                break;
        }
    }
    /// <summary>
    /// 任务更新操作 用于接收触发类型及更新数量
    /// </summary>
    /// <param name="key"></param>
    /// <param name="amount"></param>
    public virtual void OnEndOperate(string key, int amount)
    {
        UpdateOperateAmount(key, amount);
    }
    protected virtual void UpdateOperateAmount(string key, int amount)
    {
        if (key == _objectiveId)
        {
            _curAmount += amount;
        }
    }
}
