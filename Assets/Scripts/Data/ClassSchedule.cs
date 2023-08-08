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

        //private static string SaveFileName = "MyClasses.json";
        private static string SaveFileName = "MyClasses";
        public static bool AlreadyExists()
        {
            return SaveLoadData<ClassSchedule>.FileExistsOn(SaveFileName);
            //return File.Exists(FilePath());
        }


        private static string FilePath()
        {
            var rootPath = Application.persistentDataPath;
            if (Directory.Exists(rootPath)) Directory.CreateDirectory(rootPath);

            return rootPath + "/MyClasses.json";
        }

        public ClassSchedule Load()
        {
            if (!AlreadyExists())
            {
                Save();
                return this;
            }
            return SaveLoadData<ClassSchedule>.Load(SaveFileName);
            //var path = Application.persistentDataPath + "/MyClasses.json";
            //var theText = File.ReadAllText(path);
            //var theData = JsonUtility.FromJson<ClassSchedule>(theText);
            //return theData;

        }

        public void CreateTemplate()
        {
            if (File.Exists(FilePath()))
            {
                return;
            }
            //Lunes
            Day1.Add(new ClassInfo(1, 1, 0, 1));
            Day1.Add(new ClassInfo(2, 0, 5, 3, "8:00am - 10:30am"));
            Day1.Add(new ClassInfo(0, 0, 0, 1));
            //Cual es el salon de FTIS el lunes?
            Day1.Add(new ClassInfo(3, 2, 0, 2, "11:20am - 1:00pm"));
            Day1.Add(new ClassInfo(0, 0, 0, 1));
            Day1.Add(new ClassInfo(4, 3, 0, 2, "1:50pm - 3:30pm"));

            //Martes
            //Cual es el numero de laboratoro de ingles?
            Day2.Add(new ClassInfo(1, 1, 0, 1));
            Day2.Add(new ClassInfo(2, 2, 5, 2, "8:00am - 9:40am"));
            Day2.Add(new ClassInfo(5, 4, 0, 2, "9:40am - 11:20am"));
            Day2.Add(new ClassInfo(0, 0, 0, 1));
            Day2.Add(new ClassInfo(6, 5, 5, 2, "12:10pm - 1:50pm"));
            //Cual es el numero de laboratoro de FTIS?
            Day2.Add(new ClassInfo(3, 2, 0, 1, "1:50pm - 2:40pm"));

            //Miercoles
            Day3.Add(new ClassInfo(1, 1, 0, 1));
            Day3.Add(new ClassInfo(6, 5, 5, 3, "8:00am - 10:30am"));
            Day3.Add(new ClassInfo(0, 0, 0, 1));
            Day3.Add(new ClassInfo(5, 4, 0, 2, "11:20am - 1:00pm"));
            Day3.Add(new ClassInfo(7, 2, 0, 1, "1:00pm - 1:50pm"));
            Day3.Add(new ClassInfo(4, 3, 0, 2, "1:50pm - 3:30pm"));

            //Jueves
            Day4.Add(new ClassInfo(1, 1, 0, 1));
            Day4.Add(new ClassInfo(8, 6, 0, 1, "8:00am - 8:50am"));
            Day4.Add(new ClassInfo(0, 0, 0, 1));
            Day4.Add(new ClassInfo(5, 4, 0, 2, "9:40am - 11:20am"));
            Day4.Add(new ClassInfo(0, 0, 0, 1));
            Day4.Add(new ClassInfo(7, 2, 0, 1, "12:10pm - 1:00pm"));
            Day4.Add(new ClassInfo(4, 3, 0, 2));
            Day4.Add(new ClassInfo(0, 0, 0, 1));

            //Viernes
            Day5.Add(new ClassInfo(0, 0, 0, 3));
            Day5.Add(new ClassInfo(8, 6, 0, 2, "9:40am - 11:20am"));
            Day5.Add(new ClassInfo(3, 2, 5, 2, "11:20am - 1:00pm"));
            Day5.Add(new ClassInfo(0, 0, 0, 1));
            Day5.Add(new ClassInfo(9, 3, 0, 2, "1:50pm - 3:30pm"));
            Save();
        }

        public void Save()
        {
            SaveLoadData<ClassSchedule>.Save(this, SaveFileName);
            //var theJsonText = JsonUtility.ToJson(this, true);
            //File.WriteAllText(FilePath(), theJsonText);

            Debug.Log("Created Classes");
        }
    }
}