using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UTS
{
    public class SchoolInfoUi : ScreenController
    {
        private int _curIndex;

        protected void Start()
        {
            Setup();
        }

        private Transform _tabs;


        private ClassDataControlUi _classDataControl;

        private GameObject _parentGo;

        protected void Setup()
        {
            var parentGo = transform.GetChild(0);

            _parentGo = parentGo.gameObject;
            Show();

            _tabs = parentGo.Find("Tabs");

            AddTab("Classes", 0);
            AddTab("Maestros", 1);
            AddTab("Rooms", 2);

            _classDataControl = gameObject.AddComponent<ClassDataControlUi>();

            _classDataControl.Setup(parentGo);


            var adds = parentGo.Find("Adds");
            var add = adds.Find("Add").GetComponent<Button>();
            add.onClick.AddListener(OnAdd);
            Show(false);
        }

        public void Show(bool show = true)
        {
            _parentGo.SetActive(show);
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
            _curIndex = index;
            _classDataControl.ShowMainUi(index == 0);

        }

        private void OnAdd()
        {
            switch (_curIndex)
            {
                case 0:
                    _classDataControl.AddData();
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
        }

        protected override void OnShowScreen(ScreenName theScreen)
        {
            Show(theScreen == ScreenName.EditarHorarios);
        }

        protected override void OnDisplayAction(DisplayWindowAction theAction)
        {

            switch (theAction)
            {
                case DisplayWindowAction.UpdateData:
                    _classDataControl.UpdateData();
                    break;
            }
        }
    }
}