namespace UTS
{
    [System.Serializable]
    public class ClassInfo
    {
        public int ClassId;
        public int TeacherId;
        public int ClassRoomId;
        public int Duration;
        //public byte StartHour;
        //public byte EndHour;
        public string Description;

        public ClassInfo(int classId, int teacherId, int classRoomId,byte duration,string description="")
        {
            ClassId = classId;
            TeacherId = teacherId;
            ClassRoomId = classRoomId;
            Duration = duration;
            Description = description;
        }
    }
}