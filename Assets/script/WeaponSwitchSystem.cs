using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        foreach(ammoPickUp ammobox in ammoPickUps)
        {
            ammobox.gunsys = active_gun;
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


    public int i;

    public Player player;
    public int djc;
    public GameObject goumai;
    public void Qiang(Button btn)
    {
        
        switch (djc)
        {
            case 0:
                if (player.gold_count >= 3)
                {
                    i++;
                    btn.enabled = false;
                    player.gold_count -= 3;
                    canvas.gold.SetText(player.gold_count.ToString());
                    djc++;
                    goumai.SetActive(true);
                }
                break;
            case 1:
                if (djc == 1)
                {
                    if (player.gold_count >= 5)
                    {
                        i++;
                        btn.enabled = false;
                        player.gold_count -= 5;
                        canvas.gold.SetText(player.gold_count.ToString());
                        djc++;
                        goumai.SetActive(true);
                    }
                }
                break;
            default:
                break;
        }

    }

    public void deactive_gun() {
        active_gun.gameObject.SetActive(false);
    }

    public void reactive_gun()
    {
        active_gun.gameObject.SetActive(true);
    }

    private void switch_gun()
    {
        active_gun.gameObject.SetActive(false);
        canvas.gunImage[gun_index].gameObject.SetActive(false);
        gun_index++;
        if(gun_index >= i)
        {
            gun_index = 0;
        }
        canvas.gunImage[gun_index].gameObject.SetActive(true);
        player1.Switch_animator(animatorList[gun_index]);
        active_gun = guns[gun_index];
        active_gun.gameObject.SetActive(true);
        foreach (ammoPickUp ammobox in ammoPickUps)
        {
            ammobox.gunsys = active_gun;
        }
    }

    //for shop using
    public void switch_gun_shop(int index)
    {
        active_gun.gameObject.SetActive(false);
        canvas.gunImage[gun_index].gameObject.SetActive(false);
        gun_index = index;
        if (gun_index >= guns.Count)
        {
            gun_index = 0;
        }
        canvas.gunImage[gun_index].gameObject.SetActive(true);
        player1.Switch_animator(animatorList[gun_index]);
        active_gun = guns[gun_index];
        active_gun.gameObject.SetActive(true);
        foreach (ammoPickUp ammobox in ammoPickUps)
        {
            ammobox.gunsys = active_gun;
        }
    }
}
