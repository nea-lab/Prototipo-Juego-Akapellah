/***
 * This script is part of the videogame developed for Akapellah by Nea Lab.
 * This development started in october 2020.
 * Nea Lab team owns all the rgihts of the development, the specific scripts attached to it and the art created.
 * Any replication, resell or attempt or copying is totally illegal.
***/

using UnityEngine;

public class BulletController : MonoBehaviour
{
    private AudioController audioControllerScript;

    private float speed = 3.5f;

    private Transform myTransform;
    private Vector3 origPosition;

    public GameObject explosionPrefab;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        origPosition = myTransform.position;
        audioControllerScript = GameObject.Find("AudioController").GetComponent<AudioController>();
    }

    void Update()
    {
        myTransform.position += new Vector3(0, speed * Time.deltaTime, 0f);
        if (myTransform.position.y > 4.5f) Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy1"))
        {
            audioControllerScript.bulletExploded = true;
            Destroy(this.gameObject);
            //Instantiate(explosionPrefab, new Vector2(myTransform.position.x, myTransform.position.y), Quaternion.identity); // Instantiate explosion
            
        }
        if (col.gameObject.CompareTag("Enemy2"))
        {
            audioControllerScript.bulletExploded = true;
            Destroy(this.gameObject);
            //Instantiate(explosionPrefab, new Vector2(myTransform.position.x, myTransform.position.y), Quaternion.identity); // Instantiate explosion

        }
    }
}
