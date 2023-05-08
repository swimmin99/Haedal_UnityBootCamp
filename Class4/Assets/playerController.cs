using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float movespeed = 1f;
    private Rigidbody2D myrb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public GameObject AttackArea;


    private void Start()
    {
        animator = GetComponent<Animator>();
        myrb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        float movementValueX = Input.GetAxis("Horizontal");
        float movementValueY = Input.GetAxis("Vertical");

        if (movementValueX != 0 || movementValueY != 0)
            animator.SetBool("isRunning", true);
        else
            animator.SetBool("isRunning", false);

        if (movementValueX < 0)
        {
            spriteRenderer.flipX = true;
            AttackArea.transform.localScale = new Vector3(-1, 1, 1);

        }
        else if (movementValueX > 0)
        {
            spriteRenderer.flipX = false;
            AttackArea.transform.localScale = new Vector3(1, 1, 1);
        }

        myrb.MovePosition(myrb.position + new Vector2(movementValueX, movementValueY) * movespeed * Time.deltaTime);


    }

}



