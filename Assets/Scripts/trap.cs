using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Destroy(this.gameObject);
            if (other.GetComponent<EnemyController>() != null) { other.GetComponent<EnemyController>().health -= 40; }
            else if (other.GetComponent<RangedEnemy>() != null) { other.GetComponent<RangedEnemy>().health -= 40; }

        }
    }

}
