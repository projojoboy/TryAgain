using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour {

    private GameObject portalOne;
    private GameObject portalTwo;

    [SerializeField] float teleportTimer;
    [SerializeField] AudioClip portalSound;

    Rigidbody2D rb;
    AudioSource audioP;
    private bool ableTeleport;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        ableTeleport = true;
        audioP = GetComponent<AudioSource>();
	}

    private void Update()
    {
        portalOne = GameObject.Find("PortalOne(Clone)");
        portalTwo = GameObject.Find("PortalTwo(Clone)");
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (ableTeleport && coll.tag == "Portal")
        {
            if (coll.gameObject == portalOne)
            {
                if (portalTwo)
                {
                    gameObject.transform.position = portalTwo.transform.position;
                    ableTeleport = false;
                    StartCoroutine(TeleportDelay());
                }
            }

            if (coll.gameObject == portalTwo)
            {
                if (portalOne)
                {
                    gameObject.transform.position = portalOne.transform.position;
                    ableTeleport = false;
                    StartCoroutine(TeleportDelay());
                }
            }
        }
    }

    IEnumerator TeleportDelay()
    {
        audioP.clip = portalSound;
        audioP.Play();
        yield return new WaitForSeconds(teleportTimer);
        ableTeleport = true;
    }
}
