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
    public int maxGumballs;

    private int chewsUntilSticky = 10;
    private int numberGumballs = 0;
    private bool hasGumInMouth = false; //unique animation

    //references to interactable objects and their colliders
    public GameObject steering;
    public GameObject defenses;
    public GameObject dispenser;
   
    private Collider2D steeringCol;
    private Collider2D defensesCol;
    private Collider2D gumDispenserCol;

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        steeringCol = steering.GetComponent<Collider2D>();
        defensesCol = defenses.GetComponent<Collider2D>();
        gumDispenserCol = dispenser.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate(){

        //default animator resets
        anim.SetBool("isGettingGum", false);

        //Thrust
        if (Input.GetKey(KeyCode.W)) {
            rb.AddRelativeForce(Vector2.up * thrust);
            anim.SetBool("isMoving", true);
        }
        else if (Input.GetKey(KeyCode.S)) {
            rb.AddRelativeForce(Vector2.down * thrust);
            anim.SetBool("isMoving", true);
        }

        //Rotation
        if (Input.GetKey(KeyCode.A)) {
            rb.AddTorque(rotationalTorque * 1, ForceMode2D.Force);
            anim.SetBool("isMoving", true);
        }
        else if (Input.GetKey(KeyCode.D)) {
            rb.AddTorque(rotationalTorque * -1, ForceMode2D.Force);
            anim.SetBool("isMoving", true);
        }

        //special case for idle animation update; have to check for absense of primary inputs due to my dumb code structure
        if(!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))) {
            anim.SetBool("isMoving", false);
        }

        //Interactions with objects
        if (Input.GetKeyDown(KeyCode.Space)) {  //using keyDOWN since we want a trigger on each hit of the key

            if (!hasGumInMouth) {
                anim.SetBool("isGettingGum", true);
            }

            //TODO D U N K I N G (collider check -> anim -> reset gumInMouth = false)
            else if (hasGumInMouth && (chewsUntilSticky <= 0) /*&& is touching the hole*/) {

            }

            else if(col.IsTouching(defensesCol)){
                //TODO what do do when interacting with defenses
            }
            else if (col.IsTouching(steeringCol)) {
                //TODO what do do when interacting with steering/speedup
            }
            else if (col.IsTouching(gumDispenserCol)) { //just increment number of gumballs in inventory if allowed
                if(numberGumballs+1 < maxGumballs) {
                    numberGumballs++;
                }
            }
            else {  //player is not interacting, so CHEW IT
                //TODO chew 
                anim.SetBool("isChewing", true);
                if(chewsUntilSticky > 0) {
                    chewsUntilSticky--;
                }
            }
        }
        else {  //Space is not hit, so the player cannot be chewing
            anim.SetBool("isChewing", false);
        }
    }
}
