using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f; // Nueva variable para la velocidad de rotación
    float moveX = 0;
    float moveZ = 0;
    public List<GameObject> plantas = new List<GameObject>();
    public List<GameObject> piedras = new List<GameObject>();

    private Animator animator; // Referencia al componente Animator

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // Obtener el componente Animator
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");  // A/D
        moveZ = Input.GetAxis("Vertical");    // W/S

        // Move the player character
        Vector3 moveDirection = new Vector3(0, 0, moveZ);
        transform.Translate(moveDirection * Time.deltaTime * speed);  // Adjust speed as necessary

        // Rotate the player character to face the direction of horizontal movement
        if (moveX != 0)
        {
            float rotation = moveX * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotation, 0);
        }

        // Actualizar el parámetro isWalking en el Animator
        bool isWalking = moveX != 0 || moveZ != 0;
        animator.SetBool("isWalking", isWalking);
    }
}
