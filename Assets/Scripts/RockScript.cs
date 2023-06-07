using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    [SerializeField] private Material rockGlowMaterial;
    [SerializeField] private Material rockMaterial;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private GameObject tornadoSupply;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform target;
    [SerializeField] private float supplySpead;
    [SerializeField] private float destoryTime;

    public event EventHandler rockDestoryed;

    private bool glow = false;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyHealth.cantBeDamage = true;
        enemyHealth.healthbar.gameObject.SetActive(false);
    }

    void Update()
    {
        if(enemyHealth.IsDead())
        {
            DestroyRock();
        }
    }

    public void rockSwitch()
    {
        glow = !glow;
        rockGlow();
    }

    void rockGlow()
    {
        if(glow)
        {
            meshRenderer.material = rockGlowMaterial; 
            InvokeRepeating("supply", 3f, 1f);
        }else{
            CancelInvoke();            
            meshRenderer.material = rockMaterial;
        }
    }

    void supply()
    {
        GameObject tornadoObj = Instantiate(tornadoSupply, shootPoint.position, Quaternion.identity);
        Vector3 shootingDirection = target.transform.position - shootPoint.position + new Vector3(0, 3, 0);
        tornadoObj.transform.forward = shootingDirection.normalized;
        tornadoObj.GetComponent<Rigidbody>().AddForce(shootingDirection.normalized * supplySpead, ForceMode.Impulse);
        Destroy(tornadoObj, destoryTime);
    }

    private void DestroyRock()
    {
        Destroy(gameObject);
        rockDestoryed?.Invoke(this, EventArgs.Empty);
    }


}
