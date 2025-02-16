using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float score = 0;

    // A reference to our ballController
    [SerializeField] private BallController ball;

    // A reference for our PinCollection prefab
    [SerializeField] private GameObject pinCollection;

    // A reference for an empty GameObject used to spawn our pin collection prefab
    [SerializeField] private Transform pinAnchor;

    // A reference for our input manager
    [SerializeField] private InputManager inputManager;

    [SerializeField] private TextMeshProUGUI scoreText;

    private FallTrigger[] fallTriggers;
    private GameObject pinObjects;

    private void Start()
    {
        // Prevent duplicate event listeners
        inputManager.OnResetPressed.RemoveListener(HandleReset);
        inputManager.OnResetPressed.AddListener(HandleReset);

        SetPins();
    }

    private void HandleReset()
    {
        // Do NOT reset the score here
        // Just reset the pins and ball
        if (pinObjects)
        {
            Destroy(pinObjects);
            pinObjects = null; // Clear reference to prevent multiple instances
        }

        ball.ResetBall();
        SetPins();
    }

    private void SetPins()
    {
        // Ensure old pins are destroyed before spawning new ones
        if (pinObjects)
        {
            foreach (Transform child in pinObjects.transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(pinObjects);
            pinObjects = null;
        }

        // Instantiate a new set of pins
        pinObjects = Instantiate(pinCollection, pinAnchor.transform.position, Quaternion.identity, transform);

        // Get all pin FallTriggers
        fallTriggers = FindObjectsByType<FallTrigger>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        // Remove previous event listeners to prevent duplicate score updates
        foreach (FallTrigger pin in fallTriggers)
        {
            pin.OnPinFall.RemoveListener(IncrementScore);
            pin.OnPinFall.AddListener(IncrementScore);
        }
    }

    private void IncrementScore()
    {
        score++;
        scoreText.text = $"Score: {score}";
    }
}
