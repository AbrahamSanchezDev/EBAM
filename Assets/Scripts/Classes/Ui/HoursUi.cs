using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UTS
{
    public class HoursUi : ScreenController
    {
        private GameObject _parentGo;

        private Button _today;
        private Button _week;
        private Button _settings;

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

            var mainParent = transform.GetChild(0);
            _parentGo = mainParent.gameObject;
            Show();

            _dayText = mainParent.Find("Day").GetComponentInChildren<TMP_Text>();
            _onDisplayText = mainParent.Find("Display").GetComponentInChildren<TMP_Text>();
            _curHour = mainParent.Find("CurrentHour").GetComponentInChildren<Slider>();
            _nowText = mainParent.Find("Now").GetComponentInChildren<TMP_Text>();


            var mainArea = mainParent.Find("MainDisplayArea");

            var topMenu = mainParent.GetChild(0);
            _today = topMenu.transform.Find("Today").GetComponent<Button>();
            _week = topMenu.transform.Find("Week").GetComponent<Button>();

            _settings = topMenu.transform.Find("Edit").GetComponent<Button>();
            var dbs = topMenu.transform.Find("Dbs").GetComponent<Button>();

            dbs.onClick.AddListener(ShowDbs);
            _today.onClick.AddListener(ShowToday);
            _week.onClick.AddListener(ShowWeek);
            _settings.onClick.AddListener(ShowSettings);


            _daysTransform = mainArea;

            //Show(false);
        }

        private void Show(bool show = true)
        {
            _parentGo.SetActive(show);
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

                string minsText = curMin.ToString();

                if (curMin < 10)
                {
                    minsText = "0" + curMin;
                }
                _nowText.text = amPmHour + ":" + minsText + (pm ? " PM" : " AM");
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
                //Debug.Log(finalHour);
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

        protected override void OnEnable()
        {
            base.OnEnable();
            InputManager.OnSwipe.AddListener(OnSwipe);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
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

            IndexToShow = (int)today;

            SetDayText(TodayName((int)today));
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

        public void ShowDbs()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Dbs");
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

        public void UpdateData()
        {
            Setup();

            allUi.Clear();

            var CurSchedule = ClassesInfo.CurSchedule;

            CurSchedule.FillDays();


            AddDay(1, CurSchedule.Day1);
            AddDay(2, CurSchedule.Day2);
            AddDay(3, CurSchedule.Day3);
            AddDay(4, CurSchedule.Day4);
            AddDay(5, CurSchedule.Day5);

            Initday();
        }

        private Transform _hoursParent;


        public void AddDay(int dayIndex, List<ClassInfo> info)
        {
            var prefabs = PrefabRefs.Instance;

            GameObject theParent = null;
            bool hadObj = false;

            if (DaysCalendars.ContainsKey(dayIndex))
            {
                theParent = DaysCalendars[dayIndex];
                hadObj = true;
            }

            if (theParent == null)
            {
                theParent = Instantiate(prefabs.HoursParent, _daysTransform); 
            }
         

            while(theParent.transform.childCount > 0)
            {
                DestroyImmediate(theParent.transform.GetChild(0).gameObject);
            }
            //TODO ordenar la info por inicio de clase y rellenar las horas faltantes
            //TODO calcular duracion de la clase basado en inicio y terminacion de clase
            for (var i = 0; i < info.Count; i++)
            {
                var theClassInfo = Instantiate(prefabs.TheHourUiDisplay, theParent.transform);
                theClassInfo.SetClassInfo(info[i]);
                allUi.Add(theClassInfo);
            }

            if (!hadObj)
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

        private void ShowSettings()
        {
            BackToPreviewsControl.ShowGoBack(BackToHours);
            MainSetup.ChangeToScreen.Invoke(ScreenName.EditarHorarios);
        }

        private void BackToHours()
        {
            MainSetup.ChangeToScreen.Invoke(ScreenName.Horarios);
        }

        protected override void OnShowScreen(ScreenName theScreen)
        {
            Show(theScreen == ScreenName.Horarios);
        }

        protected override void OnDisplayAction(DisplayWindowAction theAction)
        {
            if(theAction == DisplayWindowAction.UpdateData)
            {
                UpdateData();
            }
        }
    }
}