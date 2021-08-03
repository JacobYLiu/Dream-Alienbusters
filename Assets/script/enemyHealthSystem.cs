using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealthSystem : MonoBehaviour
{
    public int max_health;
    public int current_health;
    public float respawn = 5f;
    // Start is called before the first frame update
    void Start()
    {
        current_health = max_health;
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public void TakingDamange(int damange)
    {
        current_health -= damange;


        if (current_health <= 0)
        {
            Destroy(gameObject);
        }
        
    }

}
