using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    Rigidbody body;
    public float speed;
    bool canBounce;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            body.velocity = transform.forward * speed;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "tile")
        {
            canBounce = true;
            //body.velocity = transform.forward * speed;
        }
    }
}
