using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicMove : MonoBehaviour
{
    Animator myAnimator;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        player = FindObjectOfType<Player>().transform;
        //myAnimator.SetBool("move", true);

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
    }
}
