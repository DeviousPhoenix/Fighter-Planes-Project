using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;
    private float horizontalScreenSize = 11.5f;
    private float verticalScreenSize = 7.5f;
    private float speed;
    private int lives;
    private int score;

    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        speed = 6f;
        lives = 3;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput,0) * Time.deltaTime * speed);
        if (transform.position.x > horizontalScreenSize || transform.position.x <= -horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        if (transform.position.y > verticalScreenSize || transform.position.y < -verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }

    public void LoseALife()
    {
        lives--;
        //lives -= 1;
        //lives = lives - 1;
        if (lives == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void EarnScore()
    {
        score += 1;
        //score += 1;
        //score = score + 1;
    }

    private void OnTriggerEnter2D(Collider2D objectHit)
    {
        if(objectHit.tag == "Enemy")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().LoseLives(1);
        }
        else if(objectHit.tag == "Coin")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().EarnScore(1);
        }
    }
}
