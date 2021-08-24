using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    Rigidbody myRigidBody;
    public float upForce, forwardForce;
    public int damageAmount = 3;
    public float bulletTimer;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();

        GrenadeThrow();

    }


    // Update is called once per frame

    private void GrenadeThrow()
    {
        myRigidBody.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
        myRigidBody.AddForce(transform.forward * upForce, ForceMode.Impulse);
    }
    void Update()
    {
        bulletTimer -= Time.deltaTime; // destroy bullet if it fly too long

        if (bulletTimer < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealthSystem>().TakingDamange(damageAmount);
        }
        Destroy(myRigidBody);
    }
}
