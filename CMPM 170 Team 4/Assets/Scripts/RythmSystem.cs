using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Reference: https://www.gamasutra.com/blogs/YuChao/20170316/293814/Music_Syncing_in_Rhythm_Games.php
public class RythmSystem : MonoBehaviour {
    /*** Singleton instance reference ***/
    public static RythmSystem instance;

    /*** Conductor parameters ***/
    public float songPosInBeats;
	float songPosition;
	float secPerBeat;
	float dsptimesong;
    public float bpm;
    private int lastBeat;

    /*** Recording parameters ***/
    private List<(float,int)> notes  = new List<(float,int)>();
    bool recording = false;

	public int init_time = 8;
	public int end_time = 16;

    /*** Events ***/
    public UnityEvent beat = new UnityEvent();           // Event invoked each beat of the song
    public UnityEvent p1StartRecord  = new UnityEvent(); // Event invoked on first beat of recorded measure
    public UnityEvent p1StartDeploy = new UnityEvent();  // Event invoked on the first beat after recording ends
    public UnityEvent p2StartRecord = new UnityEvent();
    public UnityEvent p2StartDeploy = new UnityEvent();

    public GameObject sprite;

    void Awake() 
    {
        instance = this;
    }
    

    // Start is called before the first frame update
    void Start() {
        secPerBeat = 60f / bpm;
        dsptimesong = (float) AudioSettings.dspTime;
        GetComponent<AudioSource>().Play();
        sprite.SetActive(false);
        lastBeat = 0;
    }

    // Update is called once per frame
    void Update() {
        // Update dynamic conductor variables
        songPosition = (float) (AudioSettings.dspTime - dsptimesong);
        songPosInBeats = songPosition / secPerBeat;

        // Check for beat
        if (songPosInBeats >= lastBeat + 1)
        {
            lastBeat++;
            beat.Invoke();
        }
        
        // Test for start/stop recording
        if (!recording && lastBeat == 8) {
        	recording = true;
            p1StartRecord.Invoke();
            sprite.SetActive(true);
        }
        else if (recording && lastBeat == 16) {
        	recording = false;
            p1StartDeploy.Invoke();
            sprite.SetActive(false);
        }

        // Record input
        if (recording == true) {
        	if (Input.GetKeyDown(KeyCode.Z)) {
        		notes.Add((songPosInBeats - init_time,1));
        	}
            if (Input.GetKeyDown(KeyCode.X)) {
        		notes.Add((songPosInBeats - init_time,2));
        	}
            if (Input.GetKeyDown(KeyCode.C)) {
        		notes.Add((songPosInBeats - init_time,3));
        	}
            if (Input.GetKeyDown(KeyCode.V)) {
        		notes.Add((songPosInBeats - init_time,4));
        	}
        }
    }

    // Returns a list of all notes from current recording
    public List<(float,int)> getResult()
    {
        List<(float,int)> result = new List<(float,int)>(notes);

        for(int i = 0; i < result.Count; ++i)
        {
            result[i] = (result[i].Item1 + end_time, result[i].Item2);
        }
        return result;
    }
}
