using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoving : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float moveTime = 5f;
    public Vector2 moveLocation;
    public float moveTimer = 0f;
    public Animator animator;
    public float MaxMoveLength = 10f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        moveLocation = new Vector2(transform.position.x + Random.Range(-1f*MaxMoveLength, MaxMoveLength), transform.position.y + Random.Range(-1f * MaxMoveLength, MaxMoveLength));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveTimer += Time.deltaTime;
        if (moveTimer > moveTime)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveLocation, moveSpeed * Time.deltaTime);
            animator.SetBool("isMoving", true);

            if (Vector2.Distance(transform.position, moveLocation) < MaxMoveLength/10f)
            {
                moveLocation = new Vector2(transform.position.x + Random.Range(-1f * MaxMoveLength, MaxMoveLength), transform.position.y + Random.Range(-1f * MaxMoveLength, MaxMoveLength));
                moveTimer = 0f;
                animator.SetBool("isMoving", false);

            }
        }
    }
}
