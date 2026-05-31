using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // LEVEL 1
            if (CoinManager.instance != null)
            {
                CoinManager.instance.AddCoin();
            }

            // LEVEL 2
            if (CoinManagerLv2.instance != null)
            {
                CoinManagerLv2.instance.AddCoin();
            }

            Destroy(gameObject);
        }
    }
}