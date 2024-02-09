using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace UTS
{
    public class DaysUiControl : MonoBehaviour
    {
        public TextMeshProUGUI TheDay;
        public UnityEvent<int> OnDayChange = new UnityEvent<int>();

        protected void Awake()
        {
            Setup();
        }
        public void Setup() {
            if (TheDay) return;
            TheDay = transform.Find("Dia").GetComponentInChildren<TextMeshProUGUI>();


            var previews = transform.Find("Previews").gameObject.GetComponent<Button>();
            previews.onClick.AddListener(Previews);

            var next = transform.Find("Next").gameObject.GetComponent<Button>();
            next.onClick.AddListener(Next);
        }

        private int _maxDay = 7;
        private int _minDay = 0;
        [HideInInspector]
        public int CurDay = 1;

        private void Next()
        {
            CurDay++;
            if (CurDay >= _maxDay)
                CurDay = _minDay;
            OnDayChange?.Invoke(CurDay);
            SetDay(CurDay);
        }
       
        private void Previews()
        {
            CurDay--;
            if (CurDay < _minDay)
                CurDay = _maxDay;
            OnDayChange?.Invoke(CurDay);
            SetDay(CurDay);
        }
        private void SetDay(int theDay)
        {
            Setup();
            TheDay.text = ClassInfo.TheDay(theDay);
            CurDay = theDay;
        }

    }
}