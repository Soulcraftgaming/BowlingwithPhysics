using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public UnityEvent<Vector2> OnMove = new UnityEvent<Vector2>(); // Event for movement input
    public UnityEvent OnSpacePressed = new UnityEvent(); // Event for space key press

    private void Update()
    {
        // Check for space key press and invoke OnSpacePressed event
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacePressed?.Invoke();
        }

        // Capture player movement input
        Vector2 input = Vector2.zero;
        if (Input.GetKey(KeyCode.A)) // Move left
        {
            input += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D)) // Move right
        {
            input += Vector2.right;
        }
        OnMove?.Invoke(input); // Invoke the movement event with input
    }
}
