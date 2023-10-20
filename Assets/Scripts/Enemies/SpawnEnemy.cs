using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    List<Vector3> lanes;
    GameObject toSpawn;
    [SerializeField]
    float spawnRate;
    [SerializeField]
    GameObject pool;
    [SerializeField]
    UnityEvent SpawnScore;
    int nbSpawnEnemies;
    UnityEvent waveSpawn;
    [SerializeField]
    UnityEvent noSpawn;
    [SerializeField]
    UnityEvent allSpawn;

    void Start()
    {
        StartCoroutine(Spawning());
        if (waveSpawn == null)
        {
            waveSpawn = new UnityEvent();
        }
        waveSpawn.AddListener(SpawnWaveEvent);
    }

    IEnumerator Spawning()
    {
        while(true)
        {
            nbSpawnEnemies = 0;
            yield return new WaitForSeconds(spawnRate);
            for (int i = 0; i < lanes.Count; i++)
            {
                if (getRandom())
                {
                    nbSpawnEnemies++;
                    toSpawn = pool.GetComponent<EnemyPool>().GetAvailableObject();
                    toSpawn.transform.position = lanes[i];
                    toSpawn.SetActive(true);
                }
            }
            SpawnScore.Invoke();
            waveSpawn.Invoke();
        }      
    }

    bool getRandom()
    {
        int rand = Random.Range(0, 2);
        if (rand == 1)
        {
            return true;
        }
        return false;
    }

    void SpawnWaveEvent()
    {
        if (nbSpawnEnemies == 0)
        {
            noSpawn.Invoke();
        }
        else if(nbSpawnEnemies == lanes.Count)
        {
            allSpawn.Invoke();
        }
    }
}
