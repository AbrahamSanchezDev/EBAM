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
        public static MyDbsControl Instance;

        private GameObject _parentObj;

        private AddInfoUi _addInfoUi => AddInfoUi.Instance;

        private DatabaseReference reference;

        private Button _addButton;

        private Transform _dbsParent;
        private ClassDataButton _dataButton;

        protected void Awake()
        {
            Instance = this;
            Setup();
        }


        public void Setup()
        {
            if (_parentObj) return;
            _parentObj = transform.Find("ParentObj").gameObject;

            _dbsParent = _parentObj.GetComponentInChildren<ScrollRect>().content;
            _dataButton = PrefabRefs.Instance.ClassButton;

            _addButton = _parentObj.transform.Find("Adds").transform.Find("Add").GetComponent<Button>();

            _addButton.onClick.AddListener(AddCurrent);
        }

        protected void OnEnable()
        {
            DbsControl.OnDbsLoaded.AddListener(ShowDbs);
        }
        protected void OnDisable()
        {
            DbsControl.OnDbsLoaded.RemoveListener(ShowDbs);
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

        public void ShowAddButton(bool show)
        {
            _addButton.gameObject.SetActive(show);
            //Debug.Log("SHOWING ADD " + show);
        }


        public void ShowDbs(List<DBInfo> dbs)
        {
            if (_dbsParent)
            {
                for (int i = 0; i < _dbsParent.childCount; i++)
                {
                    DestroyImmediate(_dbsParent.GetChild(0).gameObject);
                    i--;
                }
            }

            Setup();
            for (int i = 0; i < dbs.Count; i++)
            {
                var but = Instantiate(_dataButton, _dbsParent);
                but.SetData(dbs[i].Name);
                int index = i;
                but.OnClick.AddListener(() =>
                {
                    //dbs[i].LoadLocal();
                    LoadDb(dbs[index]);
                });

            }
        }

        private void LoadDb(DBInfo info)
        {
            ConfirmUi.Instance.ShowOption("", null, () =>
            {
                info.SaveToLocal();
                StartCoroutine(nameof(LoadMainSceneCo));
            });
        }

        protected IEnumerator LoadMainSceneCo()
        {
            yield return new WaitForSeconds(0.1f);
            BackToPreviewsControl.CallGoBack();

        }
    }
}