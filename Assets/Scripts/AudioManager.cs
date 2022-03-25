    using System.Collections; 
    using System.Collections.Generic; 
    using UnityEngine; 
     
    public class AudioManager : MonoBehaviour { 
        public AudioSource audioSource; 
        public AudioClip music; 
        public float bpm; 
         
        private int _timeCache; 
        private float _samplesPerBeat; 
        private int _beatCount; 
        
        // Delegate method and event that other scripts can subscribe to 
        public delegate void OnBeatAction(); 
        public static event OnBeatAction OnBeat; 
     
    	void Start () 
        { 
            audioSource.clip = music; 
            audioSource.loop = true; 
            audioSource.Play(); 
     
            // Calculate number of samples per beat 
            _samplesPerBeat = (60f / bpm) * music.frequency; 
     
            audioSource.timeSamples = 0; 
            _beatCount = 0; 
             
    	} 
    		 
    	void Update () 
        { 
            // Handle audio when looping 
            if (audioSource.timeSamples < _timeCache) 
            { 
                _beatCount = 0; 
                _timeCache = audioSource.timeSamples; 
                if (OnBeat != null) OnBeat();         
            } 
     
            // Detect if a beat has been reached - if it has, trigger the OnBeat event for scripts that are subscribing to the event 
            if (audioSource.timeSamples > _samplesPerBeat * _beatCount) 
            { 
                if (OnBeat != null) OnBeat(); 
                _beatCount++; 
                _timeCache = audioSource.timeSamples;             
            }        
        } 
    } 