using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private Animator anim;

    public float thrust;
    public float rotationalTorque;

    public int numberGumballs;
    public int chewsUntilSticky;

    private Collider2D gumDispenserCol;
    private Collider2D steeringCol;
    private Collider2D defensesCol;

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

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
            anim.SetBool("isMoving", true);
        }
        else if (Input.GetKey(KeyCode.S)) {
            rb.AddRelativeForce(Vector2.down * thrust);
            anim.SetBool("isMoving", true);
        }
        else {
            anim.SetBool("isMoving", false);
        }

        //Rotation
        if (Input.GetKey(KeyCode.A)) {
            rb.AddTorque(rotationalTorque * 1, ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.D)) {
            rb.AddTorque(rotationalTorque * -1, ForceMode2D.Force);
        }

        //Interactions with objects
        if (Input.GetKey(KeyCode.Space)) {

            if(col.IsTouching(defensesCol)){
                //what do do when interacting with defenses
            }
            else if (col.IsTouching(steeringCol)) {
                //what do do when interacting with steering/speedup
            }
            else if (col.IsTouching(gumDispenserCol)) {
                //what do do when interacting with dispenser
            }
            else {  //player is not interacting, so CHEW
                //TODO chew 
                anim.SetBool("isChewing", true);
            }
        }
        else {  //the player cannot be chewing, so let the animator know
            anim.SetBool("isChewing", false);
        }
    }
}
