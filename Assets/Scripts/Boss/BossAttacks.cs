using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour {

    Animator anim;
    Rigidbody2D rb;
    GameObject player;
    BossController bc;

    [SerializeField] float slideSpeed;
    [SerializeField] float slideTime;
    [SerializeField] float waitForSlide;

    [SerializeField] float waitForJump;
    [SerializeField] float jumpHeight;
    [SerializeField] float jumpRange;
    [SerializeField] float jumpTime;

    [SerializeField] GameObject rock;
    [SerializeField] GameObject rocksSpawnArea;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        bc = GetComponent<BossController>();
    }

    //----------------RangedAttack----------------\\
    public void RangedAttack()
    {
        //For if I want to add more ranged attacks.
        StartCoroutine(GroundStomp());
    }

    IEnumerator GroundStomp()
    {
        bc.hasActiveState = true;
        anim.Play("idle");
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(waitForJump);
        anim.Play("Jump");
        if (transform.localScale.x <= 0.1)
            rb.AddForce(transform.up * jumpHeight);
        else
            rb.AddForce(transform.up * jumpHeight);

        yield return new WaitForSeconds(jumpTime);
        StartCoroutine(SpawnRocks());
        bc.eCurState = BossController.BossActionType.Tired;
        bc.hasActiveState = false;
    }

    IEnumerator SpawnRocks()
    {
        int spawnAmount = Random.Range(2, 5);
        float spawnY = rocksSpawnArea.transform.position.y;
        float spawnX = rocksSpawnArea.transform.position.x;

        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(rock, new Vector3(spawnX + Random.Range(-2f, 2f), spawnY, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }

    //----------------MeleeAttack----------------\\
    public void MeleeAttack()
    {
        //For if I want to add more different kinds of melee attacks.
        StartCoroutine(Slide());
    }

    IEnumerator Slide()
    {
        bc.hasActiveState = true;
        anim.Play("idle");
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(waitForSlide);
        anim.SetBool("Slide", true);

        if (transform.localScale.x <= 0.1)
            rb.velocity = new Vector3(-slideSpeed, 0 ,0);
        else
            rb.velocity = new Vector3(slideSpeed, 0, 0);

        yield return new WaitForSeconds(slideTime);
        anim.SetBool("Slide", false);
        rb.velocity = Vector3.zero;
        bc.eCurState = BossController.BossActionType.Tired;
        bc.hasActiveState = false;
    }
}
