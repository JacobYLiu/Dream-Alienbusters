using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class enemyAI01 : MonoBehaviour
{
    NavMeshAgent myAgent;

    public LayerMask whatIsGround, whatIsPlayer;
    public Transform player;

    public float health;

    // Guarding
    public Vector3 destinationPoint;
    bool destinationSet;
    public float destinationRange;

    // Chasing
    public float chaseRange;
    private bool playerInChaseRange;


    // Attacking
    public float attackRange, attackTime;
    private bool playerInAttackRange, readyToAttack = true;
    public GameObject projectile;

    // States
    public bool meleeAttacker;

    //animator
    public Animator enemy_veribot_animation_control;


    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        myAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        playerInChaseRange = Physics.CheckSphere(transform.position, chaseRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInChaseRange && !playerInAttackRange) Guarding();
        if (playerInChaseRange && !playerInAttackRange) ChasePlayer();
        if (playerInChaseRange && playerInAttackRange) AttackPlayer();
    }

    private void Guarding()
    {
        if (!destinationSet)
            SearchForDestination();
        else
            myAgent.SetDestination(destinationPoint);


        Vector3 distanceToDestination = transform.position - destinationPoint;

        if (distanceToDestination.magnitude < 1f)
            destinationSet = false;
    }

    private void SearchForDestination()
    {
        // create a random point for our agent to walk to
        float randPositionZ = Random.Range(-destinationRange, destinationRange);
        float randPositionX = Random.Range(-destinationRange, destinationRange);

        // set the destination
        destinationPoint = new Vector3(transform.position.x + randPositionX, transform.position.y, transform.position.z + randPositionZ);

        if(Physics.Raycast(destinationPoint, -transform.up, 2f, whatIsGround))
        {
            destinationSet = true;
        }
    }


    private void ChasePlayer()
    {
        myAgent.SetDestination(player.position);
    }   


    private void AttackPlayer()
    {
        // Make sure enemy doesn't move 

        myAgent.SetDestination(transform.position);
        transform.LookAt(player);

        if (readyToAttack && !meleeAttacker)
        {
            Instantiate(projectile, transform.position, transform.localRotation );
            readyToAttack = false;
            StartCoroutine(ResetAttack());
        }

        

    }

  
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Invoke(nameof(DestroyEnemy), .5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackTime);
        readyToAttack = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
