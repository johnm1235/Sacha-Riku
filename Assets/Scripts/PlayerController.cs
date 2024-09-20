using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    float moveX = 0;
    float moveZ = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");  // A/D
        moveZ = Input.GetAxis("Vertical");    // W/S
                                              // Move the player character
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ);
        transform.Translate(moveDirection * Time.deltaTime * 5f);  // Adjust speed as necessary
    }
}
