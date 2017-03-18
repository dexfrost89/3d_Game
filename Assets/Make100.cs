using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Make100 : MonoBehaviour {
    
    public GameObject judge, cnv1, Myf, Enf, cnv2, bt1;
    public bool ftison;
    public bool is_fighting;
    public int i;

	// Use this for initialization
	void Start ()
    {
        is_fighting = false;
	}

    public void On()
    {
        i = 0;
        is_fighting = true;
        ftison = false;
    }

    public void off()
    {
        i = 0;


        is_fighting = false;
    }
    
	
	// Update is called once per frame
	void Update () {

        if(is_fighting && i >= 1000000000)
        {
            i = 0;
            is_fighting = false;
        } else if (is_fighting)
        {
            if(ftison == false)
            {
                Myf.GetComponent<Prefs>().RandStats();
                Myf.GetComponent<Prefs>().startfight();

                Enf.GetComponent<Prefs>().RandStats();
                Enf.GetComponent<Prefs>().startfight();
                cnv1.GetComponent<Canvas>().enabled = false;
                cnv2.GetComponent<Canvas>().enabled = true;
                judge.GetComponent<forjudge>().strt();
                ftison = true;
            } else
            {
                ftison = judge.GetComponent<forjudge>().fight_is_on;
                if(ftison == false)
                {
                    
                    bt1.GetComponent<Itstimetostop>().StopOk();
                    cnv2.GetComponent<Canvas>().enabled = false;
                    cnv1.GetComponent<Canvas>().enabled = true;
                    i++;
                }
            }
            
            
        }

    }
}
