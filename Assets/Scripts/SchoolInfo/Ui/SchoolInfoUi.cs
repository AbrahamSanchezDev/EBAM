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
        private TeachersDataControlUi _teachersDataControl;
        private HoursDataControlUi _hoursDataControl;
        private GameObject _parentGo;

        protected void Setup()
        {
            var parentGo = transform.GetChild(0);

            _parentGo = parentGo.gameObject;
            Show();

            _tabs = parentGo.Find("Tabs");

            AddTab("Materias", 0);
            AddTab("Maestros", 1);
            AddTab("Salones", 2);
            AddTab("Horarios", 3);

            _classDataControl = gameObject.AddComponent<ClassDataControlUi>();
            _teachersDataControl = gameObject.AddComponent<TeachersDataControlUi>();
            _hoursDataControl = gameObject.AddComponent<HoursDataControlUi>();

            _classDataControl.Setup(parentGo);
            _teachersDataControl.Setup(parentGo);
            _hoursDataControl.Setup(parentGo);

            var adds = parentGo.Find("Adds");
            var add = adds.Find("Add").GetComponent<Button>();
            add.onClick.AddListener(OnAdd);

            OnPressTab(0);
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
            _teachersDataControl.ShowMainUi(index == 1);

            _hoursDataControl.ShowMainUi(index == 3);

        }

        private void OnAdd()
        {
            switch (_curIndex)
            {
                case 0:
                    _classDataControl.AddData();
                    break;
                case 1:
                    _teachersDataControl.AddData();
                    break;
                case 2:
                    break;
                case 3:
                    _hoursDataControl.AddData();
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
                    _teachersDataControl.UpdateData();
                    _hoursDataControl.UpdateData();
                    break;
            }
        }
    }
}