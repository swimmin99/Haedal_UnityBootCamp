using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    float movespeed = 1f;
    Rigidbody2D myrb;
    public Animator animator;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        myrb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float movementValueX = Input.GetAxis("Horizontal");
        float movementValueY = Input.GetAxis("Vertical");
        print(movementValueX);
        print(movementValueY);


        if (movementValueX != 0 || movementValueY != 0)
        {
            animator.SetBool("isMoving", true);

                myrb.MovePosition(myrb.position + new Vector2(movementValueX, movementValueY) * movespeed * Time.fixedDeltaTime);
                if (movementValueX < 0)
                {
                    spriteRenderer.flipX = true;
                } else
                {
                    spriteRenderer.flipX = false;
                }


        } else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
