using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    public float thrust;
    public float rotationalTorque;

    private Collider2D gumDispenserCol;
    private Collider2D steeringCol;
    private Collider2D defensesCol;


    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        //TODO
        //gumDispenserCol = GameObject.Find("").GetComponent<Collider2D>();
        //steeringCol = GameObject.Find("").GetComponent<Collider2D>();
        //defensesCol = GameObject.Find("").GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate(){

        //Thrust
        if (Input.GetKey(KeyCode.W)) {
            rb.AddRelativeForce(Vector2.up * thrust);
        }
        else if (Input.GetKey(KeyCode.S)) {
            rb.AddRelativeForce(Vector2.down * thrust);
        }

        //Rotation
        if (Input.GetKey(KeyCode.A)) {
            rb.AddTorque(rotationalTorque * 1, ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.D)) {
            rb.AddTorque(rotationalTorque * -1, ForceMode2D.Force);
        }

        //interactions with objects
        if (Input.GetKey(KeyCode.Space)) {
            //TODO
            if(col.IsTouching(defensesCol)){
                
            }
        }
    }

    public void increaseShipSpeed() {

    }
}
