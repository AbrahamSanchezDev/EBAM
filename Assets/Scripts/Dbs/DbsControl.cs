using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace UTS
{
    public class DbsControl : MonoBehaviour
    {
        public GameObject SignInGo, LoggedInGo;
        public GameObject LogInGo;

        private GoogleLoginControl _googleLoginControl;

        private RawImage _user;
        private TMP_Text _name, _email, _logText;

        protected void Awake()
        {
            SetupLogin();
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
        }

        private void BackToCalendar()
        {
            SceneManager.LoadScene("Main");
        }
    }
}