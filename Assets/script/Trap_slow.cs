using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_slow : MonoBehaviour
{

    public float trapSlowdown;
    private bool triggered;
    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            other.GetComponent<Player>().slow_down(trapSlowdown);
            triggered = true;
            StartCoroutine(timmer());
        }
    }

    private IEnumerator timmer()
    {
        yield return new WaitForSeconds(5);
        triggered = false;
    }
}
