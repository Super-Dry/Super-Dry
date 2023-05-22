using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageTrigger : MonoBehaviour
{
    public event EventHandler OnPlayerEnterTrigger;

    private void OnTriggerEnter(Collider collider)
    {
        CactusGuy player = collider.GetComponent<CactusGuy>();
        if (player != null)
        {
            // Player inside trigger area!
            OnPlayerEnterTrigger?.Invoke(this, EventArgs.Empty);
        }
    }
}
