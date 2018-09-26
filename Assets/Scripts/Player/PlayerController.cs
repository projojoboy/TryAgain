using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float jumpForce;

    [SerializeField] AudioClip stepSound;
    [SerializeField] AudioClip jumpSound;

    AudioSource audioP;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public Transform shootPoint;

    private bool isGrounded;

    private float moveVelocity;

    public Rigidbody2D rb;
    private Animator anim;

    LevelControl lvlCont;

	// Use this for initialization
	void Start () {
        rb      = GetComponent<Rigidbody2D>();
        anim    = GetComponent<Animator>();
        audioP  = GetComponent<AudioSource>();
        lvlCont = GameObject.Find("LevelControl").GetComponent<LevelControl>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    // Update is called once per frame
    void Update () {
        anim.SetBool("Grounded", isGrounded);

        if (lvlCont.levelEnd == false)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if (isGrounded)
                {
                    if (!audioP.isPlaying)
                    {
                        audioP.clip = stepSound;
                        audioP.Play();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }

            if (!isGrounded && rb.velocity.y < 0)
            {
                anim.Play("jumpDown");
            }

            if (isGrounded && rb.velocity.x == 0 && rb.velocity.y == 0)
                anim.Play("idle");

            moveVelocity = 0f;

            if (Input.GetKey(KeyCode.D))
            {
                moveVelocity = moveSpeed;
            }

            if (Input.GetKey(KeyCode.A))
            {
                moveVelocity = -moveSpeed;
            }

            rb.velocity = new Vector2(moveVelocity, rb.velocity.y);

            anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
        if (lvlCont.GetComponent<LevelControl>().levelEnd)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.Play("idle");
        }
    }

    public void Jump()
    {
        audioP.clip = jumpSound;
        audioP.Play();
        anim.Play("jumpUp");
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
