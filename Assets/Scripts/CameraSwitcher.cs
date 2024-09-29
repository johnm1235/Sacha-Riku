using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera mainCamera;              // Reference to the main camera
    public Camera birdEyeCamera;           // Reference to the bird's-eye view camera
    public KeyCode switchKey = KeyCode.F; // Key to switch between cameras
    public PlayerController playerController;      // Reference to the player's movement script

    private bool isBirdEyeViewActive = false;
    void Start()
    {
        // Ensure the main camera is active and the bird's-eye view camera is inactive at the start
        mainCamera.gameObject.SetActive(true);
        birdEyeCamera.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(switchKey))
        {
            // While the key is held, use the bird's-eye view camera
            mainCamera.gameObject.SetActive(false);
            birdEyeCamera.gameObject.SetActive(true);
            playerController.EnableMovement(false);  // Disable player movement
        }
        else
        {
            // When the key is released, use the main camera again
            mainCamera.gameObject.SetActive(true);
            birdEyeCamera.gameObject.SetActive(false);
            playerController.EnableMovement(true);  // Enable player movement
        }
    }
}
