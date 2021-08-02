using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeThrow : MonoBehaviour
{
    public float force = 40f;
    private UI_controller UI_canvas;

    public int grenade_count = 0;
    public GameObject grenade;
    public int max_grenade = 50;
    // Update is called once per frame

    void Start()
    {
        UI_canvas = FindObjectOfType<UI_controller>();
        //for testing
        grenade_count = max_grenade;
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.G) && grenade_count > 0)
        {
            ThrowGrenade();
            grenade_count--;
        }
        UI_canvas.grenadeCount.SetText(grenade_count.ToString());
    }

    public void addGrenade(int number)
    {
        grenade_count += number;
        if(grenade_count > max_grenade)
        {
            grenade_count = max_grenade;
        }
    }

    private void ThrowGrenade()
    {
        GameObject grenadeOut =  Instantiate(grenade, transform.position, transform.rotation);
        Rigidbody rb = grenadeOut.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, ForceMode.VelocityChange);
    }
}
