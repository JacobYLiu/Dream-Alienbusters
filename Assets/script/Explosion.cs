using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int explosionDamage;
    // Start is called before the first frame update

    private void Start()
    {
        StartCoroutine(DestoryObj());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<enemyHealthSystem>().TakingDamange(explosionDamage);
        }
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthSystem>().TakingDamange(explosionDamage/2);
        }
        if (other.CompareTag("Boss"))
        {

        }
    }

    IEnumerator DestoryObj()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
