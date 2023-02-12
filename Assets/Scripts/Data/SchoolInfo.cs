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


        public ClassData GetClassInfo(int index)
        {
            return ClassInfo[index];
        }

        public string GetTeacher(int index)
        {
            return Teachers[index];
        }

        public RoomInfo GetRoomInfo(int index)
        {
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
            AddClassInfo("Ingles 1",160,160,254);
            AddClassInfo("Fundamentos de redes",255,185,237);
            AddClassInfo("Fundamentos de TI",195,218,236);
            AddClassInfo("Expresión oral y escrita 1",110,255,216);
            AddClassInfo("Álgebra lineal",255,192,87);
            AddClassInfo("Metadología de la programación",0);
            AddClassInfo("Tutoría", 195, 218, 236);
            AddClassInfo("Desarrollo de habilidades del pensamiento lógico",128,255,0);
            AddClassInfo("Formación sociocultural 1", 110, 255, 216);

            Teachers.Add(" ");
            Teachers.Add("Karla Paola Capetillo Camacho");
            Teachers.Add("David Rodríguez V.");
            Teachers.Add("Gustavo Cruz B.");
            Teachers.Add("Edgar Geovanni Barrera C.");
            Teachers.Add("Marco Aurelio Ramírez S.");
            Teachers.Add("Jesús Eduardo Gasca B.");

            ClassRooms.Add(new RoomInfo("D209"));
            ClassRooms.Add(new RoomInfo("Lab1"));
            ClassRooms.Add(new RoomInfo("Lab2"));
            ClassRooms.Add(new RoomInfo("Lab3"));
            ClassRooms.Add(new RoomInfo("Lab4"));
            ClassRooms.Add(new RoomInfo("Lab5"));
            ClassRooms.Add(new RoomInfo("Lab6"));
            ClassRooms.Add(new RoomInfo("Lab7"));


            Save();
        }

        public void AddClassInfo(string theName, byte r = 255, byte g = 255, byte b = 255)
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

        public void Save()
        {
            var theJsonText = JsonUtility.ToJson(this, true);
            File.WriteAllText(FilePath(), theJsonText);
            //Debug.Log("Create School info");
        }

        private static string FilePath()
        {
            var rootPath = Application.persistentDataPath;
            if (Directory.Exists(rootPath)) Directory.CreateDirectory(rootPath);
            return rootPath + "/schoolInfo.json";
        }

        private static bool AlreadyExists()
        {
            return File.Exists(FilePath());
        }
        public static SchoolInfo Load()
        {
            var theText = File.ReadAllText(FilePath());

            var data = JsonUtility.FromJson<SchoolInfo>(theText);
            CurInfo = data;

            return data;
        }
    }
}