using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneySystem : MonoBehaviour
{
    public int moneyAmount = 0; //The initial amount of money
    public Image coinImage; //Reference to the Image component displaying the coin
    public TMP_Text moneyText; //Reference to the Text component displaying the money amount

    public void UpdateMoneyAmount(int amount)
    {
        moneyAmount += amount;
        moneyText.text = moneyAmount.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
