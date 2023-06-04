using UnityEngine;

public class ThirdPersonShooterController : MonoBehaviour
{
    //bullet 
    public GameObject bullet;
    [SerializeField] private int m_damage;
    [SerializeField] public int damage
    {
        get{return m_damage;}
        set{damage = damage;}
    }

    //bullet force
    public float shootForce, upwardForce;

    //Gun stats
    public float timeBetweenShooting, spread, timeBetweenShots;
    public int bulletsPerTap;
    public bool allowButtonHold;
    int bulletsShot;

    //bools
    bool shooting;
    public bool readyToShoot;

    //Reference
    public Camera cam;
    public Transform spawnPoint;
    [SerializeField] private LayerMask aimColliderMask = new LayerMask();

    //bug fixing :D
    public bool allowInvoke = true;

    private void Awake()
    {
        readyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        //Check if allowed to hold down button and take corresponding input
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Shooting
        if (readyToShoot && shooting)
        {
            bulletsShot = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        //Find the exact hit position using a raycast
        Vector3 targetPoint = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask))
        {
            targetPoint = raycastHit.point;
        }

        //Find the exact hit position using a raycast
        // Vector3 mousePos = Input.mousePosition;
        // Ray ray = cam.ViewportPointToRay(mousePos); //Just a ray through the middle of your current view
        // RaycastHit hit;
        // Vector3 targetPoint = ray.GetPoint(5f);

        // //check if ray hits something
        // Vector3 targetPoint;
        // if (Physics.Raycast(ray, out hit))
        //     targetPoint = hit.point;
        // else
        //     targetPoint = ray.GetPoint(75); //Just a point far away from the player

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - spawnPoint.position;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        float z = Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, z); //Just add spread to last direction

        //Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, spawnPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
        currentBullet.GetComponent<SpikeProjectile>().parent = gameObject.GetComponent<ThirdPersonShooterController>();

        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(cam.transform.up * upwardForce, ForceMode.Impulse);

        bulletsShot++;

        Destroy(currentBullet, 3f);
        
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;

            //Add recoil to player (should only be called once)
            // playerRb.AddForce(-directionWithSpread.normalized * recoilForce, ForceMode.Impulse);
        }

        if (bulletsShot < bulletsPerTap)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }
}
