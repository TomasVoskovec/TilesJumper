using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{
    Rigidbody body;
    [Header("GamePlay")]
    public int Points;
    public GameObject Points_UI;
    [Space]
    [Header("Floats")]
    public float speed;
    public float upForce;
    public float boostForce;
    [Space]
    [Header("Bools")]
    public bool canBounce;
    public bool boost;
    public bool moveForward;
    [Space]
    [Header("Particles")]
    public GameObject SmokeParticle;
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
            //use boost
            boost = true;
        }
        if (canBounce)
        {
           
            // throw ball into the air
            body.AddForce(transform.up * upForce);
            if (boost)
            {
                // skip one tile
                body.AddForce(transform.forward * boostForce);
            }else
            {
                // go to next tile
                
                //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + -5), speed * Time.deltaTime);
                body.AddForce(transform.forward * speed);
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        
        if (col.gameObject.tag == "tile")
        {
            // if player collider with a tile
            OnTileCollide(col);
        }
    }
    void OnTileCollide(Collision col)
    {
        print("Collided with tile");
        // add points
        Points++;
        UpdateUI();
        canBounce = true;
        col.gameObject.GetComponent<Animator>().SetTrigger("pushed");
        // create smoke under player
        Instantiate(SmokeParticle, new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z), SmokeParticle.transform.rotation);
    }
    void OnCollisionExit(Collision col)
    {
        // when player jumps into the air
        canBounce = false;
        boost = false;
    }
    void UpdateUI()
    {
        // update points in the UI
        Points_UI.GetComponent<TextMeshProUGUI>().text = Points.ToString();
    }
}
