using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using Firebase.Database;
using UnityEngine.Events;


namespace UTS
{
    public class DbsControl : MonoBehaviour
    {

        public static UnityEvent<List<DBInfo>> OnDbsLoaded = new UnityEvent<List<DBInfo>>();

        public GameObject SignInGo, LoggedInGo;
        public GameObject LogInGo;
        public MyDbsControl MyDbsControl;

        private GoogleLoginControl _googleLoginControl;

        private RawImage _user;
        private TMP_Text _name, _email, _logText;

        public GameObject Tabs;



        protected void Awake()
        {
            MyDbsControl.Setup();
            SetupLogin();

            var prefabs = PrefabRefs.Instance;
            var loginBut = Instantiate(prefabs.TabButtons, Tabs.transform);
            loginBut.Setup(ShowLogin, "Login");

            var dbsBut = Instantiate(prefabs.TabButtons, Tabs.transform);
            dbsBut.Setup(ShowDbs, "DBs");


            if (MyDbsControl.Instance)
            {
                MyDbsControl.Instance.ShowAddButton(false);
            }
            ShowLogin();
        }

        protected void OnEnable()
        {
            GoogleLoginControl.OnUserLoadedEvent.AddListener(CheckUser);
        }

        protected void OnDisable()
        {
            GoogleLoginControl.OnUserLoadedEvent.RemoveListener(CheckUser);
        }

        #region GoogleSignIn

        #region GetDbs

        private void LoadDbs()
        {

            var reference = FirebaseDatabase.DefaultInstance.RootReference;

            reference.Child("dbs").GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("FAIL to load " + task.Status);
                }
                else if (task.IsCompleted)
                {
                    var snap = task.Result;
                    theData = new List<DBInfo>();
                    foreach (DataSnapshot info in snap.Children)
                    {
                        var text = info.GetRawJsonValue();
                        Debug.Log(string.Format("{0}: {1}", info.Key, text));
                        var data = JsonUtility.FromJson<DBInfo>(text);
                        theData.Add(data);
                    }
                }
            });


        }

        private void ShowCurrentDbs()
        {
            OnDbsLoaded?.Invoke(theData);
        }
        private void ShowAddBut()
        {
            var show = FirebaseAuth.DefaultInstance.CurrentUser != null;

#if UNITY_EDITOR
            show = true;
#endif
            //Debug.Log(show);
            if (MyDbsControl.Instance)
            {
                MyDbsControl.Instance.ShowAddButton(show);
            }
            else
            {
                Debug.Log("No INSTANCE");
            }
        }

        public List<DBInfo> theData;

        #endregion
        private void SetupLogin()
        {
            _googleLoginControl = gameObject.AddComponent<GoogleLoginControl>();

            var parentSign = SignInGo.transform.Find("ParentObj");
            var signInButton = parentSign.Find("SignInButton").GetComponent<Button>();
            signInButton.onClick.AddListener(SignToGoogle);

            var loggedInParent = LoggedInGo.transform.Find("ParentObj");

            _user = loggedInParent.Find("user").GetComponent<RawImage>();
            _name = loggedInParent.Find("name").GetComponent<TMP_Text>();
            _email = loggedInParent.Find("email").GetComponent<TMP_Text>();

            _logText = loggedInParent.Find("log").GetComponent<TMP_Text>();
            var logOutButton = loggedInParent.Find("LogOutButton").GetComponent<Button>();
            logOutButton.onClick.AddListener(LogOutFromGoogle);

            CheckUser(null);
        }

        private void SignToGoogle()
        {
            _googleLoginControl.SignInWithGoogle();
        }

        private void LogOutFromGoogle()
        {
            _googleLoginControl.SignOutFromGoogle();
        }

        private void CheckUser(FirebaseUser user)
        {
            SignInGo.SetActive(user == null);
            LoggedInGo.SetActive(user != null);
            _user.gameObject.SetActive(false);
            if (user != null)
            {
                _name.text = user.DisplayName;
                _email.text = user.Email;
                _logText.text = "";
                if (user.PhotoUrl != null)
                    StartCoroutine(nameof(LoadProfilePic), user.PhotoUrl.AbsoluteUri);
                else
                    _logText.text = "No existe foto";
            }

        }

        private void ShowLoginSignIn(bool show)
        {
            LogInGo.SetActive(show);
        }

        private IEnumerator LoadProfilePic(string url)
        {
            var www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var texture = DownloadHandlerTexture.GetContent(www);
                _user.texture = texture;
                _user.gameObject.SetActive(true);
            }
            else
            {
                _logText.text = "No Image found!";
            }
        }

        #endregion




        protected IEnumerator Start()
        {
            yield return null;
            BackToPreviewsControl.ShowGoBack(BackToCalendar);

            yield return new WaitForSeconds(1f);
            LoadDbs();
        }

        private void BackToCalendar()
        {
            SceneManager.LoadScene("Main");
        }

        private void HideAllViews()
        {
            MyDbsControl.Show(false);
            LogInGo.SetActive(false);
        }
        private void ShowLogin()
        {
            HideAllViews();
            LogInGo.SetActive(true);

        }
        private void ShowDbs()
        {
            HideAllViews();
            MyDbsControl.Show(true);
            ShowCurrentDbs();
            ShowAddBut();
        }
    }
}