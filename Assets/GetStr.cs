using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetStr : MonoBehaviour {

    public GameObject Player;
    public GameObject StrVal, AgVal, EndVal;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        StrVal.GetComponent<Text>().text = "Str:" + Player.GetComponent<Prefs>().Str.ToString();
        AgVal.GetComponent<Text>().text = "Ag:" + Player.GetComponent<Prefs>().Ag.ToString();
        EndVal.GetComponent<Text>().text = "End:" + Player.GetComponent<Prefs>().End.ToString();
    }
}
