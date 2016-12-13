using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefs : MonoBehaviour {
    public int Str, Ag, End;


    public void PlusStr()
    {
        Str = Mathf.Min(Str + 1, 99);
        Save();
    }


    public void PlusAg()
    {
        Ag = Mathf.Min(Ag + 1, 99);
        Save();
    }


    public void PlusEnd()
    {
        End = Mathf.Min(End + 1, 99);
        Save();
    }

    public void MinusStr()
    {
        Str = Mathf.Max(Str - 1, 1);
        Save();
    }


    public void MinusAg()
    {
        Ag = Mathf.Max(Ag - 1, 1);
        Save();
    }


    public void MinusEnd()
    {
        End = Mathf.Max(End - 1, 1);
        Save();
    }

    public void RandStats()
    {
        Str = Random.Range(1, 100);
        Ag = Random.Range(1, 100);
        End = Random.Range(1, 100);
        Save();
    }

    public void ResetStats()
    {
        Str = 1;
        Ag = 1;
        End = 1;
        Save();
    }


    void Save()
    {
        if(gameObject.tag == "MyFighter")
        {
            PlayerPrefs.SetInt("MyEnd", End);
            PlayerPrefs.SetInt("MyAg", Ag);
            PlayerPrefs.SetInt("MyStr", Str);
        }
    }

    // Use this for initialization
    void Start ()
    {
        if(gameObject.tag == "MyFighter")
        {
            if (PlayerPrefs.HasKey("MyStr"))
            {
                Str = PlayerPrefs.GetInt("MyStr");
            }
            else
            {
                Str = 1;
                PlayerPrefs.SetInt("MyStr", 1);
            }


            if (PlayerPrefs.HasKey("MyAg"))
            {
                Ag = PlayerPrefs.GetInt("MyAg");
            }
            else
            {
                Ag = 1;
                PlayerPrefs.SetInt("MyAg", 1);
            }


            if (PlayerPrefs.HasKey("MyEnd"))
            {
                End = PlayerPrefs.GetInt("MyEnd");
            }
            else
            {
                End = 1;
                PlayerPrefs.SetInt("MyEnd", 1);
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
	}
}
