using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    Rigidbody body;
    public float speed;
    public float upForce = 5f;
    bool canBounce;
    bool moveForward;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            body.velocity = transform.forward * speed;
        }
        if (canBounce)
        {
            body.AddForce(transform.up * upForce);
            body.AddForce(transform.forward * speed);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        
        if (col.gameObject.tag == "tile")
        {
            print("Collided with tile");
            canBounce = true;
            col.gameObject.GetComponentInParent<Animator>().SetTrigger("pushed");
            
        }
    }
    void OnCollisionExit(Collision col)
    {
        canBounce = false;
        
    }
}
