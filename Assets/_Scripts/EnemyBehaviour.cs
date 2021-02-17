using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/**
 * Vincent Tse.
 * 2021-02-13
 */
public class EnemyBehaviour : MonoBehaviour
{

    public int maxHealth = 100;

    public int currentHealth;


    public NavMeshAgent navMeshAgent;

    public Transform player;

    public HealthBar healthBar;

    public float attackRadius = 10.0f;

    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        healthBar.SetMaxHealth(maxHealth);

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








        // testing
        //if (Input.GetKeyDown(KeyCode.A))
        //    TakeDamange(20);

    }

    void TakeDamange(int damage)
    {
        currentHealth -= damage;

        healthBar.SetMaxHealth(currentHealth);
    }
}
