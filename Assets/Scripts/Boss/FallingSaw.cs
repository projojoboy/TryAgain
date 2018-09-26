using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FallingSaw : MonoBehaviour {

    GameObject boss;
    private bool destroy = true;

	// Use this for initialization
	void Start () {
        boss = GameObject.Find("Boss");
	}
	
	// Update is called once per frame
	void Update () {
        Destroy(gameObject, 6f);
	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Wall")
        {
            if (destroy)
            {
                destroy = false;
                int spawn = Random.Range(1, 3);

                if (spawn == 1)
                    return;
                else
                    Destroy(gameObject);
            }
        }
    }
}
