using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UTS
{
    public class HourUiDisplay : MonoBehaviour
    {
        private TMP_Text _title;
        private TMP_Text _room;
        private TMP_Text _teacher;
        private TMP_Text _description;

        protected void Awake()
        {
            Setup();
        }

        protected void Setup()
        {
            if(_title)return;
            _title = transform.GetChild(0).GetComponent<TMP_Text>();
            _room = transform.GetChild(1).GetComponent<TMP_Text>();
            _teacher = transform.GetChild(2).GetComponent<TMP_Text>();
            _description = transform.GetChild(3).GetComponent<TMP_Text>();
        }


        public void SetClassInfo(ClassInfo info)
        {
            Setup();

            var schoolInfo = SchoolInfo.CurInfo;

            var roomInfo = schoolInfo.GetClassInfo(info.ClassId);
            _title.text = roomInfo.TheName;
            _room.text = schoolInfo.GetRoomInfo(info.ClassRoomId).Name;


            _teacher.text = schoolInfo.GetTeacher(info.TeacherId);

            var image = GetComponent<Image>();
            image.color = new Color32(roomInfo.r, roomInfo.g, roomInfo.b, 255);

            _info = info;
            _description.text = info.Description;

            if (info.ClassId == 0)
            {
                _room.text = "";
                _teacher.text = "";
                image.enabled = false;
                _description.text = "";
            }
        }

        private ClassInfo _info;

        [ContextMenu("Get Info")]
        public void UpdateSize()
        {
            var parentSize = transform.parent.parent.GetComponent<RectTransform>();
            var rect = transform.parent.parent.GetComponent<HorizontalLayoutGroup>();
            if (rect) LayoutRebuilder.ForceRebuildLayoutImmediate(parentSize);

            var height = parentSize.rect.height / 10;
            var duration = _info.Duration;

            if (duration < 1) duration = 1;

            var theTransform = GetComponent<RectTransform>();
            var size = theTransform.sizeDelta;
            size.y = height * duration;
            theTransform.sizeDelta = size;
        }
    }
}