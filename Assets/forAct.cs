using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class forAct : MonoBehaviour
{
    public GameObject pl;
    public int who;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        if (who == 1)
        {
            if (pl.GetComponent<forjudge>().act1 == 0)
            {
                gameObject.GetComponent<Text>().text = "punch";
            }
            if (pl.GetComponent<forjudge>().act1 == 1)
            {
                gameObject.GetComponent<Text>().text = "strong punch";
            }
            if (pl.GetComponent<forjudge>().act1 == 2)
            {
                gameObject.GetComponent<Text>().text = "block";
            }
            if (pl.GetComponent<forjudge>().act1 == 3)
            {
                gameObject.GetComponent<Text>().text = "dodge";
            }
        }
        if (who == 2)
        {
            if (pl.GetComponent<forjudge>().act2 == 0)
            {
                gameObject.GetComponent<Text>().text = "punch";
            }
            if (pl.GetComponent<forjudge>().act2 == 1)
            {
                gameObject.GetComponent<Text>().text = "strong punch";
            }
            if (pl.GetComponent<forjudge>().act2 == 2)
            {
                gameObject.GetComponent<Text>().text = "block";
            }
            if (pl.GetComponent<forjudge>().act2 == 3)
            {
                gameObject.GetComponent<Text>().text = "dodge";
            }
        }
    }
}
