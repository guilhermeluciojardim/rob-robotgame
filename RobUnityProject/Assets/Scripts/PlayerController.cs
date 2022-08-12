using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
   //Variables
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private bool dead = false;

    private int health, maxHealth;
    
    private float weaponClip, weaponClipSize;
    private int keys;

    [SerializeField] private Scrollbar healthBar;
    [SerializeField] private Scrollbar weaponBar;

    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private Transform weaponPos;
    [SerializeField] private GameObject weapon;
   
    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    [SerializeField] private float jumpHeight;

   //References
    private CharacterController controller;
    private Animator anim;
    public GameObject explosionShot;

    private void Start(){
        
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        weaponClip = 35; weaponClipSize = 35;
        health = 50; healthBar.size = 0.5f; maxHealth = 100;
        keys=0;
    }

    private void Update(){
        if (!dead){
            Move();
            Aim();
            if (Input.GetKeyDown(KeyCode.Mouse0)){
                Shoot();
                Fire();
            }
        }

    }

    private void OnCollisionEnter(Collision coll){
        if (coll.gameObject.name == "shot_prefab_enemy(Clone)"){
            health -= 1;
            healthBar.size -= 1f / maxHealth;
            GameObject blow = GameObject.Instantiate(explosionShot, coll.transform.position, coll.transform.rotation) as GameObject;
            GameObject.Destroy(blow, 1f);
            Destroy(coll.gameObject);
            if (health <=0){
                Die();
            }
        }
    }

    private void Aim(){
        //Implement way for player to aim up and down
    }

    private void Move(){
        isGrounded = Physics.CheckSphere(transform.position,groundCheckDistance, groundMask);

        if ((isGrounded) && (velocity.y < 0)){
            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");
        
        
        moveDirection = new Vector3(moveX,0,moveZ);
        moveDirection = transform.TransformDirection(moveDirection);

        if (isGrounded){
            if ((moveDirection != Vector3.zero) && (!Input.GetKey(KeyCode.LeftShift))){
            Walk();
            }
            else if ((moveDirection != Vector3.zero) && (Input.GetKey(KeyCode.LeftShift))){
            Run();
            }
            else if (moveDirection == Vector3.zero){
            Idle();
                if (Input.GetKeyDown(KeyCode.Mouse1)){
                    Reload();
                }
            }
            moveDirection *= moveSpeed;

            if (Input.GetKeyDown(KeyCode.Space)){
                Jump();
            }
        }     

        controller.Move(moveDirection * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void Idle(){
        anim.SetFloat("Speed", 0,0.1f,Time.deltaTime);
    }
    private void Walk(){
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f,0.1f,Time.deltaTime);
    }
    private void Run(){
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 1f,0.1f,Time.deltaTime);
    }
    private void Jump(){
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        anim.SetTrigger("Jump");
    }

    private void Shoot(){
        anim.SetTrigger("Shoot");
    }
    private void Reload(){
        anim.SetTrigger("Reload");
        weaponClip = weaponClipSize;
        weaponBar.size = 1f;
    }
    private void Die(){
        anim.SetTrigger("Die");
        dead=true;
    }
    private void Fire(){
        if (weaponClip>0){
            GameObject shot = GameObject.Instantiate(shotPrefab, weaponPos.position,weaponPos.rotation) as GameObject;
		    GameObject.Destroy(shot, 2f);
            weaponClip-=1f;
            weaponBar.size-= 1f / weaponClipSize;
        } 
    }
    public void RecoverHealth(){
        health+=5;
        healthBar.size += 0.05f;
        if (health>100) health=100;
        if (healthBar.size > 1f) healthBar.size=1f;
    }
     public void FindKey(){
        keys+=1;
    }
    
}