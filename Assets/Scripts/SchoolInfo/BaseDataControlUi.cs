using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UTS
{
    public abstract class BaseDataControlUi : MonoBehaviour
    {
        protected GameObject _mainGo;
        protected ScrollRect _scroll;

        protected AddInfoUi _infoUi;

        public virtual void Setup(Transform theParent)
        {
            var prefabs = PrefabRefs.Instance;
            _scroll = Instantiate(prefabs.DisplayScrollView, theParent);
            _mainGo = transform.Find("ParentObj").gameObject;
            _infoUi = AddInfoUi.Instance;
            UpdateData();
        }

        public virtual void UpdateData()
        {
            while (_scroll.content.GetChildCount() > 0)
            {
                DestroyImmediate(_scroll.content.GetChild(0).gameObject);
            }
        }
        protected void Show(bool show)
        {
            _mainGo.SetActive(show);
        }

        protected void StartAddUi()
        {
            Show(false);
            if(_infoUi)
                _infoUi = AddInfoUi.Instance;
            if(_infoUi == null)
            {
                Debug.Log("NO INSTANCE!");
            }
            _infoUi.HideAllInputs();
            _infoUi.Show(true);
        }

        public void ShowMainUi(bool show)
        {
            _scroll.gameObject.SetActive(show);
        }

        #region Edit data

        protected void CancelEdit()
        {
            Show(true);
        }

        protected virtual void EditDataOn(int index)
        {
            StartAddUi();
        }
        protected virtual  void DeleteAt(int index)
        {
            Show(true);
        }
        protected abstract void UpdateDataAt(int index);
        #endregion

        public virtual void AddData()
        {
            StartAddUi();
        }

        protected virtual void AddTheCurrentData()
        {
            UpdateData();
            Show(true);
        }

    }
}
