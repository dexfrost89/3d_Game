using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class for_money : MonoBehaviour {
    public int money;


	// Use this for initialization
	void Start ()
    {
        if (PlayerPrefs.HasKey("money"))
        {
            money = PlayerPrefs.GetInt("money");
        }
        else
        {
            money = 10;
            PlayerPrefs.SetInt("money", 10);
        }
    }

    public void Res()
    {
        money = 10;
        PlayerPrefs.SetInt("money", 10);
    }

    // Update is called once per frame
    void Update () {
        PlayerPrefs.SetInt("money", money);
        gameObject.GetComponent<Text>().text = "money:" + money.ToString();
    }
}
