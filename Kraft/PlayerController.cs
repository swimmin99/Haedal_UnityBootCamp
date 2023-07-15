using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //moving
    public float speed;
    //dashing
    public float dashSpeed;
    int maxDashCount = 3;
    public int currentDashCount = 3;
    public bool isDashing;
    public float dashRechargeTime = 5f;
    //jumping
    public float jumpSpeed;
    public float groundedValue = 0.15f;
    //properties
    private bool isAlive = true;
    private int hp = 50;
    private int maxHp;
    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public bool isComplete;
    //waterproperties
    private float waterTimer = 0f;
    //battle
    public float bounceForce = 2.5f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public SpriteRenderer myBubbleImg;
    //UI
    public TextMeshProUGUI hpText;
    public GameObject deathUI;
    public GameObject NextUI;
    public Canvas mainCanvas;
    public string NextScene;
    public Image progressBar;
    public DashUI dashUI;
    public TextMeshProUGUI stageName;
    public GameObject MenuUIObj;
    //referance
    public GameObject waterObj;
    public GameObject myCamera;


    // Update is called once per frame

    private void Start()
    {
        isComplete = false;
        maxHp = hp;
        isAlive = true;
        currentDashCount = maxDashCount;
        isDashing = false;
    }

    private void Update()
    {
        stageName.text = SceneManager.GetActiveScene().name;
        if (Input.GetKeyDown(KeyCode.Escape) && isComplete == false)
        {
            callMenuUI();
        }

       if (isAlive)
        {
            //UI
            hpText.text = hp.ToString();
            progressBar.fillAmount = hp / (float)maxHp;
            dashUI.dashAmount = currentDashCount;
            waterTimer += Time.deltaTime;

            if (transform.position.y>waterObj.transform.position.y+waterObj.transform.localScale.y/2 && waterTimer >= 1f)
            {
                waterTimer = 0f;
                hp -= 1;
            }

            if(hp < 0)
            {
                characterDie();
            }



            //Moving
            float HorizontalInput = Input.GetAxis("Horizontal");
            float displayedSpeed = speed * HorizontalInput - rb.velocity.x;
            rb.AddForce(displayedSpeed * Vector2.right);

            Debug.DrawRay(transform.position, Vector2.down * groundedValue, Color.red);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jumper();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (currentDashCount > 0)
                {
                    Dash(HorizontalInput);
                }
            }

            Vector3 playerScale = transform.localScale;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePosition.x > transform.position.x)
            {
                playerScale.x = 1;
            }
            else if (mousePosition.x < transform.position.x)
            {
                playerScale.x = -1;
            }

            transform.localScale = playerScale;

            // Clamp within Screen
            Vector3 position = transform.position;
            float screenAspect = (float)Screen.width / (float)Screen.height;
            float cameraHeight = Camera.main.orthographicSize * 2;
            Bounds cameraBounds = new Bounds(
                Camera.main.transform.position,
                new Vector3(cameraHeight * screenAspect, cameraHeight, 0));

            position.x = Mathf.Clamp(position.x, cameraBounds.min.x, cameraBounds.max.x);
            position.y = Mathf.Clamp(position.y, cameraBounds.min.y, cameraBounds.max.y);

            transform.position = position;

            //Fire
            if (Input.GetMouseButtonDown(0))
            {
                FireBullet();
            }
        }
    }

    void Jumper()
    {
        if (CheckGrounded())
        {
            print("jumping");
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Force);
        } else
        {
            print("in the air");
        }

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = 2.0f;
        }
        else
        {
            rb.gravityScale = 1.0f;
        }
    }
    
    void Dash(float direction)
    {
        isDashing = true;
        print("Dash");
        rb.AddForce(new Vector2(dashSpeed * direction, 0), ForceMode2D.Impulse);
        currentDashCount--;
        isDashing = false;

        StartCoroutine(DashRecharge());
    }

    IEnumerator DashRecharge()
    {
        yield return new WaitForSeconds(dashRechargeTime);

        if (currentDashCount < maxDashCount)
        {
            currentDashCount++;
        }
    }

    bool CheckGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        RaycastHit2D hit = Physics2D.Raycast(position, direction,groundedValue, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Enemy Encounter
        if (collision.gameObject.CompareTag("Enemy"))
        {
            print("Ouch");
            Vector2 bounceBackDirection = (rb.position -(Vector2)collision.transform.position).normalized;
            StartCoroutine(FlashRed());
            rb.AddForce(bounceBackDirection * bounceForce, ForceMode2D.Impulse);
        } else if (collision.gameObject.CompareTag("Destination")) //SUCESS
        {
            isComplete = true;
            myCamera.GetComponent<CameraFollower>().followPlayer = false;
            rb.isKinematic = true;
            GameObject temp = Instantiate(NextUI, mainCanvas.transform);
            temp.GetComponent<FailedManeger>().PassedsceneName = NextScene;
        } else if (collision.gameObject.CompareTag("Health"))
        {
            hp += 25;
            Destroy(collision.gameObject);
        }

    }

    IEnumerator FlashRed()
    {
        Color originalColor = myBubbleImg.color;
        myBubbleImg.color = Color.red;
        yield return new WaitForSeconds(1);
        myBubbleImg.color = originalColor;
    }

    public void AlterHP(int change)
    {
        hp += change;
    }

    void FireBullet()
    {
        hp--;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
    }
    

    public void characterDie()
    {
        myCamera.GetComponent<CameraFollower>().followPlayer = false;
        print("dead");
        rb.isKinematic = true;
        hp = 0;
        isAlive = false;
        Instantiate(deathUI,mainCanvas.transform);
    }

    public void callMenuUI()
    {
        MenuUIObj.SetActive(true);
    }

}

