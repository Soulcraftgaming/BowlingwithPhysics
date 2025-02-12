using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private InputManager inputManager; // Reference to InputManager
    [SerializeField] private float speed = 5f; // Movement speed

    void Start()
    {
        // Initialize Rigidbody and InputManager
        rb = GetComponent<Rigidbody>();

        // Ensure InputManager is properly referenced in the scene
        inputManager = Object.FindFirstObjectByType<InputManager>();

        // Subscribe to InputManager events
        inputManager.OnMove.AddListener(MovePlayer);
    }

    void MovePlayer(Vector2 direction)
    {
        // Correctly move the player based on input from InputManager
        Vector3 moveDirection = new(direction.x, 0f, direction.y); // Use X and Z for 3D movement
        rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed); // Apply movement velocity
    }
}
