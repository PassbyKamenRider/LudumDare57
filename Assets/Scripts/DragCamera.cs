using UnityEngine;

public class DragCamera : MonoBehaviour
{
    // The last recorded mouse position during dragging
    private Vector3 lastMousePosition;
    // Flag to check if the drag is in progress
    private bool isDragging = false;

    // Adjust this multiplier to control the drag speed
    public float dragSpeed = 0.1f;

    void Update()
    {
        // When the middle mouse button is pressed, start dragging
        if (Input.GetMouseButtonDown(1))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        // When the middle mouse button is released, stop dragging
        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        // If dragging, calculate the mouse movement delta and move the camera
        if (isDragging)
        {
            // Calculate how much the mouse has moved since the last frame
            Vector3 delta = Input.mousePosition - lastMousePosition;
            // Move the camera opposite to the mouse movement
            // For a 2D or orthographic camera, you may only need to move in X and Y
            transform.Translate(-delta.x * dragSpeed, -delta.y * dragSpeed, 0);

            // Update the last mouse position to the current position for the next frame
            lastMousePosition = Input.mousePosition;
        }
    }
}