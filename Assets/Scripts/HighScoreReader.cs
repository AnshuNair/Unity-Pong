using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HighScoreReader : MonoBehaviour
{
    int highScore;
    public Text highScoreText;
    string path;

    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "\\highScore.txt";

        if (File.Exists(path))
        {
            StreamReader reader = new StreamReader(path);
            string line = reader.ReadLine();
            string[] strArray = line.Split(',');
            highScore = int.Parse(strArray[1]);
            reader.Close();
            highScoreText.text = "HighScore: " + highScore.ToString();
        }
        else
        {
            highScoreText.text = "HighScore: 0";
        }
    }
}
