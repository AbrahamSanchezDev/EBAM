using UnityEngine;
using UnityEngine.UI;

namespace UTS
{
    public class CloseApp : MonoBehaviour
    {
        protected void Awake()
        {
            Setup();
        }

        protected void Setup()
        {
            var but = GetComponent<Button>();
            but.onClick.AddListener(() =>
            {

                Application.Quit();


            });
        }
    }
}