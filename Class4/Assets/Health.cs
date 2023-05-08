using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float health = 12f;
    private SpriteRenderer renderer;

    public bool isDelay;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    public void Damage(float damage)
    {
        print("attack3");
        health -= damage;

        StartCoroutine(Red());

        if(health <= 0)
        {
            Destroy(gameObject);
        }

    }

    IEnumerator Red()
    {
        renderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        renderer.color = Color.white;
        isDelay = false;
    }

    
}
