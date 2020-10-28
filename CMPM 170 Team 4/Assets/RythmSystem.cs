using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference: https://www.gamasutra.com/blogs/YuChao/20170316/293814/Music_Syncing_in_Rhythm_Games.php
public class RythmSystem : MonoBehaviour {
	float songPosition;
	float songPosInBeats;
	float secPerBeat;
	float dsptimesong;

	public float bpm;
	private List<float> notes  = new List<float>();
	bool recording = false;
	public int init_time = 8;
	public int end_time = 16;

    // Start is called before the first frame update
    void Start() {
        secPerBeat = 60f / bpm;
        dsptimesong = (float) AudioSettings.dspTime;
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update() {
        songPosition = (float) (AudioSettings.dspTime - dsptimesong);
        songPosInBeats = songPosition / secPerBeat;
        
        if (!recording && songPosInBeats <= 16 && songPosInBeats >= 8) {
        	recording = true;
        }
        if (recording == true) {
        	if (Input.GetKeyDown(KeyCode.UpArrow)) {
        		notes.Add(songPosInBeats - init_time);
        	}
        }
        if (recording && songPosInBeats >= 16) {
        	recording = false;
        }
    }
}
