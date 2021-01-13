/***
 * This script is part of the videogame developed for Akapellah by Nea Lab.
 * This development started in october 2020.
 * Nea Lab team owns all the rgihts of the development, the specific scripts attached to it and the art created.
 * Any replication, resell or attempt or copying is totally illegal.
***/

using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float lastShootTime, shootInterval, lifeValue, shootingProbability;
    public GameObject explosionPrefab, eBulletPrefab;
    public GameUIController gameUIController;
    private Transform transform;
    private bool startedShooting;
    private float startTime;

    void Start()
    {
        transform = GetComponent<Transform>();
        shootingProbability = 0.45f;
        lastShootTime = Time.time;
        startedShooting = false;
        startTime = Time.time;
        shootInterval = 2f;
        lifeValue = 1;
    }

    void Update()
    {
        ShootCountDown();
        EventuallyShoot();
    }

    void ShootCountDown()
    {
        if(!startedShooting)
        {
            if(Time.time - startTime > 15f)
            {
                startedShooting = true;
            }
        }
    }

    void EventuallyShoot()
    {
        if(startedShooting)
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
            LoseLife(0.15f);
        }
    }

    void LoseLife(float lost)
    {
        lifeValue = lifeValue - lost;

        if(lifeValue<=0)
        {
            Die();
        }
    }

    void Die()
    {
        //gameUiController.OneKill();
        Destroy(this.gameObject);
        Instantiate(explosionPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }
}