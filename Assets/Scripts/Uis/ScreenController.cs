using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UTS
{
    public abstract class ScreenController : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            MainSetup.ChangeToScreen.AddListener(OnShowScreen);
            MainSetup.DisplayWindowAction.AddListener(OnDisplayAction);
        }
        protected virtual void OnDisable()
        {
            MainSetup.ChangeToScreen.RemoveListener(OnShowScreen);
            MainSetup.DisplayWindowAction.RemoveListener(OnDisplayAction);
        }

        protected abstract void OnShowScreen(ScreenName theScreen);


        protected abstract void OnDisplayAction(DisplayWindowAction theAction);
    }
}
