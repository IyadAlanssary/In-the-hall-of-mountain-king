using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float time = 0.0f;
    public TextMeshProUGUI textBox;
    void Start()
    {
        textBox.text = GetTimeString(time);
    }

    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        textBox.text = GetTimeString(time);
    }
    string GetTimeString(float time)
    {
        int minutes = (int)time / 60;
        int sec = (int)time % 60;
        return minutes.ToString("D2") + ":" + sec.ToString("D2");
    }
}
