/***
 * This script is part of the videogame developed for Akapellah by Nea Lab.
 * This development started in october 2020.
 * Nea Lab team owns all the rgihts of the development, the specific scripts attached to it and the art created.
 * Any replication, resell or attempt or copying is totally illegal.
***/

using UnityEngine;

public class eBulletController : MonoBehaviour
{
    private AudioController audioControllerScript;

    private float speed = 2.5f;

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
        myTransform.position += new Vector3(0, -speed * Time.deltaTime, 0f);
        if (myTransform.position.y<-5.5f) Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            audioControllerScript.pBulletExploded = true;
            Instantiate(explosionPrefab, new Vector2(myTransform.position.x, myTransform.position.y), Quaternion.identity); // Instantiate explosion     
            Destroy(this.gameObject);       
        }
    }
}