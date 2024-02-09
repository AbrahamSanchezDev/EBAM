using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UTS
{
    [CreateAssetMenu]
    public class PrefabRefs : ScriptableObject
    {
        private static PrefabRefs _instance;
        public static PrefabRefs Instance
        {
            get
            {
                if (_instance)
                {
                    return _instance;
                }

                _instance = Resources.Load<PrefabRefs>("PrefabRefs");
                return _instance;
            }
        }
        public GameObject HoursParent;

        public HourUiDisplay TheHourUiDisplay;

        [Header("School Info")]
        public Button TabButtons;

        public ClassDataButton ClassButton;


        public HourUiDisplay HourDisplay;
        //public ClassDataButton HourButton;
        public DropDownInputUiObj dropDownInputUi;

        public ScrollRect DisplayScrollView;

        public ScrollRect DisplayHoursScrollView;
        public DaysUiControl DaysContainer;

    }
}