using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    public event EventHandler OnPlayerEnterTrigger;

    private void OnTriggerStay(Collider collider)
    {
        CactusGuy player = collider.GetComponent<CactusGuy>();
        if (player != null)
        {
            // Player inside trigger area!
            OnPlayerEnterTrigger?.Invoke(this, EventArgs.Empty);
        }
    }
}
