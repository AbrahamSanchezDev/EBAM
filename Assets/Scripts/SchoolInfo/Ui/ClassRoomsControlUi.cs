using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UTS
{
    public class ClassRoomsControlUi : BaseDataControlUi
    {
        public override void UpdateData()
        {
            base.UpdateData();
            var data = SchoolInfo.CurInfo;
            var prefabs = PrefabRefs.Instance;

            var buts = new List<ClassDataButton>();

            for (int i = 1; i < data.ClassRooms.Count; i++)
            {
                var cur = data.ClassRooms[i];
                var but = Instantiate(prefabs.ClassButton, _scroll.content);
                but.SetData(cur.Name);
                var index = i;
                but.OnClick.AddListener(() => { EditDataOn(index); });
                buts.Add(but);
            }
            OrderButtons(buts);
        }

        #region EditContent


        protected override void EditDataOn(int index)
        {
            base.EditDataOn(index);
            _infoUi.SetEvents(CancelEdit, () => UpdateDataAt(index), () => DeleteAt(index));

            var data = SchoolInfo.CurInfo.ClassRooms[index];
            _infoUi._inputUi.SetDisplayText("Nombre de Salon", data.Name);

        }

        protected override void DeleteAt(int index)
        {
            base.DeleteAt(index);
            SchoolInfo.CurInfo.ClassRooms.RemoveAt(index);
            SchoolInfo.CurInfo.Save();
            UpdateData();

        }
        protected override void UpdateDataAt(int index)
        {
            var data = SchoolInfo.CurInfo.ClassRooms[index];

            data.Name = _infoUi._inputUi.GetText();

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
            _infoUi._inputUi.SetDisplayText("Nombre de Salon", "");
        }
        protected override void AddTheCurrentData()
        {
            if (_infoUi.GetInputText() == "") return;

            var theNewData = new RoomInfo(_infoUi.GetInputText());

            SchoolInfo.CurInfo.AddRoomInfo(theNewData);
            base.AddTheCurrentData();
        }
    }
}