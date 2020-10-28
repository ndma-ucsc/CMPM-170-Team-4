using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Reference: https://www.gamasutra.com/blogs/YuChao/20170316/293814/Music_Syncing_in_Rhythm_Games.php
public class RythmSystem : MonoBehaviour {
    public static RythmSystem instance;
	float songPosition;
	public float songPosInBeats;
	float secPerBeat;
	float dsptimesong;
    float qualifyingRange;

    public UnityEvent beat = new UnityEvent();
    public UnityEvent p1StartRecord  = new UnityEvent();
    public UnityEvent p1StartDeploy = new UnityEvent();
    public UnityEvent p2StartRecord = new UnityEvent();
    public UnityEvent p2StartDeploy = new UnityEvent();

	public float bpm;
	private List<float> notes  = new List<float>();
	bool recording = false;

	public int init_time = 8;
	public int end_time = 16;

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
    }

    // Update is called once per frame
    void Update() {
        songPosition = (float) (AudioSettings.dspTime - dsptimesong);
        songPosInBeats = songPosition / secPerBeat;
        
        if (!recording && songPosInBeats <= 16 && songPosInBeats >= 8) {
        	recording = true;
            p1StartRecord.Invoke();
            sprite.SetActive(true);
        }
        if (recording == true) {
        	if (Input.GetKeyDown(KeyCode.UpArrow)) {
        		notes.Add(songPosInBeats - init_time);
        	}
        }
        if (recording && songPosInBeats >= 16) {
        	recording = false;
            p1StartDeploy.Invoke();
            sprite.SetActive(false);
        }
    }

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
