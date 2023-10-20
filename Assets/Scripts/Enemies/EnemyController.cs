using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float speedBoost = 1.1f;

    private void FixedUpdate()
    {
        this.transform.position -= new Vector3(0,0,Time.fixedDeltaTime*speed);
        if (transform.position.z < -10)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void SpeedAugment()
    {
        speed *= speedBoost;
    }
}
