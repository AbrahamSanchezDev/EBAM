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

        public Button TheButton;


        public Image _theImage;

        protected void Awake()
        {
            Setup();
        }
        public void Setup() {
            if (_input) return;
            _input = GetComponentInChildren<TMP_InputField>();
            _theText = transform.Find("Title").GetComponentInChildren<TextMeshProUGUI>();
            TheButton = GetComponentInChildren<Button>();

            if (TheButton)
            {
                _theImage = TheButton.gameObject.GetComponent<Image>();
            }
        }

        public string GetText()
        {
            return _input.text;
        }


        public void SetDisplayText(string theTitle,string theContent)
        {
            SetDisplayText(theTitle);
            SetText( theContent);
        }
        public void SetText(string theText)
        {
            Setup();
            _input.text = theText;
        }
        public void SetDisplayText(string theText)
        {
            Setup();
            _theText.text = theText + ": ";
        }

        public void ClearText()
        {
            _input.text = "";
        }

        public void SetColorPickerColor(Color theColor)
        {
            Setup();
            if (_theImage)
            {
                _theImage.color = theColor;
            }
            else
            {
                Debug.Log("NO COLOR!");
            }
        }
    }
}