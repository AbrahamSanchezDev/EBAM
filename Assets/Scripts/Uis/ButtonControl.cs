using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UTS
{
    public class ButtonControl : MonoBehaviour
    {
        public void Setup(UnityAction onClick, string theText)
        {

            var but = GetComponent<Button>();
            but.onClick.AddListener(onClick);

            gameObject.GetComponentInChildren<TMPro.TMP_Text>().text = theText;
        }
    }
}
