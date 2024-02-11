using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UTS
{
    public class MyDbsControl : MonoBehaviour
    {
        private GameObject _parentObj;

        private AddInfoUi _addInfoUi => AddInfoUi.Instance;

        protected void Awake()
        {
            Setup();

            //DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        }

        protected void Setup()
        {
            _parentObj = transform.Find("ParentObj").gameObject;

            var adds = _parentObj.transform.Find("Adds").transform.Find("Add").GetComponent<Button>();

            adds.onClick.AddListener(AddCurrent);
        }


        public void Show(bool show)
        {
            _parentObj.SetActive(show);
        }

        private void AddCurrent()
        {
            Show(false);
            _addInfoUi.SetEvents(OnCancel, OnConfirmAdd);
            _addInfoUi.HideAllInputs();
            _addInfoUi._inputUi.SetDisplayText("Guardar DB Como :", "");
            _addInfoUi.Show(true);
        }

        private void OnCancel()
        {
            Show(true);
        }

        private void OnConfirmAdd()
        {
            Show(true);

            var theName = _addInfoUi._inputUi.GetText();
            Debug.Log(theName);
        }
    }
}