using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchSystem : MonoBehaviour
{
    UI_controller canvas;
    Player player1;
    private GunSystem active_gun;
    public List<GunSystem> guns = new List<GunSystem>();
    public int gun_index;
    public List<Animator> animatorList;
    public List<ammoPickUp> ammoPickUps;
    
    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<UI_controller>();
        foreach (GunSystem gun in guns)
        {
            gun.gameObject.SetActive(false);
        }
        for (int i =0; i < canvas.gunImage.Count; i++)
        {
            canvas.gunImage[i].gameObject.SetActive(false);
        }
        player1 = GetComponentInParent<Player>();
        active_gun = guns[gun_index];
        canvas.gunImage[gun_index].gameObject.SetActive(true);
        active_gun.gameObject.SetActive(true);
        player1.Switch_animator(animatorList[gun_index]);
        foreach(ammoPickUp ammoBox in ammoPickUps)
        {
            ammoBox.gunsys = active_gun;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            switch_gun();
        }
    }

    private void switch_gun()
    {
        active_gun.gameObject.SetActive(false);
        canvas.gunImage[gun_index].gameObject.SetActive(false);
        gun_index++;
        if(gun_index >= guns.Count)
        {
            gun_index = 0;
        }
        canvas.gunImage[gun_index].gameObject.SetActive(true);
        player1.Switch_animator(animatorList[gun_index]);
        active_gun = guns[gun_index];
        active_gun.gameObject.SetActive(true);
        foreach (ammoPickUp ammoBox in ammoPickUps)
        {
            ammoBox.gunsys = active_gun;
        }
    }
}
