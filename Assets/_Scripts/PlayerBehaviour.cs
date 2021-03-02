using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Vincent Tse.
 * 2021-02-13
 * 
 * Siying Li,
 * 2021-03-02
 */
public enum PaladinState
{
    IDLE,
    RUN,
    JUMP,
    SLASH
}

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Controllers")]
    public CharacterController controller;

    [Header("Movement Settings")]
    public float maxSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 3.0f;
    public Vector3 velocity;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundRadius = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    public GameController gameController;

    public AudioSource jump;
    private AudioSource _jump;

    [Header("UI")]
    public GameObject inventory;
    private bool inventoryActive = false;


    [Header("Armor")]
    public bool gotShield = false;
    public GameObject shield;

    public bool gotSword = false;
    public GameObject sword;

    [Header("Animation")]
    public Animator animator;

    private bool isAttacking;




    void Start()
    {
        controller = GetComponent<CharacterController>();
        _jump = gameController.audioSources[(int)SoundClip.JUMP];

        inventory.SetActive(inventoryActive);
    }


    void Update()
    {
        if(isAttacking == false)
        {


        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2.0f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if(x ==0 || z == 0 )
        {
            animator.SetInteger("AnimState", (int)PaladinState.IDLE);
        }

        if (x != 0 || z != 0)
        {
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * maxSpeed * Time.deltaTime);
            animator.SetInteger("AnimState", (int)PaladinState.RUN);

        }

        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
            animator.SetInteger("AnimState", (int)PaladinState.JUMP);

            //play jump audio
            jump.Play();

        }

        velocity.y += gravity * Time.deltaTime;

            //Debug.Log(velocity);

        }

        // not on platform
        if (transform.parent == null)
            controller.Move(velocity * Time.deltaTime);




        // open or close inventory UI
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActive = !inventoryActive;
            inventory.SetActive(inventoryActive);
        }

        //slash
        if (gotSword && Input.GetMouseButton(0) && isAttacking == false)
        {
            StartCoroutine(Slash());
        }




    }
    IEnumerator Slash()
    {
        Debug.Log("Attacking");
        isAttacking = true;
        animator.SetInteger("AnimState", (int)PaladinState.SLASH);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        isAttacking = false;
        Debug.Log("Finished Attacking");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Platform")
            transform.parent = other.gameObject.transform;

        if (other.gameObject.CompareTag("Enemy") && isAttacking == true){
            other.gameObject.GetComponent<EnemyBehaviour>().TakeDamange(10);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Platform")
            transform.parent = null;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Sword")
        {
            Debug.Log("Near Sword");
            //pick up sword
            if (Input.GetKeyDown(KeyCode.F) && gotSword == false)
            {
                Debug.Log("Picking up sword");
                sword.SetActive(true);
                other.gameObject.SetActive(false);
                gotSword = true;

            }

            if (other.gameObject.CompareTag("Enemy") && isAttacking == true)
            {
                other.gameObject.GetComponent<EnemyBehaviour>().TakeDamange(10);
            }


        }

        if (other.gameObject.tag == "Shield")
        {
            Debug.Log("Near Shield");
            //pick up sword
            if (Input.GetKeyDown(KeyCode.F) && gotShield == false)
            {
                Debug.Log("Picking up Shield");
                shield.SetActive(true);
                other.gameObject.SetActive(false);
                gotShield = true;

            }


        }
    }


}
