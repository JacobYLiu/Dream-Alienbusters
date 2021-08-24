using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jiaobenjianyong : MonoBehaviour
{
    public Player player;
    public WeaponSwitchSystem WeaponSwitchSystem;
    // Start is called before the first frame update
   public void jy()
    {
        player.enabled = false;
        WeaponSwitchSystem.enabled = false;
    }

    // Update is called once per frame
   public void kq()
    {
        player.enabled = true;
        WeaponSwitchSystem.enabled = true;
    }
}
