using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{

    public int maxHealth = 100;

    public int currentHealth;


    public NavMeshAgent navMeshAgent;

    public Transform player;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        healthBar.SetMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(player.position);

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
