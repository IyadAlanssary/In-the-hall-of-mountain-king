using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class AudacityLabelAlert : MonoBehaviour
{
    public TextAsset exportedLabels;

    [Header("Events")]
    public float delay = 0.0f;
    float[] labels;
    int[] positions;
    int nextLabelIndex = 0;
    [SerializeField] private GameObject pauseMenuObject;
    AudioSource music;

    void Start()
    {
        music = AudioManager.music.source;
        string rawData = exportedLabels.text;
        string[] rawLines = rawData.Split("\n\r".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
        labels = new float[rawLines.Length];
        positions = new int[rawLines.Length];

        for (int i = 0; i < rawLines.Length; i++)
        {
            string line = rawLines[i];
            string rawStartPosition = Regex.Match(line, "^[^\t]*").Value;
            string number = Regex.Match(line, @"\d$").Value;
            labels[i] = float.Parse(rawStartPosition);
            positions[i] = int.Parse(number);
        }
    }

    void Update()
    {
        while (nextLabelIndex != labels.Length && music.time >= labels[nextLabelIndex] + delay)
        {
            LevelManager.instance.RandomizeRocketOrExplosion(positions[nextLabelIndex]);
            nextLabelIndex++;
        }
    }
}
