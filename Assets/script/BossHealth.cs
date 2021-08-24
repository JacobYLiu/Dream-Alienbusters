using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public int max_health;
    public int current_health;
    public bool target_down;

    public Animator boss_animation;
    int take_damage_animation_index = 0;
    Enemy_UI_control hp_bar;

    // Start is called before the first frame update
    void Start()
    {
        current_health = max_health;
        hp_bar = GetComponentInChildren<Enemy_UI_control>();
        target_down = false;
        hp_bar.SetMaxHealth(current_health);
    }

    // Update is called once per frame
    void Update()
    {
        hp_bar.SetHealth(current_health);
    }

    public void TakingDamange(int damage)
    {
        current_health -= damage;


        if (current_health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

        }
        else
        {
            switch (take_damage_animation_index)
            {
                case 0:
                    boss_animation.SetTrigger("Take_Damage_1");
                    break;
                case 1:
                    boss_animation.SetTrigger("Take_Damage_2");
                    break;
                case 2:
                    boss_animation.SetTrigger("Take_Damage_3");
                    break;
                default:
                    take_damage_animation_index = 0;
                    break;
            }
        }
        take_damage_animation_index++;
        if(take_damage_animation_index > 2)
        {
            take_damage_animation_index = 0;
        }

    }


}
