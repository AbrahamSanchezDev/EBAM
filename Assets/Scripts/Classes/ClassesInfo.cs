using UnityEngine;

namespace UTS
{
    public class ClassesInfo : MonoBehaviour
    {
        public static ClassSchedule CurSchedule;

        public ClassSchedule Schedule;

        protected void Awake()
        {
        }

        public void Setup(HoursUi ui)
        {
            SchoolInfo.Load();
            CurSchedule = new ClassSchedule().Load();
            Schedule = CurSchedule;
            
            ui.UpdateData();
        }
    }
}