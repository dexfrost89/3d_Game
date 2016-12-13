using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoGoFight : MonoBehaviour {
    public string fighting;
    public GameObject canvasFight, canvasDefault;
    public float MaxHp, MaxEnergy, Hp, Energy;
    public int Str, Ag, End;

	// Use this for initialization
	void Start () {
        fighting = "no";
	}

    public void Go()
    {
        fighting = "yes";
        Str = GetComponent<Prefs>().Str;
    }

	
	// Update is called once per frame
	void Update () {
		if(fighting == "yes")
        {
            if(Hp <= 0)
            {
                fighting = "no";
                canvasFight.GetComponent<Canvas>().enabled = false;
                canvasDefault.GetComponent<Canvas>().enabled = true;
            }
        }
	}
}
