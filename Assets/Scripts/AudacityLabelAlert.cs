using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class AudacityLabelAlert : MonoBehaviour
{
    public TextAsset exportedLabels;

    [Header ("Events")]
    public OnLabelEventHandler onLabel;
    public float delay = 0.0f;
    float[] labels;
    int nextLabelIndex = 0;
    public static bool stopMusicBool, birdDieBool, birdWinBool;
    [SerializeField] private AudioSource music, birdDie, birdWin, rocketSFX, explosionSFX;
    [SerializeField] private GameObject pauseMenuObject;

    void Start(){
        stopMusicBool = false; birdDieBool = false; birdWinBool = false;
        string rawData = exportedLabels.text;
        string[] rawLines = rawData.Split("\n\r".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
        labels = new float[rawLines.Length];

        for (int i = 0; i < rawLines.Length; i++) {
            string line = rawLines[i];
            string rawStartPosition = Regex.Match(line, "^[^\t]*").Value;
            labels[i] = float.Parse(rawStartPosition);
        }
    }

    void Update(){
        while(nextLabelIndex != labels.Length && music.time >= labels[nextLabelIndex] + delay) {
            onLabel.Invoke();
            nextLabelIndex++;
        }
        if(Input.GetKeyDown(KeyCode.H) && LevelManager.pauseMenuObject.activeInHierarchy == false){
            if(music.pitch == 1)
                music.pitch = 2;
            else
                music.pitch = 1;
        }
        if(stopMusicBool){
            music.Stop();
        }
        if(birdDieBool){
            birdDie.Play();
            birdDieBool = false;
        }
        if(birdWinBool){
            birdWin.Play();
            birdWinBool = false;
        }
    }
    [System.Serializable]
	public class OnLabelEventHandler : UnityEngine.Events.UnityEvent
	{

	}
    public void noSFX(){
        rocketSFX.enabled = !rocketSFX.enabled;
        explosionSFX.enabled = !explosionSFX.enabled;
    }
}
