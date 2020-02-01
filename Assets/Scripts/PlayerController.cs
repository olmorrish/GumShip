using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private Animator anim;

    //movement variables
    public float thrust;
    public float rotationalTorque;
    public int maxGumballs;

    //gum status variables
    private int chewsUntilSticky = 10;
    public int numberGumballs = 0;
    private bool hasGumInMouth = false; //unique animation

    //hole detection variables
    private bool holeToPlug = false;
    public Transform holeChecker;
    public LayerMask whatIsHole;
    const float holeCheckRadius = 0.1f;   //radius around point to collision-check

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

        //check if there is a hole below the player where they can plug it
        Collider2D[] belowPlayerCollisions = Physics2D.OverlapCircleAll
            (holeChecker.position, holeCheckRadius, whatIsHole);
        foreach (Collider2D col in belowPlayerCollisions) {
            Debug.Log("Hole collision activated.");
            if (whatIsHole.Contains(col.gameObject.layer)) {  //utilizes extension method!
                holeToPlug = true;
            }
            else {
                holeToPlug = false;    //the value of holeToPlug is used to determine if a "spacebar" input results in a dunk or not
            }
        }

        //Thrust movement
        if (Input.GetKey(KeyCode.W)) {
            rb.AddRelativeForce(Vector2.up * thrust);
            anim.SetBool("isMoving", true);
        }
        else if (Input.GetKey(KeyCode.S)) {
            rb.AddRelativeForce(Vector2.down * thrust);
            anim.SetBool("isMoving", true);
        }

        //Rotation movement
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

        //All interactions via SpaceBar
        if (Input.GetKeyDown(KeyCode.Space)) {  //using keyDOWN since we want a trigger on each hit of the key

            //1. Get gum into mouth before trying anything else
            if (!hasGumInMouth && numberGumballs > 0) {
                Debug.Log("SpaceBar hit -> Player is putting gum in mouth.");
                anim.SetBool("isGettingGum", true);
                hasGumInMouth = true;
                numberGumballs--;
            }

            //2. Dunk gum if you can
            else if (hasGumInMouth && (chewsUntilSticky <= 0) && holeToPlug) {
                Debug.Log("SpaceBar hit -> Player is plugging a hole.");
                anim.SetBool("isDunking", true);
                hasGumInMouth = false;
                //TODO interaction with the hole object
            }

            //3. Fire defense system
            else if(col.IsTouching(defensesCol)){
                Debug.Log("SpaceBar hit -> Player is activating defenses.");
                //TODO what do do when interacting with defenses
            }

            //4. Speed up the ship
            else if (col.IsTouching(steeringCol)) {
                Debug.Log("SpaceBar hit -> Player is piloting ship.");
                //TODO what do do when interacting with steering/speedup
            }

            //5. Collect more gumballs
            else if (col.IsTouching(gumDispenserCol) && (numberGumballs < maxGumballs)) { 
                Debug.Log("SpaceBar hit -> Player is getting a new gumball from the dispenser.");
                numberGumballs++;

            }

            //6. No other options; player must be trying to chew the gum
            else {
                Debug.Log("SpaceBar hit -> Player is chewing gum.");
                anim.SetBool("isChewing", true);
                if(chewsUntilSticky > 0) {
                    chewsUntilSticky--;
                }
            }
        }
        else {  //Space is not hit, so the player cannot be chewing or interacting with anything
            anim.SetBool("isChewing", false);
            anim.SetBool("isInteracting", false);   //TODO ensure this doesn't break shit
            anim.SetBool("isDunking", false);       //TODO ensure this doesn't break shit
        }
    }
}
