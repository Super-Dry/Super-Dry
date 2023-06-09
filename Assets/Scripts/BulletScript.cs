using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{  
    public EnemyProjectile parent;

    void OnCollisionStay(Collision collision)
    {
        CactusGuy player = collision.gameObject.GetComponent<CactusGuy>();
        if(player != null)
        {
            Destroy(gameObject);
            // print("Player got hit by enemy");
            player.TakeDamage(parent.damage);
        }else if(collision.gameObject.tag == "Ground"){
            Destroy(gameObject);
        }else{
            Destroy(gameObject,5f);
        }
    }
}
