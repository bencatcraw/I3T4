using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    
    private Transform target;
    public Transform rotatable;
    public GameObject bulletPrefab;
    public Transform firePoint;
    [Header("Attributes")]
    public float range = 15f;
    public float rotateSpeed = 5f;
    public float fireRate = 1f;
    private float fireCount = 0f;
    private Vector3 defRotation;

    private void Start()
    {
        defRotation = rotatable.position;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(distance < shortestDistance)
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
        
        if (target == null)
        {
            Quaternion idleRot = Quaternion.LookRotation(defRotation);
            Vector3 idle = Quaternion.Lerp(rotatable.rotation,idleRot, Time.deltaTime * rotateSpeed).eulerAngles;
            rotatable.rotation = Quaternion.Euler(0f,idle.y, 0f);
            return; 
        }
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotatable.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
        rotatable.rotation = Quaternion.Euler (0f, rotation.y, 0f);

        if (fireCount <= 0f)
        {
            Shoot();
            fireCount = 1f / fireRate;
        }

        fireCount -= Time.deltaTime;
    }
    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

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
