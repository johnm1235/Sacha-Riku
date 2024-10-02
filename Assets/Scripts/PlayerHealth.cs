using UnityEngine;
using UnityEngine.UI;  // Required for UI elements

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;                  // Maximum health the player can have
    private int currentHealth;                 // Current health value
    public Image healthBarImage;               // Reference to the health bar UI Image
    public GameObject gameOverPanel;           // Reference to the Game Over UI Panel

    void Start()
    {
        currentHealth = maxHealth;             // Initialize the player with full health
        UpdateHealthBar();                     // Update health bar at the start
        gameOverPanel.SetActive(false);        // Ensure the Game Over panel is hidden at the start
    }

    // Method to handle damage taken by the player
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;               // Subtract damage from current health
        UpdateHealthBar();                     // Update health bar UI

        // Check if the player's health has reached zero
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameOver();                        // Trigger Game Over logic if health is zero
        }
    }

    // Update the health bar UI based on the current health
    private void UpdateHealthBar()
    {
        float fillAmount = (float)currentHealth / maxHealth;  // Calculate the fill amount as a percentage
        healthBarImage.fillAmount = fillAmount;               // Update the UI Image's fill amount
    }

    // Method to handle Game Over logic
    private void GameOver()
    {
        gameOverPanel.SetActive(true);          // Display the Game Over UI
        Time.timeScale = 0f;                    // Pause the game (optional)
        // Additional game over logic can be added here, such as restarting the level
    }

    // Detect collision with enemies (e.g., bat)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))  // Check if the object colliding is tagged as "Enemy"
        {
            Debug.Log("Player hit by enemy!");    // Log the collision
            TakeDamage(1);                        // Player takes 1 damage
        }
    }
}

