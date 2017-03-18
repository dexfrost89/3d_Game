using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itstimetostop : MonoBehaviour {

    public int hz;
    public GameObject judge;

    public void StopOk()
    {
        judge.GetComponent<forjudge>().Stop();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
