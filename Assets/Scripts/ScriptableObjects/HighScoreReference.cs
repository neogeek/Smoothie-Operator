using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace SmoothieOperator
{

    [CreateAssetMenu(fileName = "HighScoreReference", menuName = "HighScoreReference")]
    public class HighScoreReference : ScriptableObject
    {

        public static string path => Path.Combine(Application.persistentDataPath, "highScores.json");

        public List<Tuple<string, int>> highScores = new List<Tuple<string, int>>();

        public void Save()
        {

            File.WriteAllText(path,
                JsonConvert.SerializeObject(highScores, Formatting.Indented));

        }

        public void Load()
        {

            if (File.Exists(path))
            {

                highScores = JsonConvert.DeserializeObject<List<Tuple<string, int>>>(File.ReadAllText(path));

            }

        }

        public void AddScore(string playerName, int score)
        {

            if (score > 0)
            {

                highScores.Add(new Tuple<string, int>(playerName, score));

            }

        }

    }

}
