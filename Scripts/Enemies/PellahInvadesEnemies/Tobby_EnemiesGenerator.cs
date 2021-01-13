/***
 * This script is part of the videogame developed for Akapellah by Nea Lab.
 * This development started in october 2020.
 * Nea Lab team owns all the rgihts of the development, the specific scripts attached to it and the art created.
 * Any replication, resell or attempt or copying is totally illegal.
***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tobby_EnemiesGenerator : MonoBehaviour
{
    ObjectPooler objectPooler;

    #region Enemy Parameters
    /*
    #region Enemy 1 Parameters
    float[] xPositionsEnemy_Enemy1Line; // Las posiciones de los enemigos por cada linea de enemigos
    int enemySpawnedCounter_Enemy1Line = 0; // Cuantos enemigos han aparecido por cada linea
    int maxEnemies_Enemy1Line; // La cantidad maxima de enemigos por linea
    public bool newEnemy1LineAvailable = true; // Si esta disponible una nueva linea de enemigos


    int enemySpawnedCounter_Enemy1 = 0; // Cuantos enemigos 1 han aparecido
    #endregion

    #region Enemy 2 Parameters
    float[] xPositionsEnemy_Enemy2Line; // Las posiciones de los enemigos por cada linea de enemigos
    int enemySpawnedCounter_Enemy2Line = 0; // Cuantos enemigos han aparecido por cada linea
    int maxEnemies_Enemy2Line; // La cantidad maxima de enemigos por linea
    public bool newEnemy2LineAvailable = true; // Si esta disponible una nueva linea de enemigos

    int enemySpawnedCounter_Enemy2 = 0; // Cuantos enemigos 2 han aparecido
    #endregion
    */
    #endregion

    bool nextWaveAvailable = true;

    public int enemiesCounter_Enemy1 = 0;
    public int enemiesCounter_Enemy2 = 0;

    public int enemyWaveCounter = 0;

    public ScenesController scenesController;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance; // Creamos una instancia del pool de objetos

        #region Spawning Parameters
        /*
        #region Spawning Parameters Enemy 1

        xPositionsEnemy_Enemy1Line = new float[4]; // Posicion de los enemigos en la lindea
        maxEnemies_Enemy1Line = xPositionsEnemy_Enemy1Line.Length; // Cantidad maxima de enemigos por linea

        float xMinEnemy1 = -0.5f; // La posicion minima de los enemigos por linea
                                  // Este valor debe ser siempre por lo menos mayor en 1 unidad al valor mínimo
                                  // de desplazmaiento en el enemies_controller

        float xMaxEnemy1 = 1.5f; // La posicion maxima de los enemigos por linea
        float spawnDistanceEnemy1 = (Mathf.Abs(xMinEnemy1) + Mathf.Abs(xMaxEnemy1)) / xPositionsEnemy_Enemy1Line.Length; // La distancia que tendrá cada enemigo en la linea
        //Debug.Log("Spawn Distance: " + spawnDistance);

        // Almacenamos la distancia y le damos la posicion
        for (int i = 0; i < xPositionsEnemy_Enemy1Line.Length; i++)
        {
            xPositionsEnemy_Enemy1Line[i] = xMinEnemy1 + (spawnDistanceEnemy1 * i); 
        }

        #endregion

        #region Spawning Parameters Enemy 2

        xPositionsEnemy_Enemy2Line = new float[1]; // Posicion de los enemigos en la lindea
        maxEnemies_Enemy2Line = xPositionsEnemy_Enemy2Line.Length; // Cantidad maxima de enemigos por linea

        float xMinEnemy2 = -1.5f; // La posicion minima de los enemigos por linea
        float xMaxEnemy2 = 1.5f; // La posicion maxima de los enemigos por linea
        float spawnDistanceEnemy2 = (Mathf.Abs(xMinEnemy2) + Mathf.Abs(xMaxEnemy2)) / xPositionsEnemy_Enemy2Line.Length; // La distancia que tendrá cada enemigo en la linea
        //Debug.Log("Spawn Distance: " + spawnDistance);

        // Almacenamos la distancia y le damos la posicion
        for (int i = 0; i < xPositionsEnemy_Enemy2Line.Length; i++)
        {
            xPositionsEnemy_Enemy2Line[i] = xMinEnemy2 + (spawnDistanceEnemy2 * i);
        }

        #endregion
        */
        #endregion

    }

    #region Count Active Enemies
    public void ActiveEnemiesCounter()
    {
        #region Count Enemies 1
        GameObject[] goEnemy1Array = GameObject.FindGameObjectsWithTag("Enemy1");
        if (goEnemy1Array.Length > 0)
        {
            enemiesCounter_Enemy1 = goEnemy1Array.Length;
        }
        else if (goEnemy1Array.Length <= 0)
        {
            enemiesCounter_Enemy1 = 0;
            //newEnemy1LineAvailable = true;
            //Debug.Log("enemy amount: " + goArray.Length + ". Creating a new line of enemies");
        }
        #endregion

        #region Count Enemies 2
        GameObject[] goEnemy2Array = GameObject.FindGameObjectsWithTag("Enemy2");
        if (goEnemy2Array.Length > 0)
        {
            GameObject goEnemy2 = goEnemy2Array[0];

            for (int i = 0; i < goEnemy2Array.Length; i++)
            {
                goEnemy2 = goEnemy2Array[i];
                //Debug.Log("enemy: ");
            }

            enemiesCounter_Enemy2 = goEnemy2Array.Length;

        }
        else if (goEnemy2Array.Length <= 0)
        {
            enemiesCounter_Enemy2 = 0;
            //newEnemy2LineAvailable = true;
            //Debug.Log("enemy amount: " + goArray.Length + ". Creating a new line of enemies");
        }
        #endregion
        
    }
    #endregion

    #region Enemy Waves Definition

    // Wave 1 is a simple straight line of 4 enemies
    public void EnemyWave1()
    {
        // Straight line of 4 enemies 1
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(-0.4f, (transform.position.y + 5f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0f, (transform.position.y + 5f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0.4f, (transform.position.y + 5f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0.8f, (transform.position.y + 5f), transform.position.z)), Quaternion.identity);
        
        nextWaveAvailable = false;

        //enemyWaveCounter = 1;
    }

    // Wave 2 is a set of two little triangles
    public void EnemyWave2()
    {
        // First triangle: left
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(-0.5f, (transform.position.y + 6f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(-0.2f, (transform.position.y + 6f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(-0.35f, (transform.position.y + 5f), transform.position.z)), Quaternion.identity);

        // Second triangle: right
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0.5f, (transform.position.y + 6f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0.8f, (transform.position.y + 6f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0.65f, (transform.position.y + 5f), transform.position.z)), Quaternion.identity);
        
        nextWaveAvailable = false;

        //enemyWaveCounter = 2;
    }

    // Wave 3 is two blocks of 4 enemies
    public void EnemyWave3()
    {
        // Block one of enemies 1: left
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(-0.5f, (transform.position.y + 5f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(-0.2f, (transform.position.y + 5f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(-0.5f, (transform.position.y + 6f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(-0.2f, (transform.position.y + 6f), transform.position.z)), Quaternion.identity);

        // Block two of enemies 1: right
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0.5f, (transform.position.y + 5f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0.8f, (transform.position.y + 5f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0.5f, (transform.position.y + 6f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0.8f, (transform.position.y + 6f), transform.position.z)), Quaternion.identity);

        //enemyWaveCounter = 3;
    }

    // Wave 4 is a big pyramid of enemies
    public void EnemyWave4()
    {
        // First line: 4 enemies 1
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(-0.4f, (transform.position.y + 8f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0f, (transform.position.y + 8f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0.4f, (transform.position.y + 8f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0.8f, (transform.position.y + 8f), transform.position.z)), Quaternion.identity);

        // Second line: 3 enemies 1
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(-0.2f, (transform.position.y + 7f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0.2f, (transform.position.y + 7f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0.6f, (transform.position.y + 7f), transform.position.z)), Quaternion.identity);

        // Third line: 2 enemies 1
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0f, (transform.position.y + 6f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0.4f, (transform.position.y + 6f), transform.position.z)), Quaternion.identity);

        // Fourth line: 1 enemy 1
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0.2f, (transform.position.y + 5f), transform.position.z)), Quaternion.identity);

        nextWaveAvailable = false;

        //enemyWaveCounter = 4;
    }

    // Wave 5 is one enemy 2
    public void EnemyWave5()
    {
        // A single Enemy 2
        ObjectPooler.Instance.SpawnFromPool("Enemy2", (new Vector3(0f, (transform.position.y + 5f), transform.position.z)), Quaternion.identity);

        nextWaveAvailable = false;

        //enemyWaveCounter = 5;
    }

    // Wave 6 is one enemy 2 and a wave 2 centered triangle
    public void EnemyWave6()
    {
        // A centered triangle of enemies 1
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(-0.1f, (transform.position.y + 9f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0.4f, (transform.position.y + 9f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0.2f, (transform.position.y + 8f), transform.position.z)), Quaternion.identity);

        // A single Enemy 2
        ObjectPooler.Instance.SpawnFromPool("Enemy2", (new Vector3(0f, (transform.position.y + 5f), transform.position.z)), Quaternion.identity);

        nextWaveAvailable = false;

        //enemyWaveCounter = 6;
    }

    // Wave 7 is two enemies 2 and a wave 3 centered block
    public void EnemyWave7()
    {
        // Two enemies 2. One on each side
        ObjectPooler.Instance.SpawnFromPool("Enemy2", (new Vector3(-0.6f, (transform.position.y + 5f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy2", (new Vector3(1f, (transform.position.y + 5f), transform.position.z)), Quaternion.identity);

        // Block of 4 enemies 1 centered
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0f, (transform.position.y + 8f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0.3f, (transform.position.y + 8f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0f, (transform.position.y + 9f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy1", (new Vector3(0.3f, (transform.position.y + 9f), transform.position.z)), Quaternion.identity);

        nextWaveAvailable = false;

        //enemyWaveCounter = 7;
    }

    // Wave 8 is three enemies 2 
    public void EnemyWave8()
    {
        // Three enemies 2
        ObjectPooler.Instance.SpawnFromPool("Enemy2", (new Vector3(-0.6f, (transform.position.y + 5f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy2", (new Vector3(0.2f, (transform.position.y + 5f), transform.position.z)), Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool("Enemy2", (new Vector3(1f, (transform.position.y + 5f), transform.position.z)), Quaternion.identity);

        nextWaveAvailable = false;

        //enemyWaveCounter = 8;
    }

    #endregion

    #region Enemy Waves Spawning

    public void SpawnEnemyWaves()
    {
        if (enemiesCounter_Enemy1 == 0 &&
            enemiesCounter_Enemy2 == 0)
        {
            nextWaveAvailable = true;
            
            /*
            Debug.Log("New Wave Available: " + nextWaveAvailable);
            Debug.Log("Enemy Wave Counter: " + enemyWaveCounter);
            Debug.Log("Enemy 1 Counter: " + enemySpawnedCounter_Enemy1);
            Debug.Log("Enemy 2 Counter: " + enemySpawnedCounter_Enemy2);
            */
            

            if (nextWaveAvailable == true)
            {
                enemyWaveCounter++;

                switch(enemyWaveCounter)
                {
                    case 0:
                        break;
                    case 1:
                        EnemyWave1();
                        nextWaveAvailable = false;
                        break;
                    case 2:
                        EnemyWave2();
                        nextWaveAvailable = false;
                        break;
                    case 3:
                        EnemyWave3();
                        nextWaveAvailable = false;
                        break;
                    case 4:
                        EnemyWave4();
                        nextWaveAvailable = false;
                        break;
                    case 5:
                        EnemyWave5();
                        nextWaveAvailable = false;
                        break;
                    case 6:
                        EnemyWave6();
                        nextWaveAvailable = false;
                        break;
                    case 7:
                        EnemyWave7();
                        nextWaveAvailable = false;
                        break;
                    case 8:
                        EnemyWave8();
                        nextWaveAvailable = false;
                        break;
                }

                nextWaveAvailable = false;
            }
        }
    }

    #endregion

    private void FixedUpdate()
    {
        ActiveEnemiesCounter();
        SpawnEnemyWaves();
        if (enemyWaveCounter >= 9)
        {
            scenesController.LoadEndScene();
        }

        /*
        if (newEnemy1LineAvailable == true)
        {
            NewEnemy1Line();
        }
        if (newEnemy2LineAvailable == true)
        {
            //NewEnemy2Line();
        }
        */
    }
}
