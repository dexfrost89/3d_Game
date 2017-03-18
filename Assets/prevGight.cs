using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class prevGight : MonoBehaviour {
    public GameObject judge;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Text>().text = judge.GetComponent<forjudge>().prev;
	}
}
