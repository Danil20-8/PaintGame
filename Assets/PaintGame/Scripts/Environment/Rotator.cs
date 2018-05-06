using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {


    public float speed = 30;

	
	// Update is called once per frame
	void Update () {
        transform.localRotation = Quaternion.AngleAxis(speed * Time.deltaTime, Vector3.up) * transform.localRotation;
	}
}
