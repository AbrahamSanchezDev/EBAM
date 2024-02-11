using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTS
{
    [System.Serializable]
    public class DBInfo
    {
        public string Name;
        public string Creator;

        public ClassSchedule TheClassSchedule;
        public SchoolInfo TheSchoolInfo;

        public void LoadLocal()
        {
            TheClassSchedule = TheClassSchedule.Load();
            TheSchoolInfo = SchoolInfo.Load();
        }

        public void SaveToLocal()
        {
            TheClassSchedule.Save();
            TheSchoolInfo.Save();
        }
    }
}