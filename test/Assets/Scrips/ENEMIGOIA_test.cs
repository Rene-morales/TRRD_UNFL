using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ENEMIGOIA_test : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;  // Referencia al jugador
    public LayerMask whatIsGround, whatIsPlayer;

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // Enemy Health
    public int maxHealth = 2;
    private float currentHealth;

    // Da�o del enemigo
    public int enemyDamage; // Cantidad de da�o que el enemigo hace al jugador

    private void Awake()
    {
        player = GameObject.Find("player").transform;
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth; // Inicializamos la salud del enemigo
    }

    private void Update()
    {
        // Comprobamos si el jugador est� en rango de vista o de ataque
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // Actualizamos el estado de la IA basado en la posici�n del jugador
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange) AttackPlayer();
      
        // Comprobamos si la salud del enemigo es 0 o menor, y lo destruimos
       

    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Si hemos alcanzado el walkpoint, buscamos uno nuevo
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        // Calculamos un punto aleatorio dentro del rango
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        // Aseguramos que el enemigo no se mueva mientras ataca
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            // Llamamos a ResetAttack despu�s de un tiempo
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

            // Aqu� puedes agregar la l�gica para hacer da�o al jugador
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(enemyDamage);  // Aplicamos da�o al jugador
                Debug.Log("El enemigo aplica da�o: " + enemyDamage);
            }
            else
            {
                Debug.LogError("No se encontr� el componente PlayerHealth en el jugador.");
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    // M�todo para recibir da�o
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0) Debug.Log("El enemigo ha muerto.");
        Destroy(gameObject); ; // Reducimos la salud por la cantidad de da�o
        Debug.Log("Enemigo recibi� " + damage + " de da�o. Salud actual: " + currentHealth);
    }

    // M�todo para destruir al enemigo cuando la salud llega a 0

    // Detecta la colisi�n con el jugador y le hace da�o
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Llamamos al m�todo que hace da�o al jugador
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(enemyDamage);  // Aplicamos da�o al jugador
                Debug.Log("El jugador recibi� da�o del enemigo: " + enemyDamage);
            }
            else
            {
                Debug.LogError("No se encontr� el componente PlayerHealth en el jugador.");
            }
        }
    }
}