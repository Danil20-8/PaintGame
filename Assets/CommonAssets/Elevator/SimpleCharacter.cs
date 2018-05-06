using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleCharacter : MonoBehaviour {

    public float speed;

    new Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
    }

	// Update is called once per frame
	void FixedUpdate () {

        var velocity = (transform.right * Input.GetAxis("Horizontal") + (transform.forward * Input.GetAxis("Vertical"))).normalized * speed;
        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;

        transform.rotation = Quaternion.Euler(0, Input.GetAxis("Mouse X"), 0) * transform.rotation;
	}
}
