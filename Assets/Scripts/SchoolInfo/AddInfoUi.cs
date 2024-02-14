using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UTS
{
    public class AddInfoUi : ScreenController
    {
        private GameObject _parentGo, _inputsGo, _numbersGo, _colorGo, _contentGo, _buttonsGo;

        [HideInInspector] public InputUiObj _inputUi, _numbersUi, _colorsUi;


        private GameObject _cancelGo, _saveGo, _deleteGo;

        private FlexibleColorPicker _colorPicker;
        private GameObject _okColorGo;

        public static UnityAction OnDelete, OnSave, OnCancel;

        public static AddInfoUi Instance;

        protected void Awake()
        {
            Instance = this;
            Setup();
        }

        protected void Setup()
        {
            _parentGo = transform.Find("ParentObj").gameObject;

            var buts = _parentGo.transform.Find("Buttons");
            _buttonsGo = buts.gameObject;

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
            _contentGo = content.gameObject;

            _inputsGo = content.Find("Input").gameObject;
            _numbersGo = content.Find("Numeros").gameObject;
            _colorGo = content.Find("Color").gameObject;


            _inputUi = _inputsGo.GetComponent<InputUiObj>();
            _numbersUi = _numbersGo.GetComponent<InputUiObj>();
            _colorsUi = _colorGo.GetComponent<InputUiObj>();

            _colorPicker = _parentGo.transform.Find("ColorPicker").GetComponent<FlexibleColorPicker>();
            _okColorGo = _parentGo.transform.Find("OkColor").gameObject;
            var okBut = _okColorGo.GetComponent<Button>();
            okBut.onClick.AddListener(ConfirmColorPick);

            _colorsUi.Setup();
            _colorsUi.TheButton.onClick.AddListener(() => { ShowColorPicker(true); });

            ShowColorPicker(false);

            Show(false);
        }

        public void Show(bool show)
        {
            _parentGo.SetActive(show);
        }

        private void ShowColorPicker(bool show)
        {
            _colorPicker.gameObject.SetActive(show);
            _okColorGo.SetActive(show);

            _contentGo.SetActive(!show);
            _buttonsGo.SetActive(!show);
        }

        public Color GetColor()
        {
            return _colorPicker.color;
        }

        public void SetTheColor(Color theColor)
        {
            ShowColors(true);
            _colorPicker.color = theColor;
            _colorsUi.SetColorPickerColor(_colorPicker.color);

            _colorsUi.SetDisplayText("El color");
        }

        #region Buttons

        public void SetEvents(UnityAction onCancel, UnityAction onSave = null, UnityAction onDelete = null)
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

        public void HideAllInputs()
        {
            ShowColors(false);
            ShowNumbers(false);
            ShowColorPicker(false);
        }

        #region

        public void ShowNumbers(bool show)
        {
            _numbersGo.SetActive(show);
        }

        public void ShowColors(bool show)
        {
            _colorGo.SetActive(show);
        }

        #endregion

        public void Clear()
        {
            Show(false);
            ShowNumbers(false);
            ShowColors(false);
            OnSave = null;
            OnDelete = null;
            OnCancel = null;
        }

        private void ConfirmColorPick()
        {
            SetTheColor(GetColor());
            HideColorPicker();
        }

        private void HideColorPicker()
        {
            ShowColorPicker(false);
        }

        #region GetInfo

        public string GetInputText()
        {
            return _inputUi.GetText();
        }

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