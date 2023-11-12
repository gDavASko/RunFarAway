using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RFW.Saves
{
    public class JSONSaves : Saves
    {
        protected string path = Application.persistentDataPath +Path.PathSeparator + "Saves" + Path.PathSeparator;
        protected string fileName =  "Parameters.dat";

        protected string FullPath => path + fileName;

        public override void Save()
        {
            string json = JsonUtility.ToJson(parameters);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            File.WriteAllText(FullPath, json);
        }

        public override void Load()
        {
            string json = File.ReadAllText(FullPath);
            parameters = JsonUtility.FromJson<List<SaveParameter>>(json);
        }
    }
}