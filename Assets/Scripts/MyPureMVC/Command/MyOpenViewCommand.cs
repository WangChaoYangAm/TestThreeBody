using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace MyMVC
{
    public class MyOpenViewCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            base.Execute(notification);
            string windowName = notification.Body as string;
            if (Enum.TryParse<EWindowUI>(windowName, out EWindowUI eWindow))
            {
                UIManager.Instance.LoadWindow(eWindow);
                string mediator = windowName + "Mediator";
                Type type = Type.GetType(mediator);
                var mediatorObj = Activator.CreateInstance(type);
            }
            else Debug.LogError("未加载到对应的窗口：" + windowName);
        }
    }
}
