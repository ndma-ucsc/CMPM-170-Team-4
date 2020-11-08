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

	public int phaseLength = 2; // length of recording/attacking in measures
	private int phaseStart = 0; // beat that the current phase started on (record or attack)
    private int activePlayer = 0; // player currently recording/attacking (0 will automatically start with player 1)

    /*** Events ***/
    public UnityEvent<int> note4 = new UnityEvent<int>();  // Event invoked each 4th note beat of the song, sending 4th note position
    public UnityEvent<int> note16 = new UnityEvent<int>(); // Event invoked each 16th note beat of the song, sending 16th note position
    public UnityEvent p1StartRecord  = new UnityEvent();   // Event invoked on first beat of recorded measure
    public UnityEvent p1StartDeploy = new UnityEvent();    // Event invoked on the first beat after recording ends
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
        
        // start of recording phase
        if (!recording && last16th == phaseStart + 16 * phaseLength) {
            notes.Clear(); // empty previous recording
        	recording = true;
            phaseStart = last16th;
            if(activePlayer == 1) // switch to player 2 recording phase
            {
                Debug.Log("P2 Recording");
                activePlayer = 2;
                p2StartRecord.Invoke();
            }
            else // switch to player 1 recording phase
            {
                Debug.Log("P1 Recording");
                activePlayer = 1;
                p1StartRecord.Invoke();
            }
            sprite.SetActive(true);
        }
        else if (recording && last16th == phaseStart + 16 * phaseLength) { // start of attack phase
        	recording = false;
            phaseStart = last16th;
            if(activePlayer == 1) // switch to player 1 attack phase
            {
                Debug.Log("P1 Attacking");
                p1StartDeploy.Invoke();
            }
            else // switch to player 2 attack phase
            {
                Debug.Log("P2 Attacking");
                p2StartDeploy.Invoke();
            }
            sprite.SetActive(false);
        }

        // Record input
        if (recording == true) {
            int playedNote16 = (int)Mathf.Floor(songPosInBeats * 4 + 0.5f);
        	if (Input.GetKeyDown(KeyCode.Z)) {
                if(notes.Count == 0 || notes[notes.Count - 1].Item1 != playedNote16 + phaseLength * 16)
                {
        		    notes.Add((playedNote16 + phaseLength * 16,1));
                }
        	}
            else if (Input.GetKeyDown(KeyCode.X)) {
                if(notes.Count == 0 || notes[notes.Count - 1].Item1 != playedNote16 + phaseLength * 16)
                {
        		    notes.Add((playedNote16 + phaseLength * 16,2));
                }
        	}
            else if (Input.GetKeyDown(KeyCode.C)) {
                if(notes.Count == 0 || notes[notes.Count - 1].Item1 != playedNote16 + phaseLength * 16)
                {
        		    notes.Add((playedNote16 + phaseLength * 16,3));
                }
        	}
            else if (Input.GetKeyDown(KeyCode.V)) {
                if(notes.Count == 0 || notes[notes.Count - 1].Item1 != playedNote16 + phaseLength * 16)
                {
        		    notes.Add((playedNote16 + phaseLength * 16,4));
                }
        	}
        }
    }

    // Returns a list of all notes from current recording
    public List<(float,int)> getResult()
    {
        // Debug.Log(notes.Count);
        return new List<(float,int)>(notes);
    }
}
