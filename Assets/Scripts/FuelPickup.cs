using DG.Tweening;
using UnityEngine;

public class FuelPickup : MonoBehaviour
{
    [SerializeField] float fuelAmount = 20f; // Amount of fuel to add
    [SerializeField] Vector3 desiredPosition;
    [SerializeField] float cycleLength;

    private void Start()
    {
        transform.DOMove(desiredPosition, cycleLength).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        transform.DOLocalRotate(new Vector3(0, 360, 0), 5, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.CollectPlay();

            // Add fuel to the player's currentFuel
            Movement.Instance.currentFuel += fuelAmount;

            // Clamp currentFuel to maxFuel
            Movement.Instance.currentFuel = Mathf.Clamp(Movement.Instance.currentFuel, 0f, Movement.Instance.maxFuel);

            // Update the fuel bar UI
            Movement.Instance.UpdateFuelBar();

            gameObject.SetActive(false);
        }
    }
}
