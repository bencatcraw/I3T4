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
            other.GetComponentInParent<EnemyController>().health -= 40;
        }
    }

}
