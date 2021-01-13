/***
 * This script is part of the videogame developed for Akapellah by Nea Lab.
 * This development started in october 2020.
 * Nea Lab team owns all the rgihts of the development, the specific scripts attached to it and the art created.
 * Any replication, resell or attempt or copying is totally illegal.
***/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Transform myTransform;

    private float width;
    private float height;

    private float widthMin;
    private float widthMax;
    private float clampedWidthMin;
    private float clampedWidthMax;

    private float fireTime;
    private float fireRate = 0.75f;
    public GameObject bulletPrefab;
    public TextMesh playerDebugText;

    private Touch touch1;
    private Touch touch2;
    private bool firstTouch;
    private Vector3 touch1Vector1, touch1Vector2;
    private Vector3 speed;
    public GameObject joy, stick;
    public GameUIController gameUiController;

    //public GameController gameController;
    public ScenesController scenesController;

    private Animator animator;

    private bool alive;
    private float deathTime;
    private int lifesLeft;
    public SpriteRenderer life1, life2, life3;

    private int state;
    private bool enteredDoubleTouch;

    /* STATE MACHINE
     * 0: Not pressing
     * 1: Joystick still pressed
     * 2: Shooting still pressed
     */

    void Awake()
    {
        alive = true;
        lifesLeft = 3;

        state = 0;
        fireTime = 0f;
        firstTouch = false;
        width = (float)Screen.width;
        height = (float)Screen.height;
        speed = new Vector3(0, 0, 0);
        touch1Vector1 = new Vector3(0, 0, 0);
        touch1Vector2 = new Vector3(0, 0, 0);
        animator = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();

        // Animacion corriendo
        animator.SetBool("runBool", true);
        animator.SetBool("shootLeftBool", false);
        animator.SetBool("shootRightBool", false);

        enteredDoubleTouch = false;
    }

    void Start()
    {
        joy.SetActive(false);
    }

    void Update()
    {
        try
        {
            if (alive == true)
            {
                ProcessInput();
                //UnityEngine.Debug.Log("can shoot = " + canShoot);
                OutOfRangeCorrection();
                PlayerAnimation();

                if (canShoot == false) 
                {
                    fireTime += Time.deltaTime;

                    if (fireTime > fireRate)
                    {
                        canShoot = true;

                    }
                } 
            }
            else
            {
                Dying();
            }
        }
        catch(Exception e)
        {
            UnityEngine.Debug.Log(e);
            playerDebugText.text = e.ToString();
        }
    }

    // Collision of player with bullets or enemies
    void OnTriggerEnter2D(Collider2D col)
    {
        if(alive)
        {
            if(col.gameObject.CompareTag("Enemy1") || col.gameObject.CompareTag("eBullet"))
            {
                LoseLife();
            }
        }
    }

    // Player loses a life
    void LoseLife()
    {
        lifesLeft -= 1;
        switch (lifesLeft)
        {
            case 2:
                life3.color = Color.black;
                break;
            case 1:
                life2.color = Color.black;
                break;
            case 0:
                life1.color = Color.black;
                deathTime = Time.time;

                // Animacion muriendo
                animator.SetBool("runBool", false);
                animator.SetBool("shootLeftBool", false);
                animator.SetBool("shootRightBool", false);
                animator.SetBool("dyingBool", true);

                alive = false;
                GetComponent<BoxCollider2D>().isTrigger = true;
                break;
        }
    }

    void Dying()
    {
        var collider = this.gameObject.GetComponent<BoxCollider2D>();
        collider.enabled = false;
        canShoot = false;

        if(Time.time - deathTime > 3f)
        {
            //gameUiController.GameOver();
            Destroy(this.gameObject);
            UnityEngine.Debug.Log("Me morí");
            scenesController.LoadEndScene();
        }
    }

    void OutOfRangeCorrection()
    {
        // Si akapellah se sale de la pantalla por algún bug, vuelve al centro

        if(myTransform.position.x < -5f || myTransform.position.x > 5f || myTransform.position.y > 8f || myTransform.position.y < -8f)
        {
            myTransform.position = new Vector3(0, -4f, 1);
        }
    }

    
    void ProcessInput()
    {
        switch (Input.touchCount)
        {
            case 0:
                enteredDoubleTouch = false;
                if (firstTouch)
                {
                    joy.SetActive(false);
                    firstTouch = false;
                    state = 0;
                }
                break;

            case 1:
                enteredDoubleTouch = false;
                touch1 = Input.GetTouch(0);
                Vector2 pos = touch1.position;
                ProcessJoystick(pos);
                if (state == 2)
                {
                    //FireBullet();
                }
                playerDebugText.text = pos.ToString() + " " + state.ToString();
                break;

            case 2:
                touch1 = Input.GetTouch(0);
                touch2 = Input.GetTouch(1);
                Vector2 pos1 = touch1.position;
                Vector2 pos2 = touch2.position;

                if (pos1.x < 2 * width / 3)
                {
                    ProcessJoystick(pos1);

                    if (pos2.x > 2 * width / 3)
                    {
                        playerDebugText.text = "First moving then shooting";
                        //FireBullet();
                    }
                }
                else
                {
                    if (!enteredDoubleTouch)
                    {
                        firstTouch = false;
                        enteredDoubleTouch = true;
                    }
                    //FireBullet();

                    if (pos2.x < 2 * width / 3)
                    {
                        playerDebugText.text = "First shooting then moving   " + pos2.ToString();
                        ProcessJoystick(pos2);
                    }
                }
                break;
        }
    }

    /*
    public void FireBullet()
    {
        fireTime += Time.deltaTime;

        if (fireTime <= fireRate)
        {
            animator.SetBool("runBool", true);
            animator.SetBool("shootLeftBool", false);
            animator.SetBool("shootRightBool", false);
        }

        if (fireTime > fireRate)
        {
            if (UnityEngine.Random.value < 0.5f)
            {
                animator.SetBool("runBool", false);
                animator.SetBool("shootLeftBool", true);
                animator.SetBool("shootRightBool", false);
            }
            else
            {
                animator.SetBool("runBool", false);
                animator.SetBool("shootLeftBool", false);
                animator.SetBool("shootRightBool", true);
            }
            Instantiate(bulletPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity); // Fire bullets
            fireTime = 0f;
        }
    }
    */

    private bool canShoot = true;
    public void FireBullet()
    {
        if (canShoot)
        {
            Instantiate(bulletPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity); // Fire bullets
            fireTime = 0f;
            canShoot = false;
            //UnityEngine.Debug.Log("Fired");
        }
        else { 
            //UnityEngine.Debug.Log("Fire Try");
        }
    }

    private bool animationFireFlag = false;
    public void PlayerAnimation()
    {
        if (canShoot == true)
        {   
            // Animacion corriendo
            animator.SetBool("runBool", true);
            animator.SetBool("shootLeftBool", false);
            animator.SetBool("shootRightBool", false);

            animationFireFlag = false;
        }
        else
        {
            if (animationFireFlag == false)
            {
                if (UnityEngine.Random.value < 0.5f)
                {
                    // Animación Left
                    animator.SetBool("runBool", false);
                    animator.SetBool("shootLeftBool", true);
                    animator.SetBool("shootRightBool", false);

                    animationFireFlag = true;
                }
                else
                {
                    // Animación Right
                    animator.SetBool("runBool", false);
                    animator.SetBool("shootLeftBool", false);
                    animator.SetBool("shootRightBool", true);

                    animationFireFlag = true;
                }
            }
            else
            {
                // Animacion corriendo
                animator.SetBool("runBool", true);
                animator.SetBool("shootLeftBool", false);
                animator.SetBool("shootRightBool", false);
            }
        }
    }

    void ProcessJoystick(Vector2 vec)
    {
        if (!firstTouch)
        {
            // First finger pressed
            firstTouch = true;
            if (vec.x < 2 * width / 3)
            {
                // Touching to move
                state = 1;
                joy.SetActive(true);
                touch1Vector1 = new Vector3(2.8f * (vec.x - (width / 2)) / (width / 2), 5f * (vec.y - (height / 2)) / (height / 2), 0);
                //touch1Vector1 = new Vector3(-1.75f, -4.35f, 0.75f);
                joy.GetComponent<Transform>().position = touch1Vector1;
            }
            else
            {
                // Touching to shoot
                state = 2;
            }
        }

        if (state == 1)
        {
            if (vec.x < 2 * width / 3)
            {
                float mox;
                float moy;

                touch1Vector2 = new Vector3(2.8f * (vec.x - (width / 2)) / (width / 2), 5f * (vec.y - (height / 2)) / (height / 2), 0);

                if ((touch1Vector2 - touch1Vector1).sqrMagnitude > 1)
                {
                    Vector3 playerLimiter = Vector3.Normalize(touch1Vector2 - touch1Vector1);

                    stick.GetComponent<Transform>().position = touch1Vector1 + playerLimiter;
                    
                    mox = 0.03f * playerLimiter.x;
                    moy = 0.03f * playerLimiter.y;
                }
                else
                {
                    stick.GetComponent<Transform>().position = touch1Vector2;
                    // Move player
                    mox = 0.03f * (touch1Vector2.x - touch1Vector1.x);
                    moy = 0.03f * (touch1Vector2.y - touch1Vector1.y);
                }

                /*
                // Move player
                float mox = 0.03f * (touch1Vector2.x - touch1Vector1.x);
                float moy = 0.03f * (touch1Vector2.y - touch1Vector1.y);
                */

                if (transform.position.x + mox > 2.6f || transform.position.x + mox < -2.6f)
                {
                    mox = 0;
                }
                if (transform.position.y + moy > -2f || transform.position.y + moy < -4.3f)
                {
                    moy = 0;
                }
                transform.position = transform.position + new Vector3(mox, moy, 0);
            }
            else
            {
                firstTouch = false;
                joy.SetActive(false);
                state = 2;
            } 
        }
    }
}