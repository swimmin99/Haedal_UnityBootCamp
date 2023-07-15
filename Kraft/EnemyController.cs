using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool MovableMosnter;

    public float patrolDistance = 5f;
    public float detectionRange = 3f;
    public float yTolerance = 0.5f;
    public float speed = 2f;
    private Vector2 originalPosition;
    private Vector2 targetPosition;
    private bool isMoving = false;


    public int damage = 1;
    public float patrolTime = 5f;
    private float patrolTimer = 0f;
    private Transform player;

    public int myHP = 3;

    void Start()
    {
        originalPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (MovableMosnter == true)
        {

            float yDifference = Mathf.Abs(transform.position.y - player.position.y);

            if (Vector2.Distance(transform.position, player.position) <= detectionRange && yDifference <= yTolerance)
            {
                print("onPersuit");
                targetPosition = new Vector2(player.position.x, transform.position.y);
                isMoving = true;
            }
            else if (Vector2.Distance(transform.position, originalPosition) > patrolDistance)
            {
                print("goingBack");
                targetPosition = originalPosition;
                isMoving = true;
            }

            if (isMoving)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);

                if (Vector2.Distance(transform.position, targetPosition) < 0.001f)
                {
                    isMoving = false;
                }
            }
            else
            {
                Vector3 playerScale = transform.localScale;
                if (targetPosition.x > transform.position.x)
                {
                    playerScale.x = 1;
                }

                else if (targetPosition.x < transform.position.x)
                {
                    playerScale.x = -1;
                }


                targetPosition = originalPosition + new Vector2(Random.Range(-patrolDistance, patrolDistance), 0);


                if (Vector2.Distance(originalPosition, transform.position) < 0.2f)
                {
                    patrolTimer += Time.deltaTime;
                    if (patrolTimer > patrolTime)
                    {
                        patrolTime = 0f;
                        targetPosition = originalPosition + new Vector2(Random.Range(-patrolDistance, patrolDistance), 0);

                    }
                }
                isMoving = true;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().AlterHP(-1 * damage);
        } else if (collision.gameObject.CompareTag("Water"))
        {
            Destroy(gameObject);
        } 

    }

    public void alterHP(int takenDamage)
    {
        myHP += takenDamage;
    }

    public void enemyDie()
    {
        Destroy(gameObject);
    }
}
