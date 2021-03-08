using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/**
 * Vincent Tse.
 * 2021-02-13
 * 
 *Siying Li,
 * 2021-03-02
 */

public enum SlimeState
{
    IDLE,
    ATTACK,
    HURT,
    DIE
}
public class EnemyBehaviour : MonoBehaviour
{

    public int maxHealth = 100;

    public int currentHealth = 100;


    public NavMeshAgent navMeshAgent;

    public Transform player;
    public PlayerBehaviour playerBehaviour;

    public HealthBar healthBar;

    public float attackRadius = 10.0f;
    public AudioSource attack;

    private Vector3 originalPosition;
    private Animator animator;
    private bool canAttack = true;
    private bool canMove = true;
    private bool isNearPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        healthBar.SetMaxHealth(maxHealth);
        //currentHealth = maxHealth;
        originalPosition = transform.position;
        playerBehaviour = player.GetComponent<PlayerBehaviour>();
        healthBar.SetHealth(currentHealth);

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (canMove)
        {
            float distanceFromPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceFromPlayer <= attackRadius)
                navMeshAgent.SetDestination(player.position);


            float distanceFromOrigin = Vector3.Distance(originalPosition, transform.position);

            if (distanceFromOrigin > attackRadius)
                navMeshAgent.SetDestination(originalPosition);
        }


            if (isNearPlayer == true && canAttack)
            {
                StartCoroutine(Attack());
             }
 




        // testing
        //if (Input.GetKeyDown(KeyCode.A))
        //    TakeDamange(20);

    }


    public void TakeDamange(int damage)
    {
        canAttack = false;
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        //StartCoroutine(Hurt());
        
        if(currentHealth <= 0)
        {
            StartCoroutine(Death());
        }
        canAttack = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isNearPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            isNearPlayer = false;
        }
    }


    IEnumerator Attack()
    {
        canAttack = false;
        animator.SetInteger("AnimState", (int)SlimeState.ATTACK);
        playerBehaviour.TakeDamange(5);
        attack.Play();
        yield return new WaitForSeconds(1f);
        animator.SetInteger("AnimState", (int)SlimeState.IDLE);
        canAttack = true;
    }

    IEnumerator Hurt()
    {
        animator.SetInteger("AnimState", (int)SlimeState.HURT);
        yield return new WaitForSeconds(1);
        animator.SetInteger("AnimState", (int)SlimeState.IDLE);
    }

    IEnumerator Death()
    {
        canMove = false;

        Debug.Log("Mob Dying");
        animator.SetInteger("AnimState", (int)SlimeState.DIE);
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
