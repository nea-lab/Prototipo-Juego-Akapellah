/***
 * This script is part of the videogame developed for Akapellah by Nea Lab.
 * This development started in october 2020.
 * Nea Lab team owns all the rgihts of the development, the specific scripts attached to it and the art created.
 * Any replication, resell or attempt or copying is totally illegal.
***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_2 : MonoBehaviour
{
    private float lastShootTime, shootInterval, lifeValue, shootingProbability;
    public GameObject explosionPrefab, eBulletPrefab;
    //public GameUIController gameUiController;
    public GameController gameController;
    private Transform enemyTransform;
    private bool startedShooting;
    private float startTime;
    private float randomStartTime;

    void Start()
    {
        enemyTransform = GetComponent<Transform>();
        shootingProbability = 0.25f;
        lastShootTime = Time.time;
        startedShooting = false;
        startTime = Time.time;
        shootInterval = 3f;
        lifeValue = 1;

        randomStartTime = Random.Range(3f, 5f);
    }

    void Update()
    {
        ShootCountDown();
        EventuallyShoot();
    }

    void ShootCountDown()
    {
        if (!startedShooting)
        {
            if (Time.time - startTime > randomStartTime)
            {
                startedShooting = true;
            }
        }
    }

    void EventuallyShoot()
    {
        if (startedShooting)
        {
            if (Time.time - lastShootTime > shootInterval)
            {
                lastShootTime = Time.time;
                if (UnityEngine.Random.value < shootingProbability)
                {
                    Shoot();
                }
            }
        }
    }

    void Shoot()
    {
        Instantiate(eBulletPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            //LoseLife(0.15f);
            Die();
        }
    }

    /*void LoseLife(float lost)
    {
        lifeValue = lifeValue - lost;

        if (lifeValue <= 0)
        {
            Die();
        }
    }*/

    void Die()
    {
        //gameUiController.OneKill();
        gameController.OneKill(500);
        Destroy(this.gameObject);
        Instantiate(explosionPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }
}
