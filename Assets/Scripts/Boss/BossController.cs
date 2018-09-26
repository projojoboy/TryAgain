using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    public enum BossActionType
    {
        Idle,
        Hurt,
        Tired,
        Moving,
        RangedAttack,
        MeleeAttack,
        Dead
    }

    public BossActionType eCurState = BossActionType.Idle;

    Rigidbody2D rb;
    Animator anim;
    BossAttacks attack;
    BossMovement move;
    BossHP hp;
    GameObject player;

    [HideInInspector] public bool hasActiveState = false;

    private bool isDead;

    [SerializeField] float tiredTime;
    [SerializeField] GameObject chest;

    // Use this for initialization
    void Start () {
        rb      = GetComponent<Rigidbody2D>();
        attack  = GetComponent<BossAttacks>();
        move    = GetComponent<BossMovement>();
        anim    = GetComponent<Animator>();
        hp      = GetComponent<BossHP>();
        player  = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasActiveState)
        {
            StateUpdate();
        }
    }

    void StateUpdate()
    {
        switch (eCurState)
        {
            case BossActionType.Idle:
                StartCoroutine(Idle());
                break;

            case BossActionType.Hurt:
                StartCoroutine(Hurt());
                break;

            case BossActionType.Moving:
                BeforeMovement();
                break;

            case BossActionType.RangedAttack:
                attack.RangedAttack();
                break;
            case BossActionType.MeleeAttack:
                attack.MeleeAttack();
                break;
            case BossActionType.Dead:
                Dead();
                break;
            case BossActionType.Tired:
                StartCoroutine(Tired());
                break;
        }
    }

    public void DoDamage()
    {
        StopAllCoroutines();
        hp.healthPoints--;
        attack.StopAllCoroutines();
        move.StopAllCoroutines();
        eCurState = BossController.BossActionType.Hurt;
        hasActiveState = false;
    }

    IEnumerator Hurt()
    {
        if (hp.healthPoints != 0)
        {
            hasActiveState = true;
            rb.velocity = Vector3.zero;
            anim.Play("Hurt");
            yield return new WaitForSeconds(0.48f);
            eCurState = BossActionType.Idle;
            hasActiveState = false;
        }
    }

    public void Dead()
    {
        hasActiveState = true;
        anim.Play("dead");
        isDead = true;
        Destroy(gameObject.GetComponent<CapsuleCollider2D>());
        rb.gravityScale = 0;
        rb.velocity = Vector3.zero;
        chest.SetActive(true);
    }

    public IEnumerator Tired()
    {
        hasActiveState = true;
        anim.Play("tired");
        yield return new WaitForSeconds(tiredTime);
        eCurState = BossActionType.Idle;
        hasActiveState = false;
    }

    public IEnumerator Idle()
    {
        hasActiveState = true;
        anim.Play("idle");
        yield return new WaitForSeconds(1);

        int state = Random.Range(1, 3);
        if (state == 1)
            eCurState = BossActionType.Moving;
        else
            eCurState = BossActionType.RangedAttack;

        hasActiveState = false;
    }

    void BeforeMovement()
    {
        if (player.transform.position.x < transform.position.x - 4 || player.transform.position.x > transform.position.x + 4)
            move.Move();
        else
            eCurState = BossActionType.MeleeAttack;

        if (player.transform.position.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        /*if(coll.tag == "Player" && isDead == true)
            Physics.IgnoreCollision(player.GetComponent<2D>(), collider);*/

        if (coll.tag == "Bullet")
        {
            Destroy(coll.transform.parent.gameObject);
        }
    }
}
