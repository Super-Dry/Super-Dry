using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using NPC_Interact;
public class NPC_FOV : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;
    //public float attackRange;
    public GameObject playerRef;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;
    private bool isInteracting;
    private string objectName;
    private NPC_Interact interactive;

    //public bool canAttackPlayer;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        isInteracting = false;
        objectName = gameObject.name;
        interactive = transform.Find("ChatUI").GetComponent<NPC_Interact>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        
        while(true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                // if(distanceToTarget < attackRange)
                //     canAttackPlayer = true;
                // else
                //     canAttackPlayer = false;

                if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
                
            }
            else{
                canSeePlayer = false;
                //canAttackPlayer = false;
            }
        }
        else if(canSeePlayer){
            canSeePlayer = false;
            //canAttackPlayer = false;
        }
        if(canSeePlayer){
    		anim.SetBool("isSpeaking", true);
            if(!isInteracting)
            {
                isInteracting = true;
                interactive.Interact(objectName);
            }
    	}else{
    		anim.SetBool("isSpeaking", false);
            if(isInteracting)
            {
                isInteracting = false;
                interactive.Neglect();
            }
    	}
    }
}
