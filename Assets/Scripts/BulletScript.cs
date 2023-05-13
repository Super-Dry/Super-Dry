using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{  
    void OnCollisionStay(Collision collision)
    {
        CactusGuy player = collision.gameObject.GetComponent<CactusGuy>();
        if(player != null)
        {
            Destroy(gameObject);
            print("Player got hit by enemy");
            player.TakeDamage(10);
        }
    }
}
