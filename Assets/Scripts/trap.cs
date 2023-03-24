using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    private void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().moveable = false;
            collision.gameObject.GetComponent<EnemyController>().target = this.transform;
        }
    }

}
