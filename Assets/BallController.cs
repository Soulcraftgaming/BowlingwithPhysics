using UnityEngine;
using UnityEngine.Events;

public class BallController : MonoBehaviour
{
    [SerializeField] private float force = 5f;
    [SerializeField] private Transform ballAnchor;
    private bool isBallLaunched;
    private Rigidbody ballRB;
    private InputManager inputManager;

    void Start()
    {
        ballRB = GetComponent<Rigidbody>();

        // Use the new method to find InputManager
        inputManager = Object.FindFirstObjectByType<InputManager>(); 
        if (inputManager != null)
        {
            inputManager.OnSpacePressed.AddListener(LaunchBall);
        }
        else
        {
            Debug.LogWarning("InputManager not found!");
        }

        // Attach ball to the anchor so it follows the player
        transform.parent = ballAnchor;
        transform.localPosition = Vector3.zero;
        ballRB.isKinematic = true;
    }

    private void LaunchBall()
    {
        if (isBallLaunched) return;
        isBallLaunched = true;

        // Detach ball from player
        transform.parent = null;
        ballRB.isKinematic = false;

        // Apply force to launch the ball
        Debug.Log("Ball Launch Direction: " + transform.forward);
        ballRB.AddForce(transform.forward * force, ForceMode.Impulse);
    }
}
