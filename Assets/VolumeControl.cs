using UnityEngine;

public class VolumeControl : MonoBehaviour
{
    public float maxRotation = 160f; // Maximum rotation angle
    private float currentRotation = 0f; // Current rotation of the knob
    private Vector3 lastMousePosition; // Last mouse position

    void Update()
    {
        // Check for mouse input
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0)) // Left mouse button held down
        {
            RotateKnob();
        }
    }

    void RotateKnob()
    {
        Vector3 mouseDelta = Input.mousePosition - lastMousePosition;

        // Update the current rotation based on the mouse's horizontal movement
        currentRotation += mouseDelta.x;
        currentRotation = Mathf.Clamp(currentRotation, -maxRotation, maxRotation);

        transform.rotation = Quaternion.Euler(0, 0, -currentRotation); // Rotate around the z-axis

        // Map the rotation angle to volume level (from 0 to 1)
        float volumeLevel = (currentRotation + maxRotation) / (2 * maxRotation);
        AudioListener.volume = volumeLevel;

        // Update the last mouse position
        lastMousePosition = Input.mousePosition;
    }
}
