using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
public static class SaveLoadData<T>
{
//#if UNITY_EDITOR
    public static string SavePath = Application.persistentDataPath + "/SaveData";
//#else
//    public static string SavePath = Environment.GetFolderPath((Environment.SpecialFolder.MyDocuments)) + "/My Games/Worlds Project";
//#endif

    private static string _fileFormat = ".dat";
    public static void Save(T dataToSave, string fileName, string customEnding = "")
    {
        if (!Directory.Exists(SavePath))
        {
            Directory.CreateDirectory(SavePath);
        }
        BinaryFormatter bf = new BinaryFormatter();
        string correctPath;
        if (customEnding == "")
        {
            correctPath = SavePath + "/" + fileName + _fileFormat;
        }
        else
        {
            correctPath = SavePath + "/" + fileName + customEnding;
        }
        FileStream file = File.Create(correctPath);
        bf.Serialize(file, dataToSave);
        file.Close();
    }
    public static T Load(string fileName, string customEnding = "")
    {
        if (customEnding == "")
        {
            if (File.Exists(SavePath + "/" + fileName + _fileFormat))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(SavePath + "/" + fileName + _fileFormat, FileMode.Open);
                var nData = bf.Deserialize(file);
                //T data = nData;
                file.Close();
                return (T)nData;
            }
        }
        else
        {
            if (File.Exists(SavePath + "/" + fileName + customEnding))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(SavePath + "/" + fileName + customEnding, FileMode.Open);
                //Gets the file data 
                var theData = bf.Deserialize(file);
                file.Close();
                return (T)theData;
            }
        }

        return default(T);
    }
    public static bool FileExistsOn(string fileName)
    {
        return File.Exists(SavePath + "/" + fileName + _fileFormat);
    }
    public static bool FileExistsOn(string fileName, string ending)
    {
        return File.Exists(SavePath + "/" + fileName + ending);
    }
    public static void DeleteFileWithName(string fileName)
    {
        if (File.Exists(SavePath + "/" + fileName + _fileFormat))
        {
            File.Delete(SavePath + "/" + fileName + _fileFormat);
        }
    }
}