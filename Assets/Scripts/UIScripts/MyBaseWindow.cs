using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWindowType
{
    Normal,
    Fixed,
    Pop,
    Top
}
public class MyBaseWindow : MonoBehaviour
{
    public string _windowName;
    public EWindowType _windowType;

    private void Start()
    {
        _windowName = this.GetType().ToString();
    }

    public virtual void ShowWindow(bool isShow)
    {
        this.gameObject.SetActive(isShow);
    }

}
