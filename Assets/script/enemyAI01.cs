using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class enemyAI01 : MonoBehaviour
{
    NavMeshAgent myAgent;

    public Animator bossAnimator;

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
    public int Melee_Damage;

    // States
    public bool meleeAttacker;

    //animator
    public Animator enemy_veribot_animation_control;

    //boss
    public bool isBoss;

    int boss_attack_motion_counter = 0;

    private bool dead;

    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        myAgent = GetComponent<NavMeshAgent>();
        dead = false;
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
        if (!dead)
        {
            if (!destinationSet)
                SearchForDestination();
            else
                myAgent.SetDestination(destinationPoint);


            Vector3 distanceToDestination = transform.position - destinationPoint;

            if (distanceToDestination.magnitude < 1f)
                destinationSet = false;

            if (isBoss && distanceToDestination.magnitude >1f)
            {
                bossAnimator.SetTrigger("Walk_Cycle_1");
            }
        }

    }

    private void SearchForDestination()
    {
        // create a random point for our agent to walk to
        if (!dead)
        {
            float randPositionZ = Random.Range(-destinationRange, destinationRange);
            float randPositionX = Random.Range(-destinationRange, destinationRange);

            // set the destination
            destinationPoint = new Vector3(transform.position.x + randPositionX, transform.position.y, transform.position.z + randPositionZ);

            if(Physics.Raycast(destinationPoint, -transform.up, 2f, whatIsGround))
            {
                destinationSet = true;
            }
        }
        
    }


    private void ChasePlayer()
    {
        if (!dead)
        {
            myAgent.SetDestination(player.position);
            if (isBoss)
            {
                bossAnimator.SetTrigger("Walk_Cycle_1");
            }
        }

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
        if(readyToAttack && meleeAttacker && isBoss)
        {
            readyToAttack = false;
            switch (boss_attack_motion_counter)
            {
                case 0:
                    bossAnimator.SetTrigger("Attack_1");
                    break;
                case 1:
                    bossAnimator.SetTrigger("Attack_2");
                    break;
                case 2:
                    bossAnimator.SetTrigger("Attack_3");
                    break;
                case 3:
                    bossAnimator.SetTrigger("Attack_4");
                    break;
                case 4:
                    bossAnimator.SetTrigger("Attack_5");
                    break;
                default:
                    boss_attack_motion_counter = 0;
                    break;
            }

            boss_attack_motion_counter++;
            if(boss_attack_motion_counter > 4)
            {
                boss_attack_motion_counter = 0;
            }
            StartCoroutine(ResetAttack());
        }

        

    }

    public void enemyDead()
    {
        dead = true;
    }

    public void MeleeDamage()
    {
        if (playerInAttackRange)
        {
            player.GetComponent<PlayerHealthSystem>().TakingDamange(Melee_Damage);
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
