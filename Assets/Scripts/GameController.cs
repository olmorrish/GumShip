using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
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

    // Hole Management
    // Hole States 
    // C# Arrays Default to 0 for int
    //  - 0 = OFF
    //  - 1 = Basic Hole
    //  - 2 = Gummy Hole
    //  - 3 = Plugged with Gum
    int numberOfHoles = 14;
    int[] holes = new int[numberOfHoles];

    public bool gunReady;

    private bool beingAttacked;

    private EnemyController enemyEncounter;

    // Enemy Triggers
    // Can be set to:
    //      - ATTACK
    //      - IDLE
    //      - DYING
    //      - OFF
    public string slow1 = "OFF";
    public string slow2 = "OFF";
    public string slow3 = "OFF";

    public string med1 = "OFF";
    public string med2 = "OFF";
    public string med3 = "OFF";

    public string fast1 = "OFF";
    public string fast2 = "OFF";
    public string fast3 = "OFF";

    //public int unchewedGum;
    //public bool gumReady;

    // Start is called before the first frame update
    void Start() {
        gumReady = false;
        GoWasPressed = false;
        playerScore = 0;
        shipSpeed = 0;

        updateSpawnDistance(); 

    }

    // Update is called once per frame
    void Update(){

        updateDistance();

        updateShipSpeed();

        updateEnemySpawnRates();
        
        // This updates the enemy encounter
        // Should set correct enemy sprites to active
        // 
        if (enemyEncounter != null)
        {
            enemyEncounter.updateEnemies();
        }

        if ((distanceTravelled < spawnDistance) && (enemyEncounter == null))
        {
            enemyEncounter = new EnemyController(playerScore);
            // TO-DO 
        }

        // If gun is ready it fires and enemy encounter ends
        // A sprite will need to be played
        // All enemy sprites should be turned off
        if (gunReady)
        {
            enemyEncounter = null;
            // TO-DO PLAY BLAST ANIMATION
            // TO-DO TURN ENEMY SPRITES OFF
        }

        generateHoleSprites();
        
    }

//**********************************************************************************************************************
// Ship Movement
//**********************************************************************************************************************

    void updateDistance()
    {
        distanceTravelled += shipSpeed;
        playerScore += (int)shipSpeed;
    }

    // Ship accelerates 5x faster then it decelerates
    void updateShipSpeed()
    {
        if (GoWasPressed)
        {
            shipSpeed += 5;
        }
        else
        {
            shipSpeed -= 1;
        }
    }

//**********************************************************************************************************************
// Enemy Attacks
//**********************************************************************************************************************

    // After an attack updateSpawnDistance determines at what distance the next attack should be triggered
    private void updateSpawnDistance()
    {
        float nextInterval = Random.Range(lowerSpawnBound, upperSpawnBound);
        spawnDistance = distanceTravelled + nextInterval;
    }

    // How fast enemies spawn should ne a function of the player score
    private void updateEnemySpawnRates()
    {
        lowerSpawnBound = 80/playerScore;
        upperSpawnBound = 100/playerScore;
    }

//**********************************************************************************************************************
// Ship Status
//**********************************************************************************************************************

    void generateHoleSprites()
    {
        for (int i = 0; i < numOfHoles; i++)
        {
            // TO-DO PLAY CORRECT SPRITE BASED ON HOLE STATUSES
        }
    }

}
