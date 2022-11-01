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
        private TMP_Text _onDisplayText;
        private Slider _curHour;
        private WaitForSeconds _delay;
        private TMP_Text _nowText;

        protected void Awake()
        {
            Setup();
        }

        public void Setup()
        {
            if (_daysTransform) return;
            _delay = new WaitForSeconds(1);

            var topMenu = transform.GetChild(0);
            _dayText = transform.Find("Day").GetComponentInChildren<TMP_Text>();
            _onDisplayText = transform.Find("Display").GetComponentInChildren<TMP_Text>();
            _curHour = transform.Find("CurrentHour").GetComponentInChildren<Slider>();
            _nowText = transform.Find("Now").GetComponentInChildren<TMP_Text>();


            var mainArea = transform.Find("MainDisplayArea");

            _today = topMenu.transform.Find("Today").GetComponent<Button>();
            _week = topMenu.transform.Find("Week").GetComponent<Button>();
            _today.onClick.AddListener(ShowToday);
            _week.onClick.AddListener(ShowWeek);


            _daysTransform = mainArea;
        }

        private IEnumerator Start()
        {
            while (true)
            {
                var curHour = CurHour();
                float curMin = CurMin();

                var amPmHour = curHour;
                var pm = false;
                if (amPmHour > 12)
                {
                    amPmHour -= 12;
                    pm = true;
                }


                _nowText.text = amPmHour + ":" + curMin + (pm? " PM": " AM");
                //Clamp to the lowest hour
                if (curHour < 7)
                {
                    curHour = 7;
                }
                //Clap to the highest hour
                else if (curHour > 15)
                {
                    curHour = 15;
                }

                //turn minutes to decimal
                curMin /= 60;
                float finalHour = curHour + curMin;
                Debug.Log(finalHour);
                //Set the current hour to display it
                _curHour.value = finalHour;

                yield return _delay;
            }
        }

        private int CurHour()
        {
            var today = DateTime.Now;

            return today.Hour;
        }

        private int CurMin()
        {
            var today = DateTime.Now;

            return today.Minute;
        }

        protected void OnEnable()
        {
            InputManager.OnSwipe.AddListener(OnSwipe);
        }

        protected void OnDisable()
        {
            InputManager.OnSwipe.RemoveListener(OnSwipe);
        }

        private void OnSwipe(SwipeDirection direction)
        {
            switch (direction)
            {
                case SwipeDirection.None:
                    break;
                case SwipeDirection.Down:
                case SwipeDirection.Left:
                    IndexToShow--;
                    if (IndexToShow < 1)
                    {
                        IndexToShow = 1;
                    }

                    ShowSingleDay(IndexToShow);
                    break;
                case SwipeDirection.Up:
                case SwipeDirection.Right:
                    IndexToShow++;
                    if (IndexToShow > 5)
                    {
                        IndexToShow = 5;
                    }

                    ShowSingleDay(IndexToShow);
                    break;
            }
        }

        //Show the given day on the ui
        private void ShowSingleDay(int index)
        {
            IndexToShow = index;
            UpdateCurrentVisual();
        }

        private int IndexToShow = 0;

        private void UpdateTodaysData()
        {
            var today = DateTime.Today.DayOfWeek;

            IndexToShow = (int) today;

            SetDayText(TodayName((int) today));
        }

        private string TodayName(int index)
        {
            switch (index)
            {
                case 1:
                    return "Lunes";
                case 2:
                    return "Martes";
                case 3:
                    return "Miercoles";
                case 4:
                    return "Juevez";
                case 5:
                    return "Viernes";
            }

            return "Fin De Semana";
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
            //Make sure it displays the first day of the week
            if (IndexToShow == 0)
            {
                IndexToShow = 1;
            }
            //Make sure to display only the last day of the week
            else if (IndexToShow > 5)
            {
                IndexToShow = 1;
            }

            UpdateCurrentVisual();
        }

        private void UpdateCurrentVisual()
        {
            _onDisplayText.text = TodayName(IndexToShow);
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