using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossHP : MonoBehaviour {

    [SerializeField] TextMeshProUGUI HPtext;

    public int healthPoints;

    BossController bc;
    Animator anim;
    BossAttacks attack;
    BossMovement move;
    BossHP hp;

    // Use this for initialization
    void Start () {
        bc = GetComponent<BossController>();
        anim = GetComponent<Animator>();
        attack = GetComponent<BossAttacks>();
        move = GetComponent<BossMovement>();
    }
	
	// Update is called once per frame
	void Update () {
        if (healthPoints <= 0)
        {
            StopAllCoroutines();
            attack.StopAllCoroutines();
            move.StopAllCoroutines();
            bc.eCurState = BossController.BossActionType.Dead;
            bc.hasActiveState = false;
        }
        HPtext.text = "Boss Healthpoints: " + healthPoints;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Saw" && coll.GetComponent<Rigidbody2D>().velocity.y != 0f && bc.eCurState != BossController.BossActionType.Hurt)
        {
            Destroy(coll.gameObject);
            bc.DoDamage();
        }
    }
}
