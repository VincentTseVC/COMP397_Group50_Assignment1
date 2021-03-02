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

    public int currentHealth;


    public NavMeshAgent navMeshAgent;

    public Transform player;

    public HealthBar healthBar;

    public float attackRadius = 10.0f;

    private Vector3 originalPosition;
    private Animator animator;
    private bool isAttacking = false;
    private bool isNearPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        originalPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {


            float distanceFromPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceFromPlayer <= attackRadius)
                navMeshAgent.SetDestination(player.position);


            float distanceFromOrigin = Vector3.Distance(originalPosition, transform.position);

            if (distanceFromOrigin > attackRadius)
                navMeshAgent.SetDestination(originalPosition);

            if (isNearPlayer == true)
            {
                animator.SetInteger("AnimState", (int)SlimeState.ATTACK);
                animator.SetInteger("AnimState", (int)SlimeState.IDLE);
            //StartCoroutine(Attack());
             }
 




        // testing
        //if (Input.GetKeyDown(KeyCode.A))
        //    TakeDamange(20);

    }

    public void TakeDamange(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        animator.SetInteger("AnimState", (int)SlimeState.HURT);
        if(currentHealth <= 0)
        {
            StartCoroutine(Death());
        }
        animator.SetInteger("AnimState", (int)SlimeState.IDLE);

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
        isAttacking = true;
        animator.SetInteger("AnimState", (int)SlimeState.ATTACK);
        yield return new WaitForSeconds(300 * Time.deltaTime);
        isAttacking = false;
        animator.SetInteger("AnimState", (int)SlimeState.IDLE);
    }

    IEnumerator Death()
    {
        animator.SetInteger("AnimState", (int)SlimeState.DIE);
        yield return new WaitForSeconds(300 * Time.deltaTime);
        Destroy(this.gameObject);
    }
}
