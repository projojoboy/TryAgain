using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    AudioSource audioP;
    BossHP hp;
    GameObject player;

    [HideInInspector] public LevelManager levelManager;
    [HideInInspector] public ScoreManager scoreManager;

    [SerializeField] AudioClip waterSound;
    [SerializeField] AudioClip sawSound;
    [SerializeField] AudioClip bossSound;
    [SerializeField] bool isWater;
    [SerializeField] bool isSaw;
    [SerializeField] bool isFallingSaw;
    [SerializeField] bool isBoss;

    GameObject portalIn;
    GameObject portalOut;
    GameObject portalBulletIn;
    GameObject portalBulletOut;

    // Use this for initialization
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        audioP = GetComponent<AudioSource>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        if(GameObject.Find("Boss"))
            hp = GameObject.Find("Boss").GetComponent<BossHP>();
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            portalIn = GameObject.Find("PortalOne(Clone)");
            portalOut = GameObject.Find("PortalTwo(Clone)");
            portalBulletIn = GameObject.Find("PortalBulletOne(Clone)");
            portalBulletOut = GameObject.Find("PortalBulletTwo(Clone)");

            if (isWater)
            {
                audioP.clip = waterSound;
                audioP.Play();
            }
            else if (isSaw)
            {
                audioP.clip = sawSound;
                audioP.Play();
            }
            else if (isFallingSaw)
            {
                Destroy(gameObject);
                player.GetComponent<AudioSource>().clip = sawSound;
                player.GetComponent<AudioSource>().Play();
            }
            else if (isBoss)
            {
                audioP.clip = bossSound;
                audioP.Play();
                if(hp.healthPoints < 3)
                    hp.healthPoints++;
            }

            Destroy(portalIn);
            Destroy(portalOut);
            Destroy(portalBulletIn);
            Destroy(portalBulletOut);
            scoreManager.AddDeaths();
            levelManager.RespawnPlayer();
        }
    }
}
