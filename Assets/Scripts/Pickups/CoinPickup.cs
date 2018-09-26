using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {

    public int pointsToAdd;
    [SerializeField] GameObject particle;
    GameObject chest;
    GameObject levelControl;
    ScoreManager sm;

    private void Start()
    {
        chest           = GameObject.Find("Chest");
        levelControl    = GameObject.Find("LevelControl");
        sm              = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player"){
            sm.Finish();
            chest.GetComponent<ChestController>().CoinPickUpSound();
            levelControl.GetComponent<LevelControl>().WinScreen();
            Instantiate(particle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
