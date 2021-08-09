using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealthSystem : MonoBehaviour
{
    public int max_health;
    public int current_health;
    public float respawn = 5f;
    public bool target_down;
    private Player money;
    // Start is called before the first frame update
    void Start()
    {
        current_health = max_health;
        money = FindObjectOfType<Player>();
        target_down = false;
    }

    // Update is called once per frame
    void Update()
    {
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

    public void TakingDamange(int damange)
    {
        current_health -= damange;


        if (current_health <= 0)
        {
            target_down = true;
        }
        
    }

}
