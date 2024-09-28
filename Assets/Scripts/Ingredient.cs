using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // When the player character collides with an ingredient, he picks the ingredient up
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Destroy the ingredient
            Destroy(gameObject);
            // Add the ingredient to the player's inventory
            if (gameObject.tag == "Planta")
            {
                playerController.plantas.Add(gameObject);
            }
            else if (gameObject.tag == "Piedra")
            {
                playerController.piedras.Add(gameObject);
            }
        }
    }
}
