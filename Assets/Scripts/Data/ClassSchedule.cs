using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UTS
{
    [System.Serializable]
    public class ClassSchedule
    {
        public List<ClassInfo> Day1 = new List<ClassInfo>();
        public List<ClassInfo> Day2 = new List<ClassInfo>();
        public List<ClassInfo> Day3 = new List<ClassInfo>();
        public List<ClassInfo> Day4 = new List<ClassInfo>();
        public List<ClassInfo> Day5 = new List<ClassInfo>();

        //Weekend
        public List<ClassInfo> Day6;
        public List<ClassInfo> Day7;

        public ClassSchedule Load()
        {
            var path = Application.persistentDataPath + "/MyClasses.json";
            var theText = File.ReadAllText(path);
            var theData = JsonUtility.FromJson<ClassSchedule>(theText);
            return theData;
        }
    }
}