using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    public enum BatBehavior
    {
        PatrolAndChase,
        PatrolOnly,
        StationaryAndAttack
    }

    public BatBehavior behavior = BatBehavior.PatrolAndChase;
    public float speed = 2.0f;
    public float flightHeight = 5.0f;
    public float flightDistance = 3.0f;
    public float waveFrequency = 1.0f; // Frecuencia de la onda
    public float waveAmplitude = 0.5f; // Amplitud de la onda
    public Transform player; // Referencia al jugador
    public float detectionRange = 5.0f; // Rango de detección
    public float descentSpeed = 5.0f; // Velocidad de descenso
    public float minHeight = 1.0f; // Altura mínima para no tocar el piso
    public float escapeRange = 10.0f; // Rango de escape

    private Vector3 startPosition;
    private Vector3 attackPosition;
    private bool movingForward = true;
    private bool isChasing = false;
    private bool returningToStart = false;
    private Animator animator; // Referencia al componente Animator

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startPosition.y = flightHeight; // Asegurarse de que el murciélago esté en el aire
        transform.position = startPosition;
        animator = GetComponent<Animator>(); // Obtener el componente Animator
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (behavior)
        {
            case BatBehavior.PatrolAndChase:
                if (distanceToPlayer <= detectionRange && !returningToStart)
                {
                    isChasing = true;
                }
                else if (distanceToPlayer > escapeRange && isChasing)
                {
                    isChasing = false;
                    returningToStart = true;
                }

                if (isChasing)
                {
                    ChasePlayer();
                }
                else if (returningToStart)
                {
                    ReturnToStartPosition();
                }
                else
                {
                    Patrol();
                }
                break;

            case BatBehavior.PatrolOnly:
                Patrol();
                break;

            case BatBehavior.StationaryAndAttack:
                if (distanceToPlayer <= detectionRange && !returningToStart && !isChasing)
                {
                    isChasing = true;
                    attackPosition = player.position; // Guardar la posición inicial del jugador
                }

                if (isChasing)
                {
                    AttackPlayer();
                }
                else if (returningToStart)
                {
                    ReturnToStartPosition();
                }
                else
                {
                    LookAtPlayer(); // Vigilar al jugador
                }
                break;
        }

        // Movimiento ondulado vertical solo si no está persiguiendo ni regresando
        if (!isChasing && !returningToStart)
        {
            Vector3 position = transform.position;
            position.y = startPosition.y + Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;
            transform.position = position;
        }
    }

    private void Patrol()
    {
        // Movimiento hacia adelante y hacia atrás
        if (movingForward)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (Mathf.Abs(transform.position.z - startPosition.z) >= flightDistance)
            {
                movingForward = false;
                transform.Rotate(0, 180, 0); // Girar 180 grados
            }
        }
        else
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime); // Mover hacia adelante después de girar
            if (Mathf.Abs(transform.position.z - startPosition.z) <= 0.1f)
            {
                movingForward = true;
                transform.Rotate(0, 180, 0); // Girar 180 grados
            }
        }
    }

    private void ChasePlayer()
    {
        // Dirección hacia el jugador
        Vector3 direction = (player.position - transform.position).normalized;

        // Rotar hacia la dirección del jugador
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);

        // Movimiento horizontal hacia el jugador
        Vector3 horizontalDirection = new Vector3(direction.x, 0, direction.z);
        transform.Translate(horizontalDirection * speed * Time.deltaTime, Space.World);

        // Movimiento vertical hacia el jugador con velocidad de descenso
        Vector3 position = transform.position;
        float targetHeight = Mathf.Max(player.position.y, minHeight);
        float verticalMovement = Mathf.MoveTowards(position.y, targetHeight, descentSpeed * Time.deltaTime) - position.y;
        position.y += verticalMovement;
        transform.position = position;
    }

    private void AttackPlayer()
    {
        // Movimiento directo hacia la posición inicial del jugador
        transform.position = Vector3.MoveTowards(transform.position, attackPosition, speed * Time.deltaTime);

        // Verificar si ha llegado a la posición inicial del jugador
        if (Vector3.Distance(transform.position, attackPosition) < 0.1f)
        {
            returningToStart = true;
            isChasing = false; // Dejar de atacar
        }
    }

    private void ReturnToStartPosition()
    {
        // Dirección hacia la posición inicial
        Vector3 direction = (startPosition - transform.position).normalized;

        // Movimiento hacia la posición inicial
        transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);

        // Verificar si ha llegado a la posición inicial
        if (Vector3.Distance(transform.position, startPosition) < 0.1f)
        {
            returningToStart = false;
            transform.rotation = Quaternion.identity; // Restaurar la rotación a 0 grados
        }
    }

    private void LookAtPlayer()
    {
        // Hacer que el murciélago mire al jugador
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
    }
}
