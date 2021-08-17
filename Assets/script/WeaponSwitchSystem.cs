using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchSystem : MonoBehaviour
{
    Player player1;
    private GunSystem active_gun;
    public List<GunSystem> guns = new List<GunSystem>();
    public int gun_index;
    public List<Animator> animatorList;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GunSystem gun in guns)
        {
            gun.gameObject.SetActive(false);
        }
        player1 = GetComponentInParent<Player>();
        active_gun = guns[gun_index];
        active_gun.gameObject.SetActive(true);
        player1.Switch_animator(animatorList[gun_index]);
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
        gun_index++;
        if(gun_index >= guns.Count)
        {
            gun_index = 0;
        }
        player1.Switch_animator(animatorList[gun_index]);
        active_gun = guns[gun_index];
        active_gun.gameObject.SetActive(true);
    }
}
