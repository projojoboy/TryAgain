using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour {

    Animator anim;
    AudioSource audioP;

    [SerializeField] GameObject coin;
    [SerializeField] AudioClip chestOpening;
    [SerializeField] AudioClip CoinAppear;
    [SerializeField] AudioClip CoinCollect;
    [SerializeField] float waitTillCoinSpawn;

    private bool isOpen;
	// Use this for initialization
	void Start () {
        anim    = GetComponent<Animator>();
        audioP  = GetComponent<AudioSource>();

        isOpen  = false;
        audioP.volume = 1;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void OpenChest()
    {
        if (!isOpen)
        {
            isOpen = true;
            audioP.clip = chestOpening;
            audioP.Play();
            anim.Play("chestOpen");
            StartCoroutine(SpawnCoin());
        }
    }

    IEnumerator SpawnCoin()
    {
        yield return new WaitForSeconds(waitTillCoinSpawn);
        Instantiate(coin, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
        audioP.clip = CoinAppear;
        audioP.Play();
    }

    public void CoinPickUpSound()
    {
        audioP.clip = CoinCollect;
        audioP.volume = 0.3f;
        audioP.Play();
    }
}
