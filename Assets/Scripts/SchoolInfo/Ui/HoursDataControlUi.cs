using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UTS
{
    public class HoursDataControlUi : BaseDataControlUi
    {

        protected AddHoursUi _hoursUi;
        private DaysUiControl _dayGo;

        public override void Setup(Transform theParent)
        {
            if (_scroll) return;
            var prefabs = PrefabRefs.Instance;
            _scroll = Instantiate(prefabs.DisplayHoursScrollView, theParent);
            _mainGo = transform.Find("ParentObj").gameObject;
            _infoUi = AddInfoUi.Instance;
            _hoursUi = AddHoursUi.Instance;

            _dayGo = Instantiate(prefabs.DaysContainer, _mainGo.transform);
            _dayGo.OnDayChange.AddListener(ShowDay);
            UpdateData();
        }

        public override void ShowMainUi(bool show)
        {
            base.ShowMainUi(show);
            _dayGo?.gameObject.SetActive(show);
        }

        public override void UpdateData()
        {
            base.UpdateData();
            var data = ClassesInfo.CurSchedule;
            data.FillDays();
            ShowDay(_currentDay);
        }

        private int _currentDay = 1;
        private List<ClassInfo> curDay;
        private string _dayText;

        private void UpdateCurDay(int day)
        {
            _currentDay = day;
            var data = ClassesInfo.CurSchedule;
            curDay = null;
            switch (day)
            {
                case 1:
                    curDay = data.Day1;
                    _dayText = "Lunes";
                    break;
                case 2:
                    curDay = data.Day2;
                    _dayText = "Martes";
                    break;
                case 3:
                    curDay = data.Day3;
                    _dayText = "Miercoles";
                    break;
                case 4:
                    curDay = data.Day4;
                    _dayText = "Juevez";
                    break;
                case 5:
                    curDay = data.Day5;
                    _dayText = "Viernes";
                    break;

                default:
                    Debug.Log("DAY NOT FOUND " + day);
                    break;
            }

        }
        private void ShowDay(int day)
        {

            UpdateCurDay(day);

            RemoveOldcontent();

            if (curDay == null) return;


            var prefabs = PrefabRefs.Instance;
            for (int i = 0; i < curDay.Count; i++)
            {
                var cur = curDay[i];
                var but = Instantiate(prefabs.HourDisplay, _scroll.content);
                if (but.SetClassInfo(cur))
                {
                    var index = cur.Index;

                    but.SetClickable(() => { EditDataOn(index); });
                    but.UpdateHeight();
                }
            }
            //_hoursUi = AddHoursUi.Instance;
            //_hoursUi.SetDay(curDay);
        }

        #region EditContent


        protected override void EditDataOn(int index)
        {
            base.EditDataOn(index);
            _infoUi.Show(false);

            _hoursUi.Show(true);
            _hoursUi.SetEvents(CancelEdit, () => UpdateDataAt(index), () => DeleteAt(index));

            var data = ClassesInfo.CurSchedule;
            for (int i = 0; i < data.AllDays.Count; i++)
            {
                if (data.AllDays[i].Index == index)
                {
                    _hoursUi.SetupData(data.AllDays[i]);
                    break;
                }
            }
            UpdateData();
            data.Save();

        }

        protected override void DeleteAt(int index)
        {
            base.DeleteAt(index);

            var data = ClassesInfo.CurSchedule;
            for (int i = 0; i < data.AllDays.Count; i++)
            {
                if (data.AllDays[i].Index == index)
                {
                    data.AllDays.RemoveAt(i);
                    break;
                }
            }
            UpdateData();
            data.Save();

        }
        protected override void UpdateDataAt(int index)
        {

            var data = ClassesInfo.CurSchedule;

            for (int i = 0; i < data.AllDays.Count; i++)
            {
                if (data.AllDays[i].Index == index)
                {
                    data.AllDays[i] = _hoursUi.CurInfo();
                    break;
                }

            }


            Show(true);

            MainSetup.DisplayWindowAction.Invoke(DisplayWindowAction.UpdateData);
            data.Save();
        }

        #endregion

        protected override void StartAddUi()
        {
            Show(false);
            if (_hoursUi)
                _hoursUi = AddHoursUi.Instance;
            if (_hoursUi == null)
            {
                Debug.Log("NO INSTANCE!");
            }
            //_hoursUi.HideAllInputs();
            _hoursUi.Show(true);
        }
        public override void AddData()
        {
            base.AddData();
            _hoursUi.SetEvents(CancelEdit, AddTheCurrentData);
            _hoursUi.SetupData(null);
        }
        protected override void AddTheCurrentData()
        {
            //if (_infoUi.GetInputText() == "") return;            

            var data = ClassesInfo.CurSchedule;
            data.AllDays.Add(_hoursUi.CurInfo());
            data.Save();
            base.AddTheCurrentData();


        }
    }
}