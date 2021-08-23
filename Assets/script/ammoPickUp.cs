using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoPickUp : MonoBehaviour
{
    // Start is called before the first frame update

    public GunSystem gunsys;
    public Transform player;

    public float pickupRange;


    private void Update()
    {
       
        Vector3 distanceToPlayer = player.position - transform.position;
        if (distanceToPlayer.magnitude <= pickupRange && Input.GetKeyDown(KeyCode.F))
        { 
            gunsys.addAmmo();
        }
    }
}
