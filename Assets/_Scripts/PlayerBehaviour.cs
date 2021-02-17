using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Vincent Tse.
 * 2021-02-13
 */


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





    void Start()
    {
        controller = GetComponent<CharacterController>();
        _jump = gameController.audioSources[(int)SoundClip.JUMP];

        inventory.SetActive(inventoryActive);
    }


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2.0f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (x != 0 || z != 0)
        {
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * maxSpeed * Time.deltaTime);
        }

        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);

            //play jump audio
            jump.Play();

        }

        velocity.y += gravity * Time.deltaTime;

        //Debug.Log(velocity);

        // not on platform
        if (transform.parent == null)
            controller.Move(velocity * Time.deltaTime);




        // open or close inventory UI
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActive = !inventoryActive;
            inventory.SetActive(inventoryActive);
        }


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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Platform")
            transform.parent = null;
    }


}
