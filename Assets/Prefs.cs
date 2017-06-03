using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefs : MonoBehaviour {
    public int Str, Ag, End, En, Hp;

    public GameObject mon, need;

    public void PlusStr()
    {
        if(need.GetComponent<forneed>().need <= mon.GetComponent<for_money>().money && Str < 99)
        {
            mon.GetComponent<for_money>().money -= need.GetComponent<forneed>().need;
            Str = Mathf.Min(Str + 1, 99);
            Save();
        }
    }


    public void PlusAg()
    {
        if (need.GetComponent<forneed>().need <= mon.GetComponent<for_money>().money && Ag < 99)
        {
            mon.GetComponent<for_money>().money -= need.GetComponent<forneed>().need;
            Ag = Mathf.Min(Ag + 1, 99);
            Save();
        }
    }


    public void PlusEnd()
    {
        if (need.GetComponent<forneed>().need <= mon.GetComponent<for_money>().money && End < 99)
        {
            mon.GetComponent<for_money>().money -= need.GetComponent<forneed>().need;
            End = Mathf.Min(End + 1, 99);
            Save();
        }
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
        Str = Random.Range(1, 10);
        Ag = Random.Range(1, 10);
        End = Random.Range(1, 10);
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


            /*public double[] state0, state1;/*
    public int action0;
    public double reward0;
    public Layer[] layer_defs;
    public int temporal_window, experience_size, start_learn_threshold;
    public double gamma;
    public int learning_steps_total, learning_steps_burnin;
    public double epsilon_min, epsilon_test_time;
    public int[] hidden_layer_sizes;
    public tdtrainer_options tdtrainer_options;
    public int net_inputs;
    public int num_states;
    public int num_actions;
    public int window_size;
    public double[][] state_window;
    public double[] reward_window;
    public int[] action_window;
    public double[][] net_window;
    public Net value_net;
    public SgdTrainer tdtrainer;
    public Experience[] experience;
    public int hlp_size;
    public int age, forward_passes;
    public double epsilon = 1.0;
    public double latest_reward;
    public double[] last_input_array;
    public bool learning;
    public string tag;
    */
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

    public int fight(int nStr, int nEnd, int nAg, int nHp, int nEn, double startTime)
    {
        return gameObject.GetComponent<from_field_to_text>().brain.forward(new double[11] {Str, End, Ag, Hp, En, nStr, nEnd, nAg , nHp, nEn, startTime});
    }

    public void reward(int dEn, int dHp, int nStr)
    {
        if(dEn * dHp >= 0)
            gameObject.GetComponent<from_field_to_text>().brain.backward((float)(dHp + dEn) / (float)(3 * (Str + nStr)));
    }

    public GameObject lvl;

    public void startfight()
    {
        int help99;
        if (gameObject.name == "EnemyFighter")
        {
            help99 = Random.Range(Mathf.Max(lvl.GetComponent<forlvl>().lvl - 3, 1), Mathf.Min(lvl.GetComponent<forlvl>().lvl + 3, 295) + 1);
            Str = Random.Range(Mathf.Max(1, help99 - 196), Mathf.Min(100, help99 + 1));
            End = Random.Range(Mathf.Max(1, help99 - Str - 97), Mathf.Min(100, help99 + 2 - Str));
            Ag = help99 - Str - End + 2;
        } else
        {
            help99 
        }

        Hp = Str * 10;
        En = End * 10;
    }
	
	// Update is called once per frame
	void Update () {
	}
}
