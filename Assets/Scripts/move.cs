﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class move : MonoBehaviour
{
    Rigidbody body;
    [Header("GamePlay")]
    public int Points;
    public GameObject Points_UI;
    [Space]
    [Header("Floats")]
    public float speed;
    public float upForce = 5f;
    public float boostForce;
    [Space]
    [Header("Bools")]
    public bool canBounce;
    public bool boost;
    public bool moveForward;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        UpdateUI();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            boost = true;
        }
        if (canBounce)
        {
            body.AddForce(transform.up * upForce);
            if (boost)
            {
                body.AddForce(transform.forward * boostForce);
            }else
            {
                body.AddForce(transform.forward * speed);
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        
        if (col.gameObject.tag == "tile")
        {
            print("Collided with tile");
            Points++;
            UpdateUI();
            canBounce = true;
            col.gameObject.GetComponentInParent<Animator>().SetTrigger("pushed");
            
        }
    }
    void OnCollisionExit(Collision col)
    {
        canBounce = false;
        boost = false;
    }
    void UpdateUI()
    {
        Points_UI.GetComponent<TextMeshProUGUI>().text = Points.ToString();
    }
}
