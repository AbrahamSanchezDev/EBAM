using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;

namespace UTS
{
    public class MyDbsControl : MonoBehaviour
    {
        private GameObject _parentObj;

        private AddInfoUi _addInfoUi => AddInfoUi.Instance;

        private DatabaseReference reference;

        protected void Awake()
        {
            Setup();
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

            var fullName = theName;
            var data = new DBInfo();
            data.LoadLocal();


            if (FirebaseAuth.DefaultInstance.CurrentUser != null)
            {
                fullName = FirebaseAuth.DefaultInstance.CurrentUser.Email + "_" + theName;

                data.Creator = FirebaseAuth.DefaultInstance.CurrentUser.Email;
            }

            data.Name = theName;


            string json = JsonUtility.ToJson(data);


            reference = FirebaseDatabase.DefaultInstance.RootReference;


            reference.Child("dbs").Child(fullName).SetRawJsonValueAsync(json);

            Debug.Log("SAVED! " + fullName);
        }
    }
}