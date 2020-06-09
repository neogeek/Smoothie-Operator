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

        public Dictionary<string, int> highScores = new Dictionary<string, int>();

        public void Save()
        {

            File.WriteAllText(path,
                JsonConvert.SerializeObject(highScores, Formatting.Indented));

        }

        public void Load()
        {

            if (File.Exists(path))
            {

                highScores = JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText(path));

            }

        }

        public void AddScore(string playerName, int score)
        {

            highScores.Add(playerName, score);

        }

    }

}
