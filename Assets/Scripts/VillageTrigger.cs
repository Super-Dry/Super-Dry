using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageTrigger : MonoBehaviour
{
    public event EventHandler OnPlayerEnterTrigger;
    public event EventHandler OnPlayerExitTrigger;

    private void OnTriggerEnter(Collider collider)
    {
        CactusGuy player = collider.GetComponent<CactusGuy>();
        if (player != null)
        {
            // Player enter trigger area!
            OnPlayerEnterTrigger?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        CactusGuy player = collider.GetComponent<CactusGuy>();
        if (player != null)
        {
            // Player exit trigger area!
            OnPlayerExitTrigger?.Invoke(this, EventArgs.Empty);
        }
    }
}
