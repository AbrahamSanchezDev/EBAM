using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UTS
{
    public class DropDownInputUiObj : MonoBehaviour
    {

        private TMP_Dropdown _options;
        private TextMeshProUGUI _theText;

        public Image _theImage;

        protected void Awake()
        {
            Setup();
        }
        public void Setup() {
            if (_options) return;
            _options = GetComponentInChildren<TMP_Dropdown>();
            _theText = transform.Find("Title").GetComponentInChildren<TextMeshProUGUI>();            
        }

        public string GetText()
        {
            return _options.options[_options.value].text;
        }
        public int GetPosition()
        {
            return _options.value;
        }

        public void SetDisplayText(string theTitle, List<string> theContent)
        {
            SetDisplayText(theTitle);
            SetOptions( theContent);
        }
        public void SetOptions(List<string> theText)
        {
            _options.options.Clear();
            _options.AddOptions(theText);
        }

        public void SetCurrentOption(int index)
        {
            _options.value = index;
        }
        public void SetDisplayText(string theText)
        {
            Setup();
            _theText.text = theText + ": ";
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