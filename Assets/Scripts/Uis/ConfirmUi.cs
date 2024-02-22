using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UTS
{
    public class ConfirmUi : MonoBehaviour
    {
        private GameObject _parentGo;

        private UnityAction _onCancel, _onOk;

        public static ConfirmUi Instance;

        protected void Awake()
        {
            Instance = this;
            Setup();
        }

        public void Setup()
        {
            if (_parentGo) return;

            _parentGo = transform.Find("ParentObj").gameObject;

            Show(false);

            var ok = _parentGo.transform.Find("Ok").GetComponent<Button>();
            var cancel = _parentGo.transform.Find("Cancel").GetComponent<Button>();

            ok.onClick.AddListener(Ok);
            cancel.onClick.AddListener(Cancel);
        }

        private void Show(bool show = true)
        {
            _parentGo.SetActive(show);
        }

        public void ShowOption(string theText, UnityAction onCancel, UnityAction onOk)
        {
            _onCancel = onCancel;
            _onOk = onOk;
            Show();
        }

        private void Cancel()
        {
            _onCancel?.Invoke();
            Show(false);
        }
        private void Ok()
        {
            _onOk?.Invoke();
            Show(false);
        }
    }
}
