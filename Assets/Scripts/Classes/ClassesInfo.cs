using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTS
{
    public class ClassesInfo : MonoBehaviour
    {
        public static ClassSchedule CurSchedule;


        protected void Awake()
        {
        }

        public void Setup(HoursUi ui)
        {
            SchoolInfo.Load();
            CurSchedule = new ClassSchedule().Load();

            ui.Setup();
            ui.AddDay(1, CurSchedule.Day1);
            ui.AddDay(2, CurSchedule.Day2);
            ui.AddDay(3, CurSchedule.Day3);
            ui.AddDay(4, CurSchedule.Day4);
            ui.AddDay(5, CurSchedule.Day5);

            ui.Initday();
        }
    }
}