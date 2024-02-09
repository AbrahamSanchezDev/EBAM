namespace UTS
{
    [System.Serializable]
    public class ClassInfo
    {
        public int ClassId;
        public int TeacherId;
        public int ClassRoomId;
        public int Duration;
        public byte StartHour;
        public byte EndHour;
        public string Description;
        public byte Day;
        [System.NonSerialized]
        public int Index;

        public ClassInfo(int classId, int teacherId, int classRoomId, HourByName start, HourByName end, DaysByNam day)
        {
            ClassId = classId;
            TeacherId = teacherId;
            ClassRoomId = classRoomId;
            StartHour = (byte)start;
            EndHour = (byte)end;
            Day =  (byte)day;

           
            Duration = EndHour - StartHour;

            
            Description = ClassSchedule.Hours[StartHour].Hora + " - " + ClassSchedule.Hours[EndHour].Hora;
        }

        public bool HasTimeOnIt(int time)
        {

            //UnityEngine.Debug.Log(StartHour + " - " + EndHour + "  : " + time + "   " +"IGUAL "
            //    +(StartHour == time || EndHour == time) + "  BET "+(time >= StartHour && time < EndHour));
            //if (StartHour == time || EndHour == time) return true;
            return (time >= StartHour && time < EndHour);
        }
        public string GetDay()
        {
            return TheDay(Day);
        }

        public static string TheDay(int index)
        {
            switch (index)
            {
                case 1:
                    return "Lunes";
                case 2:
                    return "Martes";
                case 3:
                    return "Miércoles";
                case 4:
                    return "Jueves";
                case 5:
                    return "Viernes";
                case 6:
                    return "Sabado";
            }
            return "Domingo";
        }
    }
}