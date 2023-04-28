
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;
    public float damage = 10f;
    public GameObject impactEffect;
    public void Seek(Transform _target)
    {
        target = _target;
    }
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distancePerFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distancePerFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distancePerFrame, Space.World);
    }
    void HitTarget()
    {
        GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        if (target.GetComponent<RangedEnemy>() != null)
        {
            target.GetComponent<RangedEnemy>().health -= damage;
            Destroy(this.gameObject);
            return;
        }
        if (target.GetComponentInParent<RangedEnemy>() != null)
        {
            target.GetComponentInParent<RangedEnemy>().health -= damage;
            Destroy(this.gameObject);
            return;
        }
        if (target.GetComponent<EnemyController>() != null)
        {
            target.GetComponent<EnemyController>().health -= damage;
            Destroy(this.gameObject);
            return;
        }
        if (target.GetComponentInParent<EnemyController>() != null)
        {
            target.GetComponentInParent<EnemyController>().health -= damage;
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }
        Destroy(effectIns, 2f);
    }
}
