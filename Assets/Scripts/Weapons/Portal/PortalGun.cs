using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PortalGun : MonoBehaviour {

    [SerializeField] GameObject shootPoint;
    [SerializeField] GameObject portalBulletIn;
    [SerializeField] GameObject portalBulletOut;
    [SerializeField] GameObject player;
    [SerializeField] GameObject destroyPoint;
    [SerializeField] AudioClip shootSound;
    [SerializeField] float fireRate;
    [SerializeField] float speed;

    LevelControl lc;
    Rigidbody2D rb;
    AudioSource audioP;
    PauseMenu pm;

    private GameObject portalBullet;

    private bool canShoot;

	// Use this for initialization
	void Start () {
        canShoot    = true;
        rb          = GetComponent<Rigidbody2D>();
        audioP      = GetComponent<AudioSource>();
        lc          = GameObject.Find("LevelControl").GetComponent<LevelControl>();
        pm          = GameObject.Find("Canvas").GetComponent<PauseMenu>();
	}
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().buildIndex >= 5)
        {
            if (canShoot && lc.levelEnd == false && pm.isPaused == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Destroy(GameObject.Find("PortalBulletOne(Clone)"));
                    portalBullet = portalBulletIn;
                    Shoot();
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    Destroy(GameObject.Find("PortalBulletTwo(Clone)"));
                    portalBullet = portalBulletOut;
                    Shoot();
                }
            }
        }
    }

    void Shoot()
    {
        audioP.clip = shootSound;
        audioP.Play();
        Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 myPos = new Vector2(shootPoint.transform.position.x, shootPoint.transform.position.y);
        Vector2 direction = target - myPos;
        direction.Normalize();
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        GameObject destroy = (GameObject)Instantiate(destroyPoint, target, rotation);
        GameObject projectile = (GameObject)Instantiate(portalBullet, myPos, rotation);
        projectile.transform.parent = destroy.transform;
        projectile.GetComponent<Rigidbody2D>().velocity = direction * speed;
        canShoot = false;
        StartCoroutine(WaitTillShoot());
    }

    IEnumerator WaitTillShoot()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
