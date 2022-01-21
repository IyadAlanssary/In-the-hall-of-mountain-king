using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public float timeStart;
    public Text textBox;
    //public Text startBtnText;

    bool timerActive = true;

	void Start () {
        textBox.text = timeStart.ToString("F2");
	}
	
    void FixedUpdate () {
        if(timerActive){
            timeStart += Time.fixedDeltaTime;
            textBox.text = timeStart.ToString("F2");
        }
	}
    /*public void timerButton(){
        timerActive = !timerActive;
        startBtnText.text = timerActive ? "Pause" : "Start";
    }
    */
}
