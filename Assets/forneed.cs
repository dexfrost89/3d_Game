using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class forneed : MonoBehaviour {

    public GameObject lvl;
    public int need;

	// Use this for initialization
	void Start () {
        need = 1;
	}
	
	// Update is called once per frame
	void Update () {
        double hlp = 1;
		for(int i = 0; i < lvl.GetComponent<forlvl>().lvl; i++)
        {
            hlp *= 1.1;
        }
        need = (int)hlp;

        gameObject.GetComponent<Text>().text = "need money for up:" + need.ToString();
    }
}
