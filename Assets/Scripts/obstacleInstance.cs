using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleInstance : MonoBehaviour
{
    public float dist;
    float damage = 30f;
    private GameObject player;
    private float enterTime;
    public float maxDist = 15f;
    public bool damaged;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enterTime = Time.time;
    }
    private void Update()
    {
        dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist <= maxDist && Time.time >= enterTime + 1.9f && damaged == false)
        {
            damaged = true;
            player.GetComponent<PlayerController>().PlayerHealth -= damage;
            player.GetComponent<PlayerController>().Invoke("Hurt", 0f);
        }
    }
}
