    using System.Collections; 
    using System.Collections.Generic; 
    using UnityEngine; 
     
    public class AudioAction : MonoBehaviour { 
         
        public float damper = 0.1f; 
        public Vector3 onBeatScale = new Vector3(2f, 2f, 2f); 
        public Vector3 onBeatRotate = new Vector3(0, 0, -45f); 
        public int beatInterval = 1;     
        public int beatCounter; 
     
        void OnEnable() 
        { 
            // Subscribe tot he OnBeat event from the AudioManager script 
            AudioManager.OnBeat += OnBeatAction; 
        } 
     
        void OnDisable() 
        { 
            // Unsubscribe tot he OnBeat event from the AudioManager script 
            AudioManager.OnBeat -= OnBeatAction; 
        } 
     
        // Perform some action when the OnBeat event trigger the OnBeatAction method 
        void OnBeatAction() 
        { 
            beatCounter++; 
            if (beatCounter >= beatInterval) 
            { 
                transform.localScale = onBeatScale; 
                transform.Rotate(onBeatRotate); 
                beatCounter = 0; 
            } 
        } 
         
        void Update() 
        { 
            // For demo purposes, revert back to the original shape of the object 
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * damper); 
        } 
         
    } 