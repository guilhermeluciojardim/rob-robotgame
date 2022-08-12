using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Transform player;
    public LayerMask whatIsPlayer;

    [SerializeField] int health;


    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //References
    public GameObject shotPrefab;
    public Transform weaponPos;
    public GameObject explosionShot;
    public GameObject explosionOnDeath;

    //Variables

    private bool dead = false;

    private void Awake(){
        player = GameObject.Find("Player").transform;
        health = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead){
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

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

    private void AttackPlayer(){
        //Make enemy look at player
        transform.LookAt(player);
        if (!alreadyAttacked){
            
            Shoot();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack(){
        alreadyAttacked = false;
    }
    
    private void Shoot(){
        GameObject shot = GameObject.Instantiate(shotPrefab, weaponPos.position,weaponPos.rotation) as GameObject;
		GameObject.Destroy(shot, 2f);
    }
    
    private void Die(){
        dead = true;
        GetComponent<Rigidbody>().freezeRotation = true;
        GameObject exp = GameObject.Instantiate(explosionOnDeath, transform.position, transform.rotation) as GameObject;
        GameObject.Destroy(exp, 1f);
        GameObject.Destroy(gameObject, 3.5f);
    }
}
