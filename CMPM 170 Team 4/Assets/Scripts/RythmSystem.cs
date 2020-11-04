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
    private int last16th;

    /*** Recording parameters ***/
    private List<(float,int)> notes  = new List<(float,int)>();
    bool recording = false;

	public int init_time = 8;
	public int end_time = 16;

    /*** Events ***/
    public UnityEvent<int> note4 = new UnityEvent<int>(); // Event invoked each 4th note beat of the song, sending 4th note position
    public UnityEvent<int> note16 = new UnityEvent<int>(); // Event invoked each 16th note beat of the song, sending 16th note position
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
        last16th = 0;
    }

    // Update is called once per frame
    void Update() {
        // Update dynamic conductor variables
        songPosition = (float) (AudioSettings.dspTime - dsptimesong);
        songPosInBeats = songPosition / secPerBeat;

        // Check for 16th note
        if (songPosInBeats * 4 >= last16th + 1)
        {
            note16.Invoke(++last16th);
            if (last16th % 4 == 0)
            {
                note4.Invoke(last16th / 4);
                // Debug.Log(last16th / 4);
            }
            // else 
            // {
            //     Debug.Log(last16th);
            // }
        }
        
        // Test for start/stop recording
        if (!recording && last16th / 4 == 8) {
        	recording = true;
            p1StartRecord.Invoke();
            sprite.SetActive(true);
        }
        else if (recording && last16th / 4 == 16) {
        	recording = false;
            p1StartDeploy.Invoke();
            sprite.SetActive(false);
        }

        // Record input
        if (recording == true) {
            int playedNote16 = (int)Mathf.Floor(songPosInBeats * 4 + 0.5f);
        	if (Input.GetKeyDown(KeyCode.Z)) {
                if(notes.Count == 0 || notes[notes.Count - 1].Item1 != playedNote16) // note not already played for 16th
                {
        		    notes.Add((playedNote16 + (end_time - init_time) * 4,1));
                }
        	}
            else if (Input.GetKeyDown(KeyCode.X)) {
                if(notes.Count == 0 || notes[notes.Count - 1].Item1 != playedNote16) // note not already played for 16th
                {
        		    notes.Add((playedNote16 + (end_time - init_time) * 4,2));
                }
        	}
            else if (Input.GetKeyDown(KeyCode.C)) {
                if(notes.Count == 0 || notes[notes.Count - 1].Item1 != playedNote16) // note not already played for 16th
                {
        		    notes.Add((playedNote16 + (end_time - init_time) * 4,3));
                }
        	}
            else if (Input.GetKeyDown(KeyCode.V)) {
                if(notes.Count == 0 || notes[notes.Count - 1].Item1 != playedNote16) // note not already played for 16th
                {
        		    notes.Add((playedNote16 + (end_time - init_time) * 4,4));
                }
        	}
        }
    }

    // Returns a list of all notes from current recording
    public List<(float,int)> getResult()
    {
        List<(float,int)> result = new List<(float,int)>(notes);
        return result;
    }
}
