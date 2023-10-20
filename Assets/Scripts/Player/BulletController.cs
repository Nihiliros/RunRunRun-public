using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    float speed=10;


    private void FixedUpdate()
    {
        this.transform.position += new Vector3(0, 0, Time.fixedDeltaTime * speed);
        if (transform.position.z > 50)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<EnemyHealth>().GetDamage();
        Destroy(this.gameObject);
    }
}

