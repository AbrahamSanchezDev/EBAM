using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UTS
{
    public class SchoolInfoUi : MonoBehaviour
    {
        protected void Awake()
        {
            Setup();
        }

        private Transform _tabs;


        private ClassDataControlUi _classDataControl;

        protected void Setup()
        {
            var prefabs = PrefabRefs.Instance;

            var parentGo = transform.GetChild(0);
            _tabs = parentGo.Find("Tabs");

            AddTab("Classes",0);
            AddTab("Maestros", 1);
            AddTab("Rooms", 2);

            _classDataControl = gameObject.AddComponent<ClassDataControlUi>();

            _classDataControl.Setup(parentGo);
        }

        private void AddTab(string displayText,int index)
        {
            var prefabs = PrefabRefs.Instance;
            var theClassesBut = Instantiate(prefabs.TabButtons, _tabs);
            var theText = theClassesBut.GetComponentInChildren<TextMeshProUGUI>();
            theText.text = displayText;

            theClassesBut.onClick.AddListener((() => OnPressTab(index)));
        }

        private void OnPressTab(int index)
        {

            _classDataControl.Show(index == 0);

        }


    }
}