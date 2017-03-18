using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class forHp : MonoBehaviour {
    public GameObject pl;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame

	void Update () {
        gameObject.GetComponent<Text>().text = pl.GetComponent<Prefs>().Hp.ToString();
	}
}
