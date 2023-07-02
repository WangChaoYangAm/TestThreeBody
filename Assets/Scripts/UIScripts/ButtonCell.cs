using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ButtonCell : MonoBehaviour
{
    [SerializeField]
    private Button _btn;
    [SerializeField]
    private Text _textName;
    public Action OnClickAction;
    private void Awake()
    {
        TryGetComponent(out _btn);
    }
    public void Init(string name, Action action = null)
    {
        if (_btn != null)
        {
            _btn.onClick.RemoveAllListeners();
            _btn.onClick.AddListener(OnClick);
            if(_textName)
            _textName.text = name;
        }
        OnClickAction = action;
    }
    public void OnClick()
    {
        OnClickAction?.Invoke();
    }
}
