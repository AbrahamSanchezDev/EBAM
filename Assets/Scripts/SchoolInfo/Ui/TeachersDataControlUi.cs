using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UTS
{
    public class TeachersDataControlUi : BaseDataControlUi
    {
        public override void UpdateData()
        {
            base.UpdateData();
            var data = SchoolInfo.CurInfo;
            var prefabs = PrefabRefs.Instance;
            
            for (int i = 1; i < data.Teachers.Count; i++)
            {
                var cur = data.Teachers[i];
                var but = Instantiate(prefabs.ClassButton, _scroll.content);
                but.SetData(cur);
                var index = i;
                but.OnClick.AddListener(() => { EditDataOn(index); });
            }
        }

        #region EditContent


        protected override void EditDataOn(int index)
        {
            base.EditDataOn(index);
            _infoUi.SetEvents(CancelEdit, () => UpdateDataAt(index), () => DeleteAt(index));

            var data = SchoolInfo.CurInfo.Teachers[index];
            _infoUi._inputUi.SetDisplayText("Nombre de Maestro", data);
        }

        protected override void DeleteAt(int index)
        {
            base.DeleteAt(index);
            SchoolInfo.CurInfo.Teachers.RemoveAt(index);
            UpdateData();
            SchoolInfo.CurInfo.Save();

        }
        protected override void UpdateDataAt(int index) {
            SchoolInfo.CurInfo.Teachers[index]  = _infoUi._inputUi.GetText();

            Show(true);

            MainSetup.DisplayWindowAction.Invoke(DisplayWindowAction.UpdateData);
            SchoolInfo.CurInfo.Save();
            UpdateData();
        }

        #endregion

        public override void AddData()
        {
            base.AddData();
            _infoUi.SetEvents(CancelEdit, AddTheCurrentData);
            _infoUi._inputUi.SetDisplayText("Nombre de Maestro", "");
        }
        protected override void AddTheCurrentData()
        {
            if (_infoUi.GetInputText() == "") return;            
            SchoolInfo.CurInfo.AddTeacher(_infoUi.GetInputText());
            base.AddTheCurrentData();
        }
    }
}