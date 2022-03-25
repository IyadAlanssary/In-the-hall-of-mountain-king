using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savedData : MonoBehaviour
{
    public static bool winnerWinnerChickenDinner = false;
    void Start(){
        DontDestroyOnLoad(this.gameObject);
    }
}
