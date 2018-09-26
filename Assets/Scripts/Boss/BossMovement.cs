using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour {

    GameObject player;
    Rigidbody2D rb;
    Animator anim;

    [SerializeField] float speed;

    float minusSpeed;

    public void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        minusSpeed = -speed;
    }

    public void Move()
    {
        anim.Play("movement");
        if (player.transform.position.x < transform.position.x)
        {
            rb.velocity = new Vector2(minusSpeed * Time.deltaTime, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
        }
    }
}
