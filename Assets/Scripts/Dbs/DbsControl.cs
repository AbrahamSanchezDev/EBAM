using System.Collections;
using System.Collections.Generic;
//using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UTS
{
    public class DbsControl : MonoBehaviour
    {
        protected void Awake()
        {
            Setup();
        }

        protected void Setup()
        {
        }

        protected IEnumerator Start()
        {
            yield return null;
            BackToPreviewsControl.ShowGoBack(BackToCalendar);
        }

        private void BackToCalendar()
        {
            SceneManager.LoadScene("Main");
        }

        private string webId = "901758928354-ahedg2a6s7bcmiottm0bjdljvfc5tbqv.apps.googleusercontent.com";
        private string webSec = "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";
        private void SignIn()
        {
            //Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

            //auth.SignInAnonymouslyAsync().ContinueWith(task =>
            //{

            //});
        }
    }
}