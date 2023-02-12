using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UTS
{
    public class ClassDataButton : MonoBehaviour
    {
        private TextMeshProUGUI _theText;
        private Image _image;

        protected void Awake()
        {
            Setup();
        }

        protected void Setup()
        {
            if(_image)return;
            _theText = transform.GetComponentInChildren<TextMeshProUGUI>();
            _image = transform.Find("Color").GetComponent<Image>();
        }

        public void SetData(string theText, Color theColor)
        {
            _theText.text = theText;
            _image.color = theColor;
        }

        
    }
}