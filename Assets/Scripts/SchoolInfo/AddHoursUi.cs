using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace UTS
{
    public class AddHoursUi : ScreenController
    {
        private GameObject _parentGo;

        //public InputUiObj Description;

        public DropDownInputUiObj Materia, Salon, Maestro, Inicio, Termina;

        public TextMeshProUGUI TheDay;

        private GameObject _cancelGo, _saveGo, _deleteGo;


        public static UnityAction OnDelete, OnSave, OnCancel;

        public static AddHoursUi Instance;

        protected void Awake()
        {
            Instance = this;
            Setup();
        }

        protected void Setup()
        {
            if (_parentGo != null) return;
            _parentGo = transform.Find("ParentObj").gameObject;

            var buts = _parentGo.transform.Find("Buttons");

            var cancel = buts.Find("Cancel").GetComponent<Button>();
            var save = buts.Find("Save").GetComponent<Button>();
            var delete = buts.Find("Delete").GetComponent<Button>();

            _cancelGo = cancel.gameObject;
            _saveGo = save.gameObject;
            _deleteGo = delete.gameObject;

            cancel.onClick.AddListener(DoCancel);
            save.onClick.AddListener(DoSave);
            delete.onClick.AddListener(DoDelete);


            var content = _parentGo.transform.Find("Content");

            var desc = content.Find("Desc").gameObject;
            var prefabs = PrefabRefs.Instance;
            var materia = Instantiate(prefabs.dropDownInputUi, content);
            var salon = Instantiate(prefabs.dropDownInputUi, content);
            var maestro = Instantiate(prefabs.dropDownInputUi, content);
            var inicio = Instantiate(prefabs.dropDownInputUi, content);
            var termina = Instantiate(prefabs.dropDownInputUi, content);

            desc.transform.SetSiblingIndex(1);

            materia.gameObject.name = "Materia";
            salon.gameObject.name = "Salon";
            maestro.gameObject.name = "Maestro";
            inicio.gameObject.name = "Inicio";
            termina.gameObject.name = "Termina";



            //Description = desc.GetComponent<InputUiObj>();

            Materia = materia.GetComponent<DropDownInputUiObj>();
            Salon = salon.GetComponent<DropDownInputUiObj>();
            Maestro = maestro.GetComponent<DropDownInputUiObj>();
            Inicio = inicio.GetComponent<DropDownInputUiObj>();
            Termina = termina.GetComponent<DropDownInputUiObj>();

            //Description.SetDisplayText("Descripcion");
            Materia.SetDisplayText("Material");
            Salon.SetDisplayText("Salon");
            Maestro.SetDisplayText("Maestro");
            Inicio.SetDisplayText("Inicia");
            Termina.SetDisplayText("Termina");

            TheDay = _parentGo.transform.Find("Dia").GetComponentInChildren<TextMeshProUGUI>();

            var previews = _parentGo.transform.Find("Previews").gameObject.GetComponent<Button>();
            previews.onClick.AddListener(Previews);

            var next = _parentGo.transform.Find("Next").gameObject.GetComponent<Button>();
            next.onClick.AddListener(Next);


            Show(false);
        }

        public ClassInfo CurInfo()
        {
            return new ClassInfo(Materia.GetPosition(),Maestro.GetPosition(),Salon.GetPosition(),(HourByName)Inicio.GetPosition(),(HourByName)Termina.GetPosition(),(DaysByNam) CurDay);
        }

        private void Next()
        {
            CurDay++;
            if (CurDay >= _maxDay)
                CurDay = _minDay;
            SetDay(CurDay);
        }
        private int _maxDay = 7;
        private int _minDay = 0;
        private void Previews()
        {
            CurDay--;
            if (CurDay < _minDay)
                CurDay = _maxDay;
            SetDay(CurDay);
        }

        public void Show(bool show)
        {
            _parentGo.SetActive(show);
        } 

        public void SetupData(ClassInfo data)
        {
            if(data == null)
            {
                data = new ClassInfo(1, 1, 1, HourByName.at700am, HourByName.at800am,DaysByNam.Lunes);
            }
            //Description.SetText(data.Description);
            var info = SchoolInfo.CurInfo;
            var classes = ClassesInfo.CurSchedule;

            //Set the options to display
            Materia.SetOptions(info.ClassInfoNames());
            Maestro.SetOptions(info.Teachers);
            Salon.SetOptions(info.ClassRoomsNames());

            Inicio.SetOptions(classes.GetHoursText());
            Termina.SetOptions(classes.GetHoursText());
            // Set the current value
            Materia.SetCurrentOption(data.ClassId);
            Maestro.SetCurrentOption(data.TeacherId);
            Salon.SetCurrentOption(data.ClassRoomId);

            Inicio.SetCurrentOption(data.StartHour);
            Termina.SetCurrentOption(data.EndHour);

            SetDay(data.Day);
        }

        #region Buttons
        public void SetEvents(UnityAction onCancel, UnityAction onSave = null,UnityAction onDelete = null)
        {
            HideAllButtons();
            OnCancel = onCancel;
            OnSave = onSave;
            OnDelete = onDelete;
            ShowCancel(OnCancel != null);
            ShowSave(OnSave != null);
            ShowDelete(OnDelete != null);
        }
        private void DoCancel()
        {
            OnCancel?.Invoke();
            Clear();
        }
        private void DoSave()
        {
            OnSave?.Invoke();
            Clear();
        }
        private void DoDelete()
        {
            OnDelete?.Invoke();
            Clear();
        }

        public void ShowCancel(bool show)
        {
            _cancelGo.SetActive(show);
        }
        public void ShowSave(bool show)
        {
            _saveGo.SetActive(show);
        }
        public void ShowDelete(bool show)
        {
            _deleteGo.SetActive(show);
        }
        #endregion

        public void HideAllButtons()
        {
            ShowCancel(false);
            ShowSave(false);
            ShowDelete(false);
        }

        public int CurDay;
        private void SetDay(int theDay)
        {
            Setup();
            TheDay.text = ClassInfo.TheDay(theDay);
            CurDay = theDay;
        }
        public void Clear()
        {
            Show(false);
            OnSave = null;
            OnDelete = null;
            OnCancel = null;
        }

        #region GetInfo

        protected override void OnShowScreen(ScreenName theScreen)
        {
            Show(false);
        }

        protected override void OnDisplayAction(DisplayWindowAction theAction)
        {
            
        }
        #endregion
    }
}