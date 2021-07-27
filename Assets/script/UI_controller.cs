using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_controller : MonoBehaviour
{

    public TextMeshProUGUI ammoCount;
    public TextMeshProUGUI total_bullet;

    public Slider hp_bar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaxHealth(int health)
    {
        hp_bar.maxValue = health;
        hp_bar.value = health;
    }

    public void SetHealth(int health)
    {
        hp_bar.value = health;
    }
}
