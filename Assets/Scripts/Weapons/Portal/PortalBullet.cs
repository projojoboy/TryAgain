using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBullet : MonoBehaviour {

    [SerializeField] float range;
    [SerializeField] float speed;
    [SerializeField] GameObject portal;

    public GameObject impactEffect;

    [SerializeField] bool portal1;
    [SerializeField] bool portal2;

    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(transform.parent.gameObject, range);
        GameObject PO = GameObject.Find("PortalOne(Clone)");
        GameObject PT = GameObject.Find("PortalTwo(Clone)");

        if (portal1) Destroy(PO);
        if (portal2) Destroy(PT);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "DeathPoint")
        {
            Instantiate(portal, coll.transform.position, Quaternion.Euler(new Vector3(0,0,0)));
            Destroy(transform.parent.gameObject);
        }

        if (coll.tag == "Wall" || coll.tag == "Enemy")
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
