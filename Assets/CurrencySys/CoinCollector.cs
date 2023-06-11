using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public MoneySystem moneySystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            moneySystem.UpdateMoneyAmount(1); //Increase by 1
            Destroy(gameObject);
        }
    }
}
