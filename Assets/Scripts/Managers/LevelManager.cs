using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject currentCheckpoint;

    private PlayerController player;
    private CameraController cam;

    public GameObject deathParticle;
    public GameObject respawnParticle;

    Rigidbody2D rb;

    public int pointRemove;

    public float respawnDelay;

    private float gravityStore;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
        cam = FindObjectOfType<CameraController>();
        rb = GetComponent<Rigidbody2D>();

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
    }

    public void RespawnPlayer()
    {
        StartCoroutine("RespawnPlayerCo");
    }

    public IEnumerator RespawnPlayerCo()
    {
        player.rb.gravityScale = 0;
        Instantiate(deathParticle, player.transform.position, player.transform.rotation);
        player.enabled = false;
        player.GetComponent<Renderer>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        yield return new WaitForSeconds(respawnDelay);

        player.rb.gravityScale = 5;
        player.transform.position = currentCheckpoint.transform.position;
        player.enabled = true;
        player.GetComponent<Renderer>().enabled = true;
        Instantiate(respawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
    }
}
