using UnityEngine;

// Add to a coin object with a trigger Collider. The player car must have a Collider.
public class CoinPickup : MonoBehaviour
{
    public int coinValue = 5;
    public float spinSpeed = 90f;

    private void Update()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        RaceGameManager manager = FindFirstObjectByType<RaceGameManager>();
        if (manager != null) manager.CollectCoin(coinValue);
        Destroy(gameObject);
    }
}