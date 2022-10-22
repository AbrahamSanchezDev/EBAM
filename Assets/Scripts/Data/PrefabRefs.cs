using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}