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
#if UNITY_EDITOR
                UnityEditor.EditorApplication.ExitPlaymode();
#endif
                Application.Quit();
            });
        }
    }
}