using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forjudge : MonoBehaviour {


    public bool fight_is_on;
    public GameObject player, enemy;
    public string prev;
    public double delayTime;
    public double startTime;

    public int act1, act2, hp1, prevhp1, hp2, prevhp2, en1, preven1, en2, preven2;
    // Use this for initialization
    void Start () {
        fight_is_on = false;
        prev = "None";
        delayTime = Time.time;
	}


    public void Stop()
    {
        fight_is_on = false;
    }

    public void strt()
    {
        fight_is_on = true;
        startTime = Time.time;
    }

    public void win()
    {
        prev = "Win";
        fight_is_on = false;

    }
    public void loose()
    {
        prev = "Loose";
        fight_is_on = false;

    }
    public void draw()
    {
        prev = "Draw";
        fight_is_on = false;

    }

    // Update is called once per frame
    void Update () {

		if(fight_is_on/* && Time.time - delayTime > 1*/)
        {
            delayTime = Time.time;
            preven1 = player.GetComponent<Prefs>().En;
            preven2 = enemy.GetComponent<Prefs>().En;
            prevhp1 = player.GetComponent<Prefs>().Hp;
            prevhp2 = enemy.GetComponent<Prefs>().Hp;
            act1 = player.GetComponent<Prefs>().fight(enemy.GetComponent<Prefs>().Str, enemy.GetComponent<Prefs>().End, enemy.GetComponent<Prefs>().Ag, 
                enemy.GetComponent<Prefs>().Hp, enemy.GetComponent<Prefs>().En, Time.time - startTime); //получаем действие игрока
            act2 = Random.Range(0, 5); // получаем действие врага
            if(act1 == 0 && player.GetComponent<Prefs>().En >= 2) //слабый удар на 1 хп и 2 энергии
            {
                if (act2 == 0 && enemy.GetComponent<Prefs>().En >= 2)
                {
                    enemy.GetComponent<Prefs>().Hp -= player.GetComponent<Prefs>().Str;
                    enemy.GetComponent<Prefs>().En -= 2;
                    player.GetComponent<Prefs>().Hp -= enemy.GetComponent<Prefs>().Str;
                    player.GetComponent<Prefs>().En -= 2;
                }
                else if (act2 == 1 && enemy.GetComponent<Prefs>().En >= 3)
                {
                    enemy.GetComponent<Prefs>().Hp -= player.GetComponent<Prefs>().Str;
                    enemy.GetComponent<Prefs>().En -= 3;
                    player.GetComponent<Prefs>().Hp -= enemy.GetComponent<Prefs>().Str * 2;
                    player.GetComponent<Prefs>().En -= 2;
                }
                else if (act2 == 2 && enemy.GetComponent<Prefs>().En > 0)
                {
                    enemy.GetComponent<Prefs>().En = Mathf.Max(0, enemy.GetComponent<Prefs>().En - player.GetComponent<Prefs>().Str);
                    player.GetComponent<Prefs>().En -= 2;
                } else if (act2 == 3 && enemy.GetComponent<Prefs>().En >= 2)
                {
                    if(Random.Range(1, enemy.GetComponent<Prefs>().Ag + player.GetComponent<Prefs>().Ag) <= enemy.GetComponent<Prefs>().Ag)
                    {
                        player.GetComponent<Prefs>().Hp -= (int) (enemy.GetComponent<Prefs>().Str * 1.5);
                        player.GetComponent<Prefs>().En -= 2;
                    } else
                    {
                        player.GetComponent<Prefs>().En -= 2;
                        enemy.GetComponent<Prefs>().Hp -= player.GetComponent<Prefs>().Str;
                    }
                    enemy.GetComponent<Prefs>().En -= 2;
                } else
                {
                    player.GetComponent<Prefs>().En -= 2;
                    enemy.GetComponent<Prefs>().Hp -= player.GetComponent<Prefs>().Str;
                }
            } else if(act1 == 1 && player.GetComponent<Prefs>().En >= 3) // сильный удар на 2 хп и 3 энергии
            {
                if (act2 == 0 && enemy.GetComponent<Prefs>().En >= 2)
                {
                    enemy.GetComponent<Prefs>().Hp -= player.GetComponent<Prefs>().Str * 2;
                    enemy.GetComponent<Prefs>().En -= 2;
                    player.GetComponent<Prefs>().Hp -= enemy.GetComponent<Prefs>().Str;
                    player.GetComponent<Prefs>().En -= 3;
                }
                else if (act2 == 1 && enemy.GetComponent<Prefs>().En >= 3)
                {
                    enemy.GetComponent<Prefs>().Hp -= player.GetComponent<Prefs>().Str * 2;
                    enemy.GetComponent<Prefs>().En -= 3;
                    player.GetComponent<Prefs>().Hp -= enemy.GetComponent<Prefs>().Str * 2;
                    player.GetComponent<Prefs>().En -= 3;
                }
                else if (act2 == 2 && enemy.GetComponent<Prefs>().En > 0)
                {
                    if(enemy.GetComponent<Prefs>().En >= player.GetComponent<Prefs>().Str * 3)
                    {
                        enemy.GetComponent<Prefs>().En = Mathf.Max(0, enemy.GetComponent<Prefs>().En - player.GetComponent<Prefs>().Str * 3);
                        player.GetComponent<Prefs>().En -= 3;
                    } else
                    {
                        enemy.GetComponent<Prefs>().Hp -= enemy.GetComponent<Prefs>().En - player.GetComponent<Prefs>().Str * 3;
                        player.GetComponent<Prefs>().En -= 3;
                    }
                }
                else if (act2 == 3 && enemy.GetComponent<Prefs>().En >= 2)
                {
                    if (Random.Range(1, enemy.GetComponent<Prefs>().Ag * 2 + player.GetComponent<Prefs>().Ag) <= enemy.GetComponent<Prefs>().Ag * 2)
                    {
                        player.GetComponent<Prefs>().Hp -= (int)(enemy.GetComponent<Prefs>().Str * 1.5);
                        player.GetComponent<Prefs>().En -= 3;
                    }
                    else
                    {
                        player.GetComponent<Prefs>().En -= 3;
                        enemy.GetComponent<Prefs>().Hp -= player.GetComponent<Prefs>().Str * 2;
                    }
                    enemy.GetComponent<Prefs>().En -= 2;
                }
                else
                {
                    player.GetComponent<Prefs>().En -= 3;
                    enemy.GetComponent<Prefs>().Hp -= player.GetComponent<Prefs>().Str * 3;
                }
            } else if(act1 == 2 && player.GetComponent<Prefs>().En > 0)// блок
            {
                if (act2 == 0 && enemy.GetComponent<Prefs>().En >= 2)
                {
                    enemy.GetComponent<Prefs>().En -= 2;
                    if (player.GetComponent<Prefs>().En >= enemy.GetComponent<Prefs>().Str)
                    {
                        player.GetComponent<Prefs>().En -= enemy.GetComponent<Prefs>().Str;
                    } else
                    {
                        player.GetComponent<Prefs>().Hp -= enemy.GetComponent<Prefs>().Str - player.GetComponent<Prefs>().En;
                        player.GetComponent<Prefs>().En = 0;
                    }
                }
                else if (act2 == 1 && enemy.GetComponent<Prefs>().En >= 3)
                {
                    enemy.GetComponent<Prefs>().En -= 3;
                    if (player.GetComponent<Prefs>().En >= enemy.GetComponent<Prefs>().Str * 3)
                    {
                        player.GetComponent<Prefs>().En -= enemy.GetComponent<Prefs>().Str * 3;
                    }
                    else
                    {
                        player.GetComponent<Prefs>().Hp -= enemy.GetComponent<Prefs>().Str * 3 - player.GetComponent<Prefs>().En;
                        player.GetComponent<Prefs>().En = 0;
                    }
                }
                else if(act2 == 3 && enemy.GetComponent<Prefs>().En >= 2)
                {

                    enemy.GetComponent<Prefs>().En -= 2;
                }
                else
                {
                    player.GetComponent<Prefs>().En += 1;
                    enemy.GetComponent<Prefs>().En += 1;
                    player.GetComponent<Prefs>().En = Mathf.Min(player.GetComponent<Prefs>().En, player.GetComponent<Prefs>().End * 10);
                    enemy.GetComponent<Prefs>().En = Mathf.Min(enemy.GetComponent<Prefs>().En, enemy.GetComponent<Prefs>().End * 10);
                }
            } else if(act1 == 3 && player.GetComponent<Prefs>().En >= 2) // уклон
            {
                if (act2 == 0 && enemy.GetComponent<Prefs>().En >= 2)
                {
                    if(Random.Range(1, enemy.GetComponent<Prefs>().Ag + player.GetComponent<Prefs>().Ag) <= player.GetComponent<Prefs>().Ag)
                    {
                        enemy.GetComponent<Prefs>().Hp -= (int)(player.GetComponent<Prefs>().Str * 1.5);
                        enemy.GetComponent<Prefs>().En -= 2;
                    } else
                    {
                        enemy.GetComponent<Prefs>().En -= 2;
                        player.GetComponent<Prefs>().Hp -= enemy.GetComponent<Prefs>().Str;
                    }
                }
                else if (act2 == 1 && enemy.GetComponent<Prefs>().En >= 3)
                {
                    if (Random.Range(1, enemy.GetComponent<Prefs>().Ag + player.GetComponent<Prefs>().Ag * 2) <= player.GetComponent<Prefs>().Ag * 2)
                    {
                        enemy.GetComponent<Prefs>().Hp -= (int)(player.GetComponent<Prefs>().Str * 1.5);
                        enemy.GetComponent<Prefs>().En -= 3;
                    }
                    else
                    {
                        enemy.GetComponent<Prefs>().En -= 3;
                        player.GetComponent<Prefs>().Hp -= enemy.GetComponent<Prefs>().Str * 2;
                    }
                }
                else if(act2 == 3 && enemy.GetComponent<Prefs>().En >= 2)
                {
                    enemy.GetComponent<Prefs>().En -= 2;
                }
                else
                {
              //      player.GetComponent<Prefs>().En += 1;
                    enemy.GetComponent<Prefs>().En += 1;
                  //  player.GetComponent<Prefs>().En = Mathf.Min(player.GetComponent<Prefs>().En, player.GetComponent<Prefs>().End * 10);
                    enemy.GetComponent<Prefs>().En = Mathf.Min(enemy.GetComponent<Prefs>().En, enemy.GetComponent<Prefs>().End * 10);
                }
                player.GetComponent<Prefs>().En -= 2;
            } else
            {
                if (act2 == 0 && enemy.GetComponent<Prefs>().En >= 2)
                {
                    enemy.GetComponent<Prefs>().En -= 2;
                    player.GetComponent<Prefs>().Hp -= enemy.GetComponent<Prefs>().Str;
                }
                else if (act2 == 1 && enemy.GetComponent<Prefs>().En >= 3)
                {
                    enemy.GetComponent<Prefs>().En -= 3;
                    player.GetComponent<Prefs>().Hp -= enemy.GetComponent<Prefs>().Str * 2;
                }
                else if (act2 == 3 && enemy.GetComponent<Prefs>().En >= 2)
                {
                    enemy.GetComponent<Prefs>().En -= 2;
                }
                else
                {
                    player.GetComponent<Prefs>().En += 1;
                    enemy.GetComponent<Prefs>().En += 1;
                    player.GetComponent<Prefs>().En = Mathf.Min(player.GetComponent<Prefs>().En, player.GetComponent<Prefs>().End * 10);
                    enemy.GetComponent<Prefs>().En = Mathf.Min(enemy.GetComponent<Prefs>().En, enemy.GetComponent<Prefs>().End * 10);
                }
            }
            en1 = player.GetComponent<Prefs>().En;
            en2 = enemy.GetComponent<Prefs>().En;
            hp1 = player.GetComponent<Prefs>().Hp;
            hp2 = enemy.GetComponent<Prefs>().Hp;
            player.GetComponent<Prefs>().reward(prevhp2 - hp2 + hp1 - prevhp1, preven2 - en2 + en1 - preven1, enemy.GetComponent<Prefs>().Str);
            if (player.GetComponent<Prefs>().Hp <= 0)
            {
                if(enemy.GetComponent<Prefs>().Hp <= 0)
                {
                    draw();
                } else
                {
                    loose();
                }
            } else
            {
                if (enemy.GetComponent<Prefs>().Hp <= 0)
                {
                    win();
                }
            }
        }
	}
}
