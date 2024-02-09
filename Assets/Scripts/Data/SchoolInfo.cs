using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UTS
{
    [System.Serializable]
    public class SchoolInfo
    {
        public List<ClassData> ClassInfo = new List<ClassData>();
        public List<string> Teachers = new List<string>();
        public List<RoomInfo> ClassRooms = new List<RoomInfo>();

        private static SchoolInfo _curInfo;

        public static SchoolInfo CurInfo
        {
            get
            {
                if (_curInfo != null)
                {
                    return _curInfo;
                }

                _curInfo = Load();
                return _curInfo;
            }
            set
            {
                _curInfo = value;
            }
        }

        public List<string> ClassInfoNames()
        {
            var list = new List<string>();
            for (int i = 0; i < ClassInfo.Count; i++)
            {
                list.Add(ClassInfo[i].TheName);
            }
            return list;
        }
        public List<string> ClassRoomsNames()
        {
            var list = new List<string>();
            for (int i = 0; i < ClassRooms.Count; i++)
            {
                list.Add(ClassRooms[i].Name);
            }
            return list;
        }

        public ClassData GetClassInfo(int index)
        {
            if(index >= ClassInfo.Count)
            {
                return new ClassData() { TheName = "NO CLASS DATA", r = 1, g = 1, b = 1 };
            }
            return ClassInfo[index];
        }

        public string GetTeacher(int index)
        {
            if(index <= Teachers.Count)
            {
                return "No Maestro";
            }
            return Teachers[index];
        }

        public RoomInfo GetRoomInfo(int index)
        {
            if(index >= ClassRooms.Count)
            {
                return ClassRooms[ClassRooms.Count - 1];
            }
            return ClassRooms[index];
        }


        public void CreateTemplateData()
        {
            if (AlreadyExists())
            {
                Load();
                return;
            }

            AddClassInfo("");
            AddClassInfo("Ingles 1",160f/255f,160f / 255f, 254f / 255f);
            AddClassInfo("Fundamentos de redes",255f / 255f, 185f / 255f, 237f / 255f);
            AddClassInfo("Fundamentos de TI",(195f / 255f), 218f / 255f, 236f / 255f);
            AddClassInfo("Expresión oral y escrita 1",110f / 255f, 255f / 255f, 216f / 255f);
            AddClassInfo("Álgebra lineal",255f / 255f, 192f / 255f, 87f / 255f);
            AddClassInfo("Metadología de la programación",1f,1f,1f);
            AddClassInfo("Tutoría", 195f / 255f, 218f / 255f, 236f / 255f);
            AddClassInfo("Desarrollo de habilidades del pensamiento lógico",128f / 255f, 255f / 255f, 0);
            AddClassInfo("Formación sociocultural 1", 110f / 255f, 255f / 255f, 216f / 255f);

            AddTeacher(" ");
            AddTeacher("Karla Paola Capetillo Camacho");
            AddTeacher("David Rodríguez V.");
            AddTeacher("Gustavo Cruz B.");
            AddTeacher("Basilio Angeles Kevin Axl");
            AddTeacher("Marco Aurelio Ramírez S.");
            AddTeacher("Jesús Eduardo Gasca B.");

            AddRoomInfo(new RoomInfo("D209"));
            AddRoomInfo(new RoomInfo("Lab1"));
            AddRoomInfo(new RoomInfo("Lab2"));
            AddRoomInfo(new RoomInfo("Lab3"));
            AddRoomInfo(new RoomInfo("Lab4"));
            AddRoomInfo(new RoomInfo("Lab5"));
            AddRoomInfo(new RoomInfo("Lab6"));
            AddRoomInfo(new RoomInfo("Lab7"));


            Save();
        }

        public void AddClassInfo(string theName, float r = 1, float g = 1, float b = 1)
        {
            var data = new ClassData()
            {
                TheName = theName,
                r = r,
                g = g,
                b = b
            };
            ClassInfo.Add(data);
        }
        public void AddClassInfo(ClassData data)
        {
            ClassInfo.Add(data);
        }

        public void AddTeacher(string theName)
        {
            Teachers.Add(theName);
        }

        public void AddRoomInfo(RoomInfo info)
        {
            ClassRooms.Add(info);
        }

        public void Save()
        {
            SaveLoadData<SchoolInfo>.Save(this, SaveFileName);
            //var theJsonText = JsonUtility.ToJson(this, true);
            //File.WriteAllText(FilePath(), theJsonText);
            //Debug.Log("Create School info");


        }

        private static string SaveFileName = "schoolInfo";
        private static string FilePath()
        {
            var rootPath = Application.persistentDataPath;
            if (Directory.Exists(rootPath)) Directory.CreateDirectory(rootPath);
            return rootPath + "/schoolInfo.json";
        }

        private static bool AlreadyExists()
        {
            return SaveLoadData<SchoolInfo>.FileExistsOn(SaveFileName);
            //return File.Exists(FilePath());
        }
        public static SchoolInfo Load()
        {
           
            CurInfo =  SaveLoadData<SchoolInfo>.Load(SaveFileName);
            return CurInfo;

            //var theText = File.ReadAllText(FilePath());
            //var data = JsonUtility.FromJson<SchoolInfo>(theText);
            //CurInfo = data;
            //return data;
        }
    }
}