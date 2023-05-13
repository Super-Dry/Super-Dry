using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public CactusGuy player;
    
    void OnCollisionStay(Collision collision)
    {
        if(collision.collider.name == "CactusGuy")
        {
            Destroy(gameObject);
            print("Player got hit by enemy");
            player.TakeDamage(10);
        }
    }
}
