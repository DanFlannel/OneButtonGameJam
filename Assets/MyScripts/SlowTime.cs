using UnityEngine;
using System.Collections;

public class SlowTime : MonoBehaviour { 

    public float timeSpeed;

	// Use this for initialization
	void Start () {
        if(timeSpeed == 0)
        {
            timeSpeed = 1;
        }
        Time.timeScale = timeSpeed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
