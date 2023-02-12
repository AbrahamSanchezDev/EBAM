using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTS
{
    public class ClassDataControlUi : MonoBehaviour
    {
        protected void Awake()
        {
        }

        private GameObject _mainGo;

        public void Setup(Transform theParent)
        {
            var prefabs = PrefabRefs.Instance;
            var scroll = Instantiate(prefabs.DisplayScrollView, theParent);
            _mainGo = scroll.gameObject;



            var data = SchoolInfo.CurInfo;

            for (int i = 1; i < data.ClassInfo.Count; i++)
            {
                var cur = data.ClassInfo[i];
                var but = Instantiate(prefabs.ClassButton, scroll.content);
                but.SetData(cur.TheName,new Color32(cur.r, cur.g, cur.b,255));
            }

        }

        public void Show(bool show)
        {
            _mainGo.SetActive(show);
        }
    }
}