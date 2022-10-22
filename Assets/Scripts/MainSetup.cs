using System.IO;
using UnityEngine;

namespace UTS
{
    public class MainSetup : MonoBehaviour
    {
        public HoursUi HoursUi;


        protected void Awake()
        {
            Setup();
        }

        protected void Setup()
        {
            CreateSchoolInfo();
            CreateMyClasses();
            LoadMyClasses();
        }

        private void CreateSchoolInfo()
        {
            var info = new SchoolInfo();
            info.CreateTemplateData();
        }

        private void CreateMyClasses()
        {
            var classes = new ClassSchedule();
            //Lunes
            classes.Day1.Add(new ClassInfo(1, 1, 0, 1));
            classes.Day1.Add(new ClassInfo(2, 0, 5, 3,"8:00am - 10:30am"));
            classes.Day1.Add(new ClassInfo(0, 0, 0, 1));
            //Cual es el salon de FTIS el lunes?
            classes.Day1.Add(new ClassInfo(3, 2, 0, 2, "11:20am - 1:00pm"));
            classes.Day1.Add(new ClassInfo(0, 0, 0, 1));
            classes.Day1.Add(new ClassInfo(4, 3, 0, 2, "1:50pm - 3:30pm"));

            //Martes
            //Cual es el numero de laboratoro de ingles?
            classes.Day2.Add(new ClassInfo(1, 1, 0, 1));
            classes.Day2.Add(new ClassInfo(2, 2, 5, 2, "8:00am - 9:40am"));
            classes.Day2.Add(new ClassInfo(5, 4, 0, 2, "9:40am - 11:20am"));
            classes.Day2.Add(new ClassInfo(0, 0, 0, 1));
            classes.Day2.Add(new ClassInfo(6, 5, 5, 2, "12:10pm - 1:50pm"));
            //Cual es el numero de laboratoro de FTIS?
            classes.Day2.Add(new ClassInfo(3, 2, 0, 1, "1:50pm - 2:40pm"));

            //Miercoles
            classes.Day3.Add(new ClassInfo(1, 1, 0, 1));
            classes.Day3.Add(new ClassInfo(6, 5, 5, 3, "8:00am - 10:30am"));
            classes.Day3.Add(new ClassInfo(0, 0, 0, 1));
            classes.Day3.Add(new ClassInfo(5, 4, 0, 2, "11:20am - 1:00pm"));
            classes.Day3.Add(new ClassInfo(7, 2, 0, 1, "1:00pm - 1:50pm"));
            classes.Day3.Add(new ClassInfo(4, 3, 0, 2, "1:50pm - 3:30pm"));

            //Jueves
            classes.Day4.Add(new ClassInfo(1, 1, 0, 1));
            classes.Day4.Add(new ClassInfo(8, 6, 0, 1, "8:00am - 8:50am"));
            classes.Day4.Add(new ClassInfo(0, 0, 0, 1));
            classes.Day4.Add(new ClassInfo(5, 4, 0, 2, "9:40am - 11:20am"));
            classes.Day4.Add(new ClassInfo(0, 0, 0, 1));
            classes.Day4.Add(new ClassInfo(7, 2, 0, 1, "12:10pm - 1:00pm"));
            classes.Day4.Add(new ClassInfo(4, 3, 0, 2));
            classes.Day4.Add(new ClassInfo(0, 0, 0, 1));

            //Viernes
            classes.Day5.Add(new ClassInfo(0, 0, 0, 3));
            classes.Day5.Add(new ClassInfo(8, 6, 0, 2, "9:40am - 11:20am"));
            classes.Day5.Add(new ClassInfo(3, 2, 5, 2, "11:20am - 1:00pm"));
            classes.Day5.Add(new ClassInfo(0, 0, 0, 1));
            classes.Day5.Add(new ClassInfo(9, 3, 0, 2, "1:50pm - 3:30pm"));


            var theJsonText = JsonUtility.ToJson(classes, true);

            var rootPath = Application.persistentDataPath;
            if (Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }
            File.WriteAllText(rootPath + "/MyClasses.json", theJsonText);
            //Debug.Log("Created Classes");
        }

        private void LoadMyClasses()
        {
            var classInfo = gameObject.AddComponent<ClassesInfo>();
            classInfo.Setup(HoursUi);
        }
    }
}