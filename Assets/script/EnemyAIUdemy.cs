using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;



public class NewBehaviourScript : MonoBehaviour
{
    NavMeshAgent myAgent;
    public LayerMask whatIsGround;

    public Vector3 destinationPoint;
    bool destinationSet;
    public float destinationRange;

    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Guarding();

    }

    private void Guarding()
    {
        if (!destinationSet)
            SearchForDestination();
    }

    private void SearchForDestination()
    {
        float randPositionZ = Random.Range(-destinationRange, destinationRange);
        float randPositionX = Random.Range(-destinationRange, destinationRange);

    }
}
    