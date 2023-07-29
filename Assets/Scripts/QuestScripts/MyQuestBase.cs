using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 任务状态
/// </summary>
public enum EQuestStatus
{
    NotAccept,
    Receiving,//进行中
    Complete,//已完成但未提交
    Failed,//失败
    Submit,//已提交
    Abort//放弃
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
            return _questStatus == EQuestStatus.Submit;
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
        OnChangeQuestStatus(EQuestStatus.Receiving);
    }
    protected virtual void OnChangeQuestStatus(EQuestStatus status)
    {
        switch (status)
        {
            case EQuestStatus.NotAccept:
                break;
            case EQuestStatus.Receiving:
                InitQuest();
                MyQuestManager.Instance.AddAction(true, _objectiveType, OnEndOperate);
                break;
            case EQuestStatus.Complete:
                //MyQuestManager.Instance.AddAction(false, _objectiveType, OnEndOperate);
                //其实应该切换状态的，但配置文件中的相关配置有问题，所以暂时采用不更新状态
                MyQuestManager.Instance.NextQuests();
                break;
            case EQuestStatus.Failed:
                MyQuestManager.Instance.AddAction(false, _objectiveType, OnEndOperate);
                break;

            case EQuestStatus.Submit:
                if (_questStatus != EQuestStatus.Complete && _questStatus != EQuestStatus.Failed)
                    Debug.LogError("要提交的任务，状态存在问题，请检查");
                break;
            case EQuestStatus.Abort:
                break;
        }
        _questStatus = status;

    }
    public virtual void ForceChangeStatus(EQuestStatus status)
    {
        OnChangeQuestStatus(status);
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
            //TODO 由于未配置需求数量，故暂时都采用1作为目标值
            if (_curAmount > 0)
            {
                OnChangeQuestStatus(EQuestStatus.Complete);
            }
        }
    }
}
