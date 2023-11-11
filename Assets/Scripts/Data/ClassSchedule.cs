using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UTS
{
    [System.Serializable]
    public class ClassHours
    {
        public string Hora;
    }
    public enum HourByName
    {
        at700am = 0,
        at800am = 1,
        at850am = 2,
        at940am = 3,
        at1030am = 4,
        at1120am = 5,
        at1210pm = 6,
        at100pm = 7,
        at150pm = 8,
        at240pm = 9,
        at330pm = 10
    }
    public enum DaysByNam
    {
        Domingo,
        Lunes,
        Martes,
        Miercoles,
        Jueves,
        Viernes,
        Sabado
    }
    [System.Serializable]
    public class ClassSchedule
    {
        public List<ClassInfo> Day1 = new List<ClassInfo>();
        public List<ClassInfo> Day2 = new List<ClassInfo>();
        public List<ClassInfo> Day3 = new List<ClassInfo>();
        public List<ClassInfo> Day4 = new List<ClassInfo>();
        public List<ClassInfo> Day5 = new List<ClassInfo>();

        public List<ClassInfo> AllDays = new List<ClassInfo>();


        public void FillDays()
        {
            if (AllDays.Count == 0) {
                Debug.Log("NO DATA ON ALL DAYS");
                return;
                    };

            //Remove empty slots
            for (int i = 0; i < AllDays.Count; i++)
            {
                if (AllDays[i].ClassId ==  0 && AllDays[i].ClassRoomId == 0 && AllDays[i].TeacherId == 0)
                {
                    AllDays.RemoveAt(i);
                    i--;
                }
            }

            AllDays.Sort((p1, p2) => p1.Day.CompareTo(p2.Day));

            Day1 = FillList(1);
            Day2 = FillList(2);
            Day3 = FillList(3);
            Day4 = FillList(4);
            Day5 = FillList(5);

            //Index them all
            for (int i = 0; i < AllDays.Count; i++)
            {
                AllDays[i].Index = i;
            }
            Debug.Log("SORTING!");
        }

        private List<ClassInfo> FillList(int day)
        {
            var theDayData = new List<ClassInfo>();
            //AddExisting
            for (int i = 0; i < AllDays.Count; i++)
            {
                if(AllDays[i].Day == day)
                {
                    theDayData.Add(AllDays[i]);
                }
            }
            int maxHours = 10;
            // Fill empty slots
            for (int i = 0; i < maxHours; i++)
            {
                if (!HasTimeInList(theDayData, i))
                {
                    theDayData.Add(new ClassInfo(0, 0, 0, (HourByName)i, (HourByName)i + 1, (DaysByNam)day));
                    //Debug.Log("Adding hour " + i + " to day: " + day);
                }
            }
            // Order them by the hour
            theDayData.Sort((p1, p2) => p1.StartHour.CompareTo(p2.EndHour));

            return theDayData;

        }
        private bool HasTimeInList(List<ClassInfo> data,int time)
        {

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].HasTimeOnIt(time))
                {
                    return true;
                }

            }
            return false;
        }
        public List<string> GetHoursText()
        {
            var list = new List<string>();
      
            for (int i = 0; i < Hours.Length; i++)
            {
                list.Add(Hours[i].Hora);
            }
            return list;
        }
        public static ClassHours[] Hours =
            Hours = new ClassHours[]
            {
                new ClassHours{Hora = "7:00 am"},
                new ClassHours{Hora = "8:00 am"},
                new ClassHours{Hora = "8:50 am"},
                new ClassHours{Hora = "9:40 am"},
                new ClassHours{Hora = "10:30 am"},
                new ClassHours{Hora = "11:20 am"},
                new ClassHours{Hora = "12:10 pm"},
                new ClassHours{Hora = "1:00 pm"},
                new ClassHours{Hora = "1:50 pm"},
                new ClassHours{Hora = "2:40 pm"},
                new ClassHours{Hora = "3:30 pm"}
            };

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

            return rootPath + "SaveData/MyClasses.data";
        }

        public ClassSchedule Load()
        {
            if (!AlreadyExists())
            {
                Debug.Log("DIDNT EXISTS");
                CreateTemplate();
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
                Debug.Log("FILE ALREADY EXISTS");
                return;
            }

            AllDays.Clear();
            //Lunes
            AllDays.Add(new ClassInfo(1, 1, 0, HourByName.at700am, HourByName.at800am, DaysByNam.Lunes));
            AllDays.Add(new ClassInfo(2, 1, 5, HourByName.at800am, HourByName.at1030am, DaysByNam.Lunes));
            //Cual es el salon de FTIS el lunes?
            AllDays.Add(new ClassInfo(3, 2, 0, HourByName.at1120am, HourByName.at100pm, DaysByNam.Lunes));
            AllDays.Add(new ClassInfo(4, 3, 0, HourByName.at150pm, HourByName.at330pm, DaysByNam.Lunes));

            //Martes
            //Cual es el numero de laboratoro de ingles?
            AllDays.Add(new ClassInfo(1, 1, 0, HourByName.at700am, HourByName.at800am, DaysByNam.Martes));
            AllDays.Add(new ClassInfo(2, 2, 5, HourByName.at800am, HourByName.at940am, DaysByNam.Martes));
            AllDays.Add(new ClassInfo(5, 4, 0, HourByName.at940am, HourByName.at1120am, DaysByNam.Martes));
            AllDays.Add(new ClassInfo(6, 5, 5, HourByName.at1210pm, HourByName.at150pm, DaysByNam.Martes));
            //Cual es el numero de laboratoro de FTIS?
            AllDays.Add(new ClassInfo(3, 2, 0, HourByName.at150pm, HourByName.at240pm, DaysByNam.Martes));

            //Miercoles
            AllDays.Add(new ClassInfo(1, 1, 0, HourByName.at700am, HourByName.at800am, DaysByNam.Miercoles));
            AllDays.Add(new ClassInfo(6, 5, 5, HourByName.at800am, HourByName.at1030am, DaysByNam.Miercoles));

            AllDays.Add(new ClassInfo(5, 4, 0, HourByName.at1120am, HourByName.at100pm, DaysByNam.Miercoles));
            AllDays.Add(new ClassInfo(7, 2, 0, HourByName.at100pm, HourByName.at150pm, DaysByNam.Miercoles));
            AllDays.Add(new ClassInfo(4, 3, 0, HourByName.at150pm, HourByName.at330pm, DaysByNam.Miercoles));

            //Jueves
            AllDays.Add(new ClassInfo(1, 1, 0, HourByName.at700am, HourByName.at800am, DaysByNam.Jueves));

            AllDays.Add(new ClassInfo(8, 6, 0, HourByName.at800am, HourByName.at850am, DaysByNam.Jueves));

            AllDays.Add(new ClassInfo(5, 4, 0, HourByName.at940am, HourByName.at1120am, DaysByNam.Jueves));

            AllDays.Add(new ClassInfo(7, 2, 0, HourByName.at1210pm, HourByName.at100pm, DaysByNam.Jueves));
            AllDays.Add(new ClassInfo(4, 3, 0, HourByName.at100pm, HourByName.at240pm, DaysByNam.Jueves));

            //Viernes

            AllDays.Add(new ClassInfo(8, 6, 0, HourByName.at940am, HourByName.at1120am, DaysByNam.Viernes));
            AllDays.Add(new ClassInfo(3, 2, 5, HourByName.at1120am, HourByName.at100pm, DaysByNam.Viernes));
            AllDays.Add(new ClassInfo(9, 3, 0, HourByName.at150pm, HourByName.at330pm, DaysByNam.Viernes));


            FillDays();
            Debug.LogWarning("Update the class info");
            Save();
        }

        public void Save()
        {
            SaveLoadData<ClassSchedule>.Save(this, SaveFileName);
            //var theJsonText = JsonUtility.ToJson(this, true);
            //File.WriteAllText(FilePath(), theJsonText);

            //Debug.Log("Created Classes");
        }
    }
}