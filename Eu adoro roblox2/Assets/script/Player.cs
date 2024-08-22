using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Player : MonoBehaviour
{

    public UnityEvent OnPlayerKillEnemy;
    public UnityEvent OnPause;
    public UnityEvent OnUnPause;
    public int score;
    public float Life = 3f;
    public float lifeMax;
    public GameObject bullet;
    public Transform foot;
    bool groundCheck;
    public float speed = 5, jumpStrength = 5, bulletSpeed = 8;
    float horizontal;
    public Rigidbody2D body;

    Collider2D footCollision;
    int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        lifeMax = Life;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector2(horizontal * speed, body.velocity.y);

        //groundCheck = Physics2D.OverlapCircle(foot.position, 0.05f);
        footCollision = Physics2D.OverlapCircle(foot.position, 0.05f);
        groundCheck = footCollision;
        if (footCollision != null)
        {
            if (footCollision.CompareTag("Enemy"))
            {

                body.AddForce(new Vector2(0, jumpStrength * 150));
                Destroy(footCollision.gameObject);
            }
        }

        if (Input.GetButtonDown("Jump") && groundCheck)
        {
            body.AddForce(new Vector2(0, jumpStrength * 100));
        }
        if (horizontal != 0)//Para GetAxisRaw
        {
            direction = (int)horizontal;
        }
        /*Para quem está usando GetAxis
        if(horizontal < 0)
        {
            direction = -1;
        } else if(horizontal > 0)
        {
            direction = 1;
        }
        */
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject temp = Instantiate(bullet, transform.position, transform.rotation);
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * direction, 0);
        }
        if (Input.GetButtonDown("Cancel"))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                OnUnPause.Invoke();
            }
            else if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                OnPause.Invoke();
            }

        }

    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Boss"))
        {
            Life -= collision.gameObject.GetComponent<BossLegal>().damage;
            if (Life <= 0)
            {
                Destroy(gameObject);
            }
        }
       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

    }
    
    
}

