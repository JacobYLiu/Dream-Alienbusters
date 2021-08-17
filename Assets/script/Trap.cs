using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // Start is called before the first frame update

    public int trapDamage;
    
    private bool triggered;
    void Start()
    {
        gameObject.GetComponent<Animation>().Play("Anim_TrapNeedle_Hide");
        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ! triggered){
            other.GetComponent<PlayerHealthSystem>().TakingDamange(trapDamage);
            gameObject.GetComponent<Animation>().Play("Anim_TrapNeedle_Show");
            triggered = true;
            StartCoroutine(timmer());
        }
    }

    private IEnumerator timmer()
    {
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<Animation>().Play("Anim_TrapNeedle_Hide");
        triggered = false;
    }
}
