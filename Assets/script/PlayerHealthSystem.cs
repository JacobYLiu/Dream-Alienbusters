using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public int max_health;
    public int current_health;

    UI_controller hp_bar;
    // Start is called before the first frame update
    void Start()
    {
        current_health = max_health;
        hp_bar = FindObjectOfType<UI_controller>();
        hp_bar.SetMaxHealth(max_health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakingDammange(int damange)
    {
        current_health -= damange;

        hp_bar.SetHealth(current_health);

        if(damange <= 0)
        {
            //player die
            gameObject.SetActive(false);
        }
    }
}
