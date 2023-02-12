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
            AddCom<InputManager>();


            CreateSchoolInfo();
            CreateMyClasses();
            LoadMyClasses();
        }

        private T AddCom<T>() where T : MonoBehaviour
        {
            var com = GetComponent<T>();
            if (com == null) com = gameObject.AddComponent<T>();

            return com;
        }

        private void CreateSchoolInfo()
        {
            var info = new SchoolInfo();
            info.CreateTemplateData();
        }

        private void CreateMyClasses()
        {
            var classes = new ClassSchedule();

            if (!ClassSchedule.AlreadyExists()) classes.CreateTemplate();
        }

        private void LoadMyClasses()
        {
            var classInfo = gameObject.AddComponent<ClassesInfo>();
            classInfo.Setup(HoursUi);
        }
    }
}