/***
 * This script is part of the videogame developed for Akapellah by Nea Lab.
 * This development started in october 2020.
 * Nea Lab team owns all the rgihts of the development, the specific scripts attached to it and the art created.
 * Any replication, resell or attempt or copying is totally illegal.
***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tobby_EnemiesController : MonoBehaviour
{
    private float lastShootTime, shootInterval, lifeValue, shootingProbability;
    public GameObject explosionPrefab, eBulletPrefab;
    //public GameUIController gameUiController;
    public GameController gameController;
    private Transform enemyTransform;
    private Rigidbody2D enemyRigidBody2D;

    private bool startedShooting;
    private float startTime;
    private float randomStartTime;

    private float origPosX;
    private float origPosY;
    private Vector3 enemyOrigPosition;

    void Start()
    {
        enemyTransform = GetComponent<Transform>();
        enemyOrigPosition = enemyTransform.position;
        origPosX = enemyTransform.position.x;
        origPosY = enemyTransform.position.y;
        //yValue = origPosY;

        enemyRigidBody2D = GetComponent<Rigidbody2D>();

        shootingProbability = 0.25f;
        lastShootTime = Time.time;
        startedShooting = false;
        startTime = Time.time;
        shootInterval = 3f; // Estos son valores de prueba
        lifeValue = 1;

        randomStartTime = Random.Range(0f, 5f); // Estos son valores de prueba
        //randomStartTime = 0.5f;

        Debug.Log("One Enemy 1 Created");
    }

    void Update()
    {
        ShootCountDown();

        EventuallyShoot();

        EnemyMovementHorizontal();

        EnemyMovementVertical(1f, 3.8f);
    }

    #region Counting Down to Shooting
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
    #endregion

    #region Shooting Eventually
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
    #endregion

    #region Shooting
    void Shoot()
    {
        Instantiate(eBulletPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }
    #endregion



    /*void LoseLife(float lost)
    {
        lifeValue = lifeValue - lost;

        if (lifeValue <= 0)
        {
            Die();
        }
    }*/

    #region Dying
    void Die()
    {
        //gameUiController.OneKill();
        gameController.OneKill(100);
        Destroy(this.gameObject);
        Instantiate(explosionPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            //LoseLife(0.15f);
            Die();
        }
    }
    #endregion

    #region Movimiento Horizontal
    private float xMin = -1.5f, xMax = 2f;
    public float timeHorizontalValue = 0f;
    private float movementHorizontalSpeed = 0.5f;
    public void EnemyMovementHorizontal()
    {        
        // Compute the sin position relaitve to objects origin position
        float xValue = origPosX + Mathf.Sin(timeHorizontalValue * movementHorizontalSpeed);

        // Compute the Clamp Value
        float xPos = Mathf.Clamp(xValue, xMin, xMax);     

        // Update the position of the enemy
        enemyTransform.position = new Vector3(xPos, enemyTransform.position.y, enemyTransform.position.z);

        // Increas Animation Time
        timeHorizontalValue = timeHorizontalValue + Time.deltaTime;

        // Reset animation time if its greater than planned
        
        if (xValue > Mathf.PI * 2f)
        {
            timeHorizontalValue = 0f;
        }
    }
    #endregion

    #region Movimiento Vertical 
    private float speed = 0.5f; // Initial speed
    private float movementVerticalSpeed = 0.1f; // Normal movement speed at which the enemy is going to move
    public void EnemyMovementVertical(float maxVerticalPos, float changeSpeedPosition)
    {
        float yPos = enemyTransform.position.y; // Calculates Y position

        if(yPos >= maxVerticalPos) // If Y position is less than the threshold
        {
            if (yPos >= changeSpeedPosition) // If it has reached the position where speed should change
            { 
                enemyRigidBody2D.velocity = new Vector2(0.0f, -speed);
            }
            else
            {
                speed = movementVerticalSpeed; // New Speed
                enemyRigidBody2D.velocity = new Vector2(0.0f, -speed);
            }
            
        }
        else
        {
            speed = 0f; // No more speed. The enemy stays in its place
            enemyRigidBody2D.velocity = new Vector2(0.0f, -speed);
        }
        
    }
    #endregion
}
