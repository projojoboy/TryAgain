using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saw : MonoBehaviour {

    [SerializeField] float speed;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, Time.deltaTime * speed);
    }
}
