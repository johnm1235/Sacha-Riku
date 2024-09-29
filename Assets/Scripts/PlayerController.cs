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
    public CharacterController characterController; // Referencia al componente CharacterController
    private bool canMove = true;

    private Animator animator; // Referencia al componente Animator

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // Obtener el componente Animator
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");  // A/D
        moveZ = Input.GetAxisRaw("Vertical");    // W/S

        if (canMove)
        {
            // Move the player character
            Vector3 moveDirection = new Vector3(0, 0, moveZ);
            moveDirection = moveDirection.normalized;
            Vector3 localMove = transform.TransformDirection(moveDirection);

            characterController.Move(localMove * speed * Time.deltaTime);
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
    // Method to enable or disable movement
    public void EnableMovement(bool enable)
    {
        canMove = enable;
    }
}
