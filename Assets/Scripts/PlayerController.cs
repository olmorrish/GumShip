using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private Animator animPlayer;
    public GameObject gumOverlay;
    private Animator animGum;

    public GameObject gameControllerObj;
    private GameController controller;

    //SO MANY GAMEOBJECTS FFS
    public GameObject bubbleGumDispenser;
    private Animator animDispenser;
    public GameObject goButton;
    private Animator animGoButton;
    public GameObject fireButton;
    private Animator animFireButton;

    //movement variables
    public float thrust;
    public float rotationalTorque;

    //gum status variables
    public int chewsToStickyMax = 20;
    public int chewsUntilSticky;
    private bool hasGumInMouth = false; 

    //hole detection variables
    private bool holeToPlug = false;
    public Transform holeChecker;
    public LayerMask whatIsHole;
    const float holeCheckRadius = 1f;   //radius around point to collision-check

    //references to interactable objects and their colliders
    public GameObject steering;
    public GameObject defenses;
    public GameObject dispenser;
    private Collider2D steeringCol;
    private Collider2D defensesCol;
    private Collider2D gumDispenserCol;

    // Start is called before the first frame update
    void Start(){
        controller = gameControllerObj.GetComponent<GameController>();
        animDispenser = bubbleGumDispenser.GetComponent<Animator>();
        animGoButton = goButton.GetComponent<Animator>();
        animFireButton = fireButton.GetComponent<Animator>();

        chewsUntilSticky = chewsToStickyMax;

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animPlayer = GetComponent<Animator>();
        animGum = gumOverlay.GetComponent<Animator>();
        hasGumInMouth = false;
        animGum.SetBool("hasGumInMouth", false);

        steeringCol = steering.GetComponent<Collider2D>();
        defensesCol = defenses.GetComponent<Collider2D>();
        gumDispenserCol = dispenser.GetComponent<Collider2D>();
    }

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(holeChecker.transform.position, holeCheckRadius);
    }

    // Update is called once per frame
    void Update(){

        //default animator resets
        animPlayer.SetBool("isGettingGum", false);
        animGum.SetBool("isGettingGum", false);

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
        }
        else if (Input.GetKey(KeyCode.S)) {
            rb.AddRelativeForce(Vector2.down * thrust);
        }

        //Rotation movement
        if (Input.GetKey(KeyCode.A)) {
            rb.AddTorque(rotationalTorque * 1, ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.D)) {
            rb.AddTorque(rotationalTorque * -1, ForceMode2D.Force);
        }

        //special case for idle animation update
        if(rb.velocity.magnitude < 0.1) {
        }

        //All interactions via SpaceBar
        if (Input.GetKeyDown(KeyCode.Space)) {  //using keyDOWN since we want a trigger on each hit of the key

            //2. Dunk gum if you can
            if (hasGumInMouth && (chewsUntilSticky <= 0) && holeToPlug) {
                Debug.Log("SpaceBar hit -> Player is plugging a hole.");
                animPlayer.SetBool("isDunking", true);
                animGum.SetBool("isDunking", true);
                animGum.SetBool("hasGumInMouth", false);
                hasGumInMouth = false;
                //TODO interaction with the hole object
            }

            //3. Fire defense system
            else if(col.IsTouching(defensesCol)){
                Debug.Log("SpaceBar hit -> Player is activating defenses.");
                animFireButton.SetBool("isPushed", true);
                controller.blastWasPressed = true;
            }

            //4. Speed up the ship
            else if (col.IsTouching(steeringCol)) {
                Debug.Log("SpaceBar hit -> Player is piloting ship.");
                animGoButton.SetBool("isPushed", true);
                controller.goWasPressed = true;
            }

            //5. Collect more gumballs
            else if (col.IsTouching(gumDispenserCol) && !hasGumInMouth) { 
                Debug.Log("SpaceBar hit -> Player is getting a new gumball from the dispenser.");
                chewsUntilSticky = chewsToStickyMax;
                animDispenser.SetBool("Dispensing", true);
                animPlayer.SetBool("isGettingGum", true);
                animGum.SetBool("isGettingGum", true);
                animGum.SetBool("hasGumInMouth", true);
                hasGumInMouth = true;
            }

            //6. No other options; player must be trying to chew the gum
            else{
                Debug.Log("SpaceBar hit -> Player is chewing gum.");
                animPlayer.SetBool("isChewing", true);
                animGum.SetBool("isChewing", true);   
                if (chewsUntilSticky > 0) {
                    chewsUntilSticky--;
                }
            }


        }
        else {  //Space is not hit, so the player cannot be chewing or interacting with anything
            animPlayer.SetBool("isDunking", false);       //TODO ensure this doesn't break 
            animPlayer.SetBool("isChewing", false);

            animGum.SetBool("isDunking", false);       //TODO ensure this doesn't break 
            animGum.SetBool("isChewing", false);

            animDispenser.SetBool("Dispensing", false);
            animGoButton.SetBool("isPushed", false);
            animFireButton.SetBool("isPushed", false);
        }
    }
}
