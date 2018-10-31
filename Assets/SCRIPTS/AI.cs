﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {

    [Header("Radius Values")]
    public float OuterRadius;
    public float InnerRadius;

    [Header("AI Values")]
    public float AIBulletSpeed;
    public float AIFireRate;
    public float AIHealth;
    public float AIRotationSpeed;

    [Header("Damage Values")]
    public float BulletDamage;
    public float PelletDamage;
    public float SwordDamage;
    public float LaserDamage;

    [Header("Game Objects")]
    public Transform Barrel;
    public GameObject Bullet;

    [Header("UI Objects")]
    public GameObject AIHealthBar;

    private GameObject Player;

    private float time;

    private Transform target;
    private NavMeshAgent agent;

    void Start ()
    {
        // remove this later
        Player = GameObject.FindGameObjectWithTag("Player");

        // set target 
        target = Player.transform;
        agent = GetComponent<NavMeshAgent>();
    }
	
	void Update ()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        FaceTarget();

        // follows player
        if (distance <= OuterRadius)
        {
            agent.SetDestination(target.position);

            time += Time.deltaTime;

            // attack the player
            if (time >= AIFireRate)
            {
                GameObject bullet = Instantiate(Bullet, Barrel.position, Barrel.rotation) as GameObject;
                bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * AIBulletSpeed);
                bullet.transform.Rotate(90, 0, 0);

                time = 0;
            }

            // personal space
            if (distance <= InnerRadius)
            {
                agent.SetDestination(transform.position);
            }
        }

        if (AIHealth < 0)
        {
            Destroy(this.gameObject);
        }
	}

    private void FaceTarget()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0f, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * AIRotationSpeed);
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.collider.tag == "Bullet")
            AIHealth -= BulletDamage;

        if (collision.collider.tag == "Pellet")
            AIHealth -= PelletDamage;

        if (collision.collider.tag == "Laser")
            AIHealth -= LaserDamage;

        if (collision.collider.tag == "Block")
            AIHealth -= SwordDamage;

        AIHealthBar.transform.localScale = new Vector3(0.1f, 0.1f, AIHealth);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, OuterRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, InnerRadius);
    }
}