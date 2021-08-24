using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int max_health;
    public int current_health;
    public float respawn = 5f;
    public bool target_down;
    private Player money;

    Enemy_UI_control hp_bar;

    // Start is called before the first frame update
    void Start()
    {
        current_health = max_health;
        hp_bar = GetComponentInChildren<Enemy_UI_control>();
        money = FindObjectOfType<Player>();
        target_down = false;
        hp_bar.SetMaxHealth(current_health);
    }

    // Update is called once per frame
    void Update()
    {
        hp_bar.SetHealth(current_health);
        drop_gold();
    }

    private void drop_gold()
    {
        if (target_down)
        {
            money.GetComponent<Player>().Add_gold(1);
            Destroy(gameObject);
        }

    }

    public void TakingDamange(int damage)
    {
        current_health -= damage;


        if (current_health <= 0)
        {
            target_down = true;
        }

    }

}
