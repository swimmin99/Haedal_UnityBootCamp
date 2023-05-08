using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;
    public GameObject attackArea;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("attack");

            Attack();
        }

        if (attacking == true)
        {
            
            timer += Time.deltaTime;

            if (timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(false);
            }
        }
    }

    void Attack()
    {
        attacking = true;
        attackArea.SetActive(true);
        print("attack1");
    }
}
