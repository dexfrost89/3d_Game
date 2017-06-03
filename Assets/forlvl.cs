using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class forlvl : MonoBehaviour {

    public GameObject my;
    public int lvl;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        lvl = my.GetComponent<Prefs>().End + my.GetComponent<Prefs>().Ag + my.GetComponent<Prefs>().Str - 2;
        gameObject.GetComponent<Text>().text = "level:" + lvl.ToString();
	}
}
