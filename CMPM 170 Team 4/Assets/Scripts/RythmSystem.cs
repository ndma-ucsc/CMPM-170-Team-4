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
    private List<float> notes  = new List<float>();
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
        
        // Test for time to start recording, if not already recording
        if (!recording && songPosInBeats <= 16 && songPosInBeats >= 8) {
        	recording = true;
            p1StartRecord.Invoke();
            sprite.SetActive(true);
        }
        // Record input
        if (recording == true) {
        	if (Input.GetKeyDown(KeyCode.UpArrow)) {
        		notes.Add(songPosInBeats - init_time);
        	}
        }
        // Stop recording
        if (recording && songPosInBeats >= 16) {
        	recording = false;
            p1StartDeploy.Invoke();
            sprite.SetActive(false);
        }
    }

    // Returns a list of all notes from current recording
    public List<float> getResult()
    {
        List<float> result = new List<float>(notes);

        for(int i = 0; i < result.Count; ++i)
        {
            result[i] += end_time;
        }
        return result;
    }
}
