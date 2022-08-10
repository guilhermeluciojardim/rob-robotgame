using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] int health;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //References
    private Animator anim;
    public GameObject shotPrefab;
    public Transform weaponPos;
    private CharacterController controller;
    public GameObject explosionShot;
    public GameObject explosionOnDeath;

    //Variables

    private bool dead = false;

    private void Awake(){
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead){
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange){
                Patroling();
            }
            if (playerInSightRange && !playerInAttackRange){
                ChasePlayer();
            }
            if (playerInAttackRange && playerInSightRange){
                AttackPlayer();
            }
        }
      
    }

    private void OnCollisionEnter(Collision coll){
        if (coll.gameObject.name == "shot_prefab(Clone)"){
            health -= 1;
            GameObject blow = GameObject.Instantiate(explosionShot, coll.transform.position, coll.transform.rotation) as GameObject;
            GameObject.Destroy(blow, 1f);
            Destroy(coll.gameObject);
            if (health <=0){
                Die();
            }
        }
    }
    
    private void Patroling(){
        Walk();
        if (!walkPointSet){
            SearchWalkPoint();
        }
        if (walkPointSet){
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        //WalkPoint reached
        if (distanceToWalkPoint.magnitude < 1f){
            walkPointSet = false;
        }

    }
    private void ChasePlayer(){
        agent.SetDestination(player.position);
        Run();
    }
    private void AttackPlayer(){
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        Idle();
        //Make enemy look at player
        transform.LookAt(player);

        if (!alreadyAttacked){
            //Attack code
            Shoot();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack(){
        alreadyAttacked = false;
    }
    private void SearchWalkPoint(){
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        
    walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)){
            walkPointSet = true;
        }
    }
    private void Idle(){
        anim.SetFloat("Speed", 0,0.1f,Time.deltaTime);
    }
    private void Walk(){
        
        anim.SetFloat("Speed", 0.5f,0.1f,Time.deltaTime);
    }
    private void Run(){
        
        anim.SetFloat("Speed", 1f,0.1f,Time.deltaTime);
    }
    private void Shoot(){
        anim.SetTrigger("Shoot");
        GameObject shot = GameObject.Instantiate(shotPrefab, weaponPos.position,weaponPos.rotation) as GameObject;
		GameObject.Destroy(shot, 2f);
    }
    private void Reload(){
        anim.SetTrigger("Reload");
    }

    private void Die(){
        anim.SetTrigger("Die");
        dead = true;
        GameObject exp = GameObject.Instantiate(explosionOnDeath, transform.position, transform.rotation) as GameObject;
        GameObject.Destroy(exp, 1f);
        GameObject.Destroy(gameObject, 3.5f);
    }

}
