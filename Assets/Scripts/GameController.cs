using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    // Speed Will be Represented From 10 to 20 to 30;
    public float shipSpeed;

    public float tierOne = 10;
    public float tierTwo = 20;
    public float tierThree = 30;
    public float maxSpeed = 40;

    public float speedDecayRate = 5;

    // What tier we are currently in
    public int currentTier = 1;

    // Controlled in PlayerController Class
    public bool GoWasPressed;

    public float distanceTravelled;
    public int playerScore;

    // What Distance the Next Enemy Attack will be Triggered At
    // This Distance is Randomly Generated 
    // The Spawn of Enemies Increases as a Function of the Player Score
    private float spawnDistance;
    private int lowerSpawnBound;
    private int upperSpawnBound;

    public float oxygenLevel;

    public int unchewedGum;
    public bool gumReady;

    private bool beingAttacked;

    //private EnemyController enemyEncounter;



    //

    // Start is called before the first frame update
    void Start() {
        gumReady = false;
        GoWasPressed = false;
        playerScore = 0;
        shipSpeed = 0;

        updateSpawnDistance();

    }

    // Update is called once per frame
    void Update() {

        updateDistance();

        updateShipSpeed();

        updateEnemySpawnRates();

        if (beingAttacked) {
            //enemyEncounter.updateEnemies();
        }

        if (distanceTravelled < spawnDistance) {
            //enemyEncounter = new EnemyController;
        }

    }

    //**********************************************************************************************************************
    // Ship Movement
    //**********************************************************************************************************************

    void updateDistance() {
        distanceTravelled += shipSpeed;
        playerScore += (int)shipSpeed;
    }

    // Ship accelerates 5x faster then it decelerates
    void updateShipSpeed() {
        if (GoWasPressed) {
            shipSpeed += 5;
        }
        else {
            shipSpeed -= 1;
        }
    }

    //**********************************************************************************************************************
    // Enemy Attacks
    //**********************************************************************************************************************

    // After an attack updateSpawnDistance determines at what distance the next attack should be triggered
    private void updateSpawnDistance() {
        float nextInterval = Random.Range(lowerSpawnBound, upperSpawnBound);
        spawnDistance = distanceTravelled + nextInterval;
    }

    // How fast enemies spawn should ne a function of the player score
    private void updateEnemySpawnRates() {
        lowerSpawnBound = 80 / playerScore;
        upperSpawnBound = 100 / playerScore;
    }

    //**********************************************************************************************************************
    // Ship Status
    //**********************************************************************************************************************

}
