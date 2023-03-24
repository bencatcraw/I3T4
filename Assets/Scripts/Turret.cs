using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    LineRenderer laser;
    public float fireRate = 1f;
    public float fireTimer;

    public float damage = 5;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        laser = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            laser.SetPosition(0, transform.position);
            laser.SetPosition(1, transform.position);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(target == null)
        {
            target = other.gameObject;
        }
        
        else if (target.gameObject.tag == "enemy" && target != null)
        {
            laser.SetPosition(0, transform.position);
            laser.SetPosition(1, target.transform.position);
            fireTimer += Time.deltaTime;
            if (fireTimer > fireRate)
            {
                fireTimer = 0;
                if(target.GetComponent<RangedEnemy>() != null )
                {
                    target.GetComponent<RangedEnemy>().health -= damage;
                }
                else
                {
                    target.GetComponent<EnemyController>().health -= damage;
                }
                

                
            }
        }
        

    }


}
