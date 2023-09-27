using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UTS
{
    public class InputUiObj : MonoBehaviour
    {

        private TMP_InputField _input;
        private TextMeshProUGUI _theText;

        protected void Awake()
        {
            Setup();
        }
        protected void Setup() {
            _input = GetComponentInChildren<TMP_InputField>();
            _theText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public string GetText()
        {
            return _input.text;
        }

        public void SetDisplayText(string theText)
        {
            _theText.text = theText;
        }

        public void ClearText()
        {
            _input.text = "";
        }
    }
}