using UnityEngine;
using UnityEngine.UI;

public class ThrustButtonController : MonoBehaviour
{
    [SerializeField] Movement _movement;
    public Button thrustButton;

    private void Start()
    {
        thrustButton = GetComponent<Button>();
        // Add listeners for button press and release events
        thrustButton.onClick.AddListener(OnThrustButtonDown);
        thrustButton.onClick.AddListener(OnThrustButtonUp);
    }

    private void OnThrustButtonDown()
    {
        // Notify the PlayerController script that the thrust button is pressed
        _movement.StartThrusting();
    }

    private void OnThrustButtonUp()
    {
        // Notify the PlayerController script that the thrust button is released
        _movement.StopThrusting();
    }
}

