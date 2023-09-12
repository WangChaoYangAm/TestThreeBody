using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyMVC
{
    public class UIPackageWindowMediator : Mediator
    {
        public new static string NAME = "UIPackageWindowMediator";

        public UIPackageWindowMediator(string mediatorName, object viewComponent = null) : base(mediatorName, viewComponent)
        {
        }

    }
}