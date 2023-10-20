using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField]
    int poolSize;
    [SerializeField]
    GameObject EnemyPrefab;

    List<GameObject> pool = new List<GameObject>();

    void Start()
    {
        for(int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(EnemyPrefab, this.transform);
            enemy.SetActive(false);
            pool.Add(enemy);
        }
    }

    //choose an inactive object to spawn 
    public GameObject GetAvailableObject()
    {
        foreach(GameObject item in pool)
        {
            if (!item.activeSelf)
            {
                return item;
            }
        }
        return null;
    }
    
    //Augments various variables for all enemies of the pool
    public void GetHealthAugment()
    {
        foreach (GameObject item in pool)
        {
            item.GetComponent<EnemyHealth>().HealthAugment();
        }
    }
    public void GetSpeedAugment()
    {
        foreach (GameObject item in pool)
        {
            item.GetComponent<EnemyController>().SpeedAugment();
        }
    }

    //Deactivate all enemies of the pool
    public void CleanEnemies()
    {
        foreach (GameObject item in pool)
        {
            item.SetActive(false);
        }
    }
}
