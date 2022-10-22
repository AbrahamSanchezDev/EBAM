using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UTS
{
    public class HoursUi : MonoBehaviour
    {
        private Button _today;
        private Button _week;

        private Transform _daysTransform;

        private TMP_Text _dayText;

        protected void Awake()
        {
            Setup();
        }

        public void Setup()
        {
            if (_daysTransform) return;
            var topMenu = transform.GetChild(0);
            _dayText = transform.GetChild(1).GetComponentInChildren<TMP_Text>();
            var mainArea = transform.GetChild(2);

            _today = topMenu.transform.Find("Today").GetComponent<Button>();
            _week = topMenu.transform.Find("Week").GetComponent<Button>();
            _today.onClick.AddListener(ShowToday);
            _week.onClick.AddListener(ShowWeek);


            _daysTransform = mainArea;
        }


        private int IndexToShow = 0;

        private void UpdateTodaysData()
        {
            var today = DateTime.Today.DayOfWeek;

            IndexToShow = 0;
            switch (today)
            {
                case DayOfWeek.Monday:
                    SetDayText("Lunes");
                    IndexToShow = 1;
                    break;
                case DayOfWeek.Tuesday:
                    SetDayText("Martes");
                    IndexToShow = 2;
                    break;
                case DayOfWeek.Wednesday:
                    SetDayText("Miercoles");
                    IndexToShow = 3;
                    break;
                case DayOfWeek.Thursday:
                    SetDayText("Jueves");
                    IndexToShow = 4;
                    break;
                case DayOfWeek.Friday:
                    SetDayText("Viernes");
                    IndexToShow = 5;
                    break;
                case DayOfWeek.Saturday:
                    SetDayText("Fin De Semana");
                    IndexToShow = 1;
                    break;
                case DayOfWeek.Sunday:
                    SetDayText("Fin De Semana");
                    IndexToShow = 1;
                    break;
            }
        }

        private void SetDayText(string theDay)
        {
            if (_dayText)
            {
                var today = DateTime.Today;

                _dayText.text = theDay + "       " + today.Day + "/" + today.Month + "/" + today.Year;
            }
            else
            {
                Debug.Log("No Day text");
            }
        }

        public void ShowToday()
        {
            UpdateTodaysData();
            foreach (var day in DaysCalendars) day.Value.SetActive(IndexToShow == day.Key);
        }

        private void ShowWeek()
        {
            UpdateTodaysData();
            foreach (var day in DaysCalendars) day.Value.SetActive(true);
        }

        private Dictionary<int, GameObject> DaysCalendars = new Dictionary<int, GameObject>();
        private List<HourUiDisplay> allUi = new List<HourUiDisplay>();

        public void AddDay(int dayIndex, List<ClassInfo> info)
        {
            var prefabs = PrefabRefs.Instance;

            var theParent = Instantiate(prefabs.HoursParent, _daysTransform);


            for (var i = 0; i < info.Count; i++)
            {
                var theClassInfo = Instantiate(prefabs.TheHourUiDisplay, theParent.transform);
                theClassInfo.SetClassInfo(info[i]);
                allUi.Add(theClassInfo);
            }


            DaysCalendars.Add(dayIndex, theParent.gameObject);
          
        }

        public void Initday()
        {
            StartCoroutine("UpdateTheSizeCo");
        }

        private IEnumerator UpdateTheSizeCo()
        {
            yield return null;
            for (var i = 0; i < allUi.Count; i++) allUi[i].UpdateSize();
            ShowToday();
        }
    }
}