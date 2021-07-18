using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_controller : MonoBehaviour
{
    public float speed, bulletTimer;


    public Rigidbody rigidbody_;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Bullet_movement();
        bulletTimer -= Time.deltaTime; // destroy bullet if it fly too long

        if (bulletTimer < 0)
        {
            Destroy(gameObject);
        }
    }

    private void Bullet_movement()
    {
        rigidbody_.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
