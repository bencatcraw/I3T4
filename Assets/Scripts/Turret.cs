using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    LineRenderer laser;
    public float fireRate = 1f;
    public float fireTimer;


    public float damage = 5;
    List<GameObject> targets = new List<GameObject>();
    private GameObject target;
    

    private bool ableToShoot = true;
    public float maxHeat = 100;
    public float heat;
    public Image overheatBar;
    void Start()
    {
        laser = GetComponent<LineRenderer>();
        heat = maxHeat;
    }

    // Update is called once per frame
    void Update()
    {
        if(targets.Count > 0)
        {
            FindClosestEnemy();
            Shoot();
        }
        else
        {
            laser.SetPosition(0, transform.position);
            laser.SetPosition(1, transform.position);
        }
        UpdateOverheat(maxHeat, heat);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "enemy")
        {
            targets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            targets.Remove(other.gameObject);
        }
    }
    void FindClosestEnemy()
    {
        float minDist = Mathf.Infinity;
            foreach (GameObject enemy in targets)
            {
                float dist = Vector3.Distance(enemy.transform.position, transform.position);
                if (minDist == -1)
                {
                    minDist = dist;
                }
                if (dist < minDist)
                {
                    minDist = dist; 
                    target = enemy.gameObject;
                }
            }
    }
    public void Shoot()
    {
        laser.SetPosition(0, transform.position);
        laser.SetPosition(1, target.transform.position);
        fireTimer += Time.deltaTime;
        if (fireTimer > 0.5)
        {
            heat -= 0.01f;
        }
        if (fireTimer > fireRate)
        {
            fireTimer = 0;
            if (target.GetComponent<RangedEnemy>() != null)
            {
                target.GetComponentInParent<RangedEnemy>().health -= damage;
            }
            else
            {
                target.GetComponentInParent<EnemyController>().health -= damage;
            }
        }
    }
    public void UpdateOverheat(float maxHeat, float heat)
    {
        if(heat <= 0)
        {
            ableToShoot = false;
        }
        overheatBar.fillAmount = heat / maxHeat;
    }


}
