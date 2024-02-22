using UnityEngine;
using UnityEngine.Events;

namespace UTS
{
    public enum ScreenName
    {
        None,
        Horarios,
        EditarHorarios
    }
    public enum DisplayWindowAction
    {
        None,
        UpdateData
    }
    public class ChangeToScreenEvent : UnityEvent<ScreenName> { }
    public class DisplayWindowActionEvent : UnityEvent<DisplayWindowAction> { }
    public class MainSetup : MonoBehaviour
    {
        public HoursUi HoursUi;

        public static ChangeToScreenEvent ChangeToScreen = new ChangeToScreenEvent();
        public static DisplayWindowActionEvent DisplayWindowAction = new DisplayWindowActionEvent();


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
            //Debug.Log("CreateSchoolInfo");
        }

        private void CreateMyClasses()
        {
            var classes = new ClassSchedule();

            if (!ClassSchedule.AlreadyExists())
            {
                classes.CreateTemplate();
                //Debug.Log("Created Template data");
            }
        }

        private void LoadMyClasses()
        {
            var classInfo = gameObject.AddComponent<ClassesInfo>();
            classInfo.Setup(HoursUi);
        }
    }
}