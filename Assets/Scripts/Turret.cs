using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Turret : MonoBehaviour
{

    private Transform target;
    public Transform rotatable;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public bool ableToShoot = true;
    [Header("Attributes")]
    public float range = 15f;
    public float rotateSpeed = 5f;
    public float fireRate = 1f;
    private float fireCount = 0f;
    private Quaternion defRotation;

    [SerializeField] private AudioSource turShoot;
    private void Start()
    {
        defRotation = rotatable.transform.rotation;
        
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
    }
    private void Update()
    {
        if (ableToShoot == true)
        {

            if (target == null)
            {
                Vector3 idle = Quaternion.Lerp(rotatable.rotation, defRotation, Time.deltaTime * rotateSpeed).eulerAngles;
                rotatable.rotation = Quaternion.Euler(0f, idle.y, 0f);
                return;
            }
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(rotatable.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
            rotatable.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            if (fireCount <= 0f)
            {
                Shoot();
                fireCount = 1f / fireRate;
                if (this.gameObject.GetComponent<HeatSystem>()) { this.gameObject.GetComponent<HeatSystem>().heat -= 5f; }
            }


            fireCount -= Time.deltaTime;
        }
    }
    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        turShoot.Play();
        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }
    
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
