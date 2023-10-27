using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UTS
{
    public class ClassDataControlUi : BaseDataControlUi
    {
        public override void UpdateData()
        {
            base.UpdateData();
            var data = SchoolInfo.CurInfo;
            var prefabs = PrefabRefs.Instance;
            
            for (int i = 1; i < data.ClassInfo.Count; i++)
            {
                var cur = data.ClassInfo[i];
                var but = Instantiate(prefabs.ClassButton, _scroll.content);
                but.SetData(cur.TheName, cur.GetColor());
                var index = i;
                but.OnClick.AddListener(() => { EditDataOn(index); });
            }
        }

        #region EditContent


        protected override void EditDataOn(int index)
        {
            base.EditDataOn(index);
            _infoUi.SetEvents(CancelEdit, () => UpdateDataAt(index), () => DeleteAt(index));

            var data = SchoolInfo.CurInfo.ClassInfo[index];
            _infoUi._inputUi.SetDisplayText("Nombre de Materia", data.TheName);

            _infoUi.SetTheColor(data.GetColor());

        }

        protected override void DeleteAt(int index)
        {
            base.DeleteAt(index);
            SchoolInfo.CurInfo.ClassInfo.RemoveAt(index);
            UpdateData();
            SchoolInfo.CurInfo.Save();

        }
        protected override void UpdateDataAt(int index) {
            var data = SchoolInfo.CurInfo.ClassInfo[index];
            var curColor = _infoUi.GetColor();

            data.SetColor(curColor);

            data.TheName = _infoUi._inputUi.GetText();

            Show(true);

            MainSetup.DisplayWindowAction.Invoke(DisplayWindowAction.UpdateData);
            SchoolInfo.CurInfo.Save();
        }
      
        #endregion       
       
        public override void AddData()
        {
            base.AddData();
            _infoUi.SetEvents(CancelEdit, AddTheCurrentData);
            _infoUi._inputUi.SetDisplayText("Nombre de Materia", "");
            _infoUi.SetTheColor(Color.white);
        }
        protected override void AddTheCurrentData()
        {
            if (_infoUi.GetInputText() == "") return;
            var theNewData = new ClassData();
            theNewData.SetColor(_infoUi.GetColor());
            theNewData.TheName = _infoUi.GetInputText();
            SchoolInfo.CurInfo.AddClassInfo(theNewData);
            base.AddTheCurrentData();
        }
    }
}