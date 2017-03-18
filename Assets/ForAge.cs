using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForAge : MonoBehaviour {
    public Text txt;
    public from_field_to_text neuro;

	// Use this for initialization
	void Start () {
        txt = GameObject.FindObjectOfType<ForAge>().GetComponent<Text>();
        neuro = GameObject.Find("MyFighter").GetComponent<from_field_to_text>();
	}
	
	// Update is called once per frame
	void Update () {
        txt.text = neuro.brain.age.ToString();
	}
}
