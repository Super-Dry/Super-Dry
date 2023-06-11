using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDropper : MonoBehaviour
{
    public GameObject moneyPreFab; //Reference to the money prefab
    public int minCoinsDropped = 1; 
    public int maxCoinsDropped = 3;
    public float maxOffset = 0.5f;

    private void OnDestroy()
    {
        int numCoinsToDrop = Random.Range(minCoinsDropped, maxCoinsDropped + 1);

        for (int i = 0; i < numCoinsToDrop; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-maxOffset, maxOffset), 0f, Random.Range(-maxOffset, maxOffset));

            GameObject coin = Instantiate(moneyPreFab, transform.position + randomOffset, Quaternion.identity);

            coin.transform.position = new Vector3(coin.transform.position.x, 15.25f, coin.transform.position.z);
            coin.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
    }
}
