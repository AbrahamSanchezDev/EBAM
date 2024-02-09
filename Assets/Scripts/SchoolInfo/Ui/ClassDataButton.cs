using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UTS
{
    public class ClassDataButton : MonoBehaviour
    {
        private TextMeshProUGUI _theText;
        private Image _image;

        public UnityEvent OnClick = new UnityEvent();

        protected void Awake()
        {
            Setup();
        }

        protected void Setup()
        {
            if(_image)return;
            _theText = transform.GetComponentInChildren<TextMeshProUGUI>();
            _image = transform.Find("Color").GetComponent<Image>();

            var but = gameObject.GetComponent<Button>();
            but.onClick.AddListener(DoOnClick);
        }
        private void DoOnClick()
        {
            OnClick?.Invoke();
        }
        public void SetData(string theText, Color theColor)
        {
            Setup();
            if (_theText)
                _theText.text = theText;
            else
                Debug.Log("No Text in " + gameObject.name);
            if (_image)
            _image.color = theColor;
        }
        public string GetText()
        {
            return _theText.text;
        }
        public void SetData(string theText)
        {
            Setup();
            if (_theText)
                _theText.text = theText;
            else
                Debug.Log("No Text in " + gameObject.name);
            if (_image)
                _image.gameObject.SetActive(false);
        }

    }
}