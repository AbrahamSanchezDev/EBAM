using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UTS
{
    public class BackToPreviewsControl : MonoBehaviour
    {
        private static BackToPreviewsControl Instance;

        private GameObject _backUiGo;
        private UnityAction _backAction;

        protected void Awake()
        {
            Setup();
            Instance = this;

        }

        private void Setup()
        {
            _backUiGo = transform.Find("Back").gameObject;

            var but = _backUiGo.GetComponent<Button>();
            but.onClick.AddListener(DoGoBack);
            Show(false);
        }

        public static void ShowGoBack(UnityAction backTo)
        {
            if (Instance)
            {
                Instance._backAction = backTo;
                Instance.Show();
            }
        }

        private void Show(bool show = true)
        {
            _backUiGo.SetActive(show);
        }
        private void DoGoBack()
        {
            _backAction?.Invoke();
            _backAction = null;
            Show(false);
        }
    }
}
