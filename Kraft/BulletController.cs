using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damage;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().alterHP(damage);
            Destroy(gameObject);
        } else if(collision.gameObject.CompareTag("Ground"))
        {
            print(collision.gameObject.name);
            Destroy(gameObject);
        }
    }
}
