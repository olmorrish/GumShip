using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    // Speed Will be Represented From 10 to 20 to 30;
    public float shipSpeed;

    public float tierOne = 25;
    public float tierTwo = 50;
    public float tierThree = 75;
    public float maxSpeed = 100;

    // What tier we are currently in
    public int currentTier = 1;

    // Controlled in PlayerController Class
    // Turned on in PlayerController Class
    // Turned 
    public bool GoWasPressed;
    public bool blastWasPressed;

    public float distanceTravelled;
    public int playerScore;

    // What Distance the Next Enemy Attack will be Triggered At
    // This Distance is Randomly Generated 
    // The Spawn of Enemies Increases as a Function of the Player Score
    private float spawnDistance;
    private int lowerSpawnBound;
    private int upperSpawnBound;


    // Goes from 100 to 0 
    public float oxygenLevel;

    public int numHoles;

    // Hole Management
    // Hole States 
    // C# Arrays Default to 0 for int
    //  - 0 = OFF
    //  - 1 = Basic Hole
    //  - 2 = Gummy Hole
    //  - 3 = Plugged with Gum
    // 14 is also hard coded in hole sprite function
    int[] holes = new int[14];

    public int currentGunCharge;
    public bool gunReady;

    public EnemyController enemyEncounter;

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

    //private EnemyController enemyEncounter;

    // Start is called before the first frame update
    void Start() {
        GoWasPressed = false;
        playerScore = 0;
        shipSpeed = 0;
        numHoles = 0;
        currentGunCharge = 0;
        oxygenLevel = 100;

        updateSpawnDistance();
        /*
        animator = GetComponent<Animator>();

        animator.SetString("slow1", slow1);
        animator.SetString("slow2", slow2);
        animator.SetString("slow3", slow3);

        animator.SetString("med1", med1);
        animator.SetString("med2", med2);
        animator.SetString("med3", med3);

        animator.SetString("fast1", fast1);
        animator.SetString("fast2", fast2);
        animator.SetString("fast3", fast3);*/
    }

    // Update is called once per frame

    void FixedUpdate(){

        updateDistance();

        updateShipSpeed();
        
        // This updates the enemy encounter
        // Should set correct enemy sprites to active
        // 
        if (enemyEncounter != null)
        {
            enemyEncounter.updateAttack();
            setEnemySprites(enemyEncounter.attacking, enemyEncounter.typesOfEnemiesInSlots);
        }

        if ((distanceTravelled < spawnDistance) && (enemyEncounter == null))
        {
            enemyEncounter = new EnemyController(playerScore);
            setEnemySprites(enemyEncounter.attacking, enemyEncounter.typesOfEnemiesInSlots);
        }

        updateGunCharge();

        // If gun is ready it fires and enemy encounter ends
        // A sprite will need to be played
        // All enemy sprites should be turned off
        if (gunReady)
        {
            enemyEncounter = null;
            //animator.setBool("Blast", true);
            clearEnemySprites();
            updateEnemySpawnRates();
            updateSpawnDistance();
            gunReady = false;
            currentGunCharge = 0;
        }

        updateHoleSprites();
        updateEnemySprites();

        updateEnemySpawnRates();

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
    void updateSpawnDistance() {
        float nextInterval = Random.Range(lowerSpawnBound, upperSpawnBound);
        spawnDistance = distanceTravelled + nextInterval;
    }

    // How fast enemies spawn should ne a function of the player score
    void updateEnemySpawnRates() {
        lowerSpawnBound = 80 / playerScore;
        upperSpawnBound = 100 / playerScore;
    }

    void setEnemySprites(bool[] attacking, int[] activeEnemies)
    {
        // First Enemy
        if (activeEnemies[0] == 1)
        {
            if (attacking[0])
            {
                slow1 = "ATTACK";
                attacking[0] = false;
            }
            else
            {
                slow1 = "IDLE";
            }
        }
        else if (activeEnemies[0] == 2)
        {
            if (attacking[0])
            {
                med1 = "ATTACK";
                attacking[0] = false;
            }
            else
            {
                med1 = "IDLE";
            }
        }
        else if (activeEnemies[0] == 3)
        {
            if (attacking[0])
            {
                fast1 = "ATTACK";
                attacking[0] = false;
            }
            else
            {
                fast1 = "IDLE";
            }
        }

        // Second Enemy
        if (activeEnemies[1] == 1)
        {
            if (attacking[1])
            {
                slow2 = "ATTACK";
                attacking[1] = false;
            }
            else
            {
                slow2 = "IDLE";
            }
        }
        else if (activeEnemies[1] == 2)
        {
            if (attacking[1])
            {
                med2 = "ATTACK";
                attacking[1] = false;
            }
            else
            {
                med2 = "IDLE";
            }
        }
        else if (activeEnemies[1] == 3)
        {
            if (attacking[1])
            {
                fast2 = "ATTACK";
                attacking[1] = false;
            }
            else
            {
                fast2 = "IDLE";
            }
        }

        // Third Enemy
        if (activeEnemies[2] == 1)
        {
            if (attacking[2])
            {
                slow3 = "ATTACK";
                attacking[2] = false;
            }
            else
            {
                slow3 = "IDLE";
            }
        }
        else if (activeEnemies[2] == 2)
        {
            if (attacking[2])
            {
                med3 = "ATTACK";
                attacking[2] = false;
            }
            else
            {
                med3 = "IDLE";
            }
        }
        else if (activeEnemies[2] == 3)
        {
            if (attacking[2])
            {
                fast3 = "ATTACK";
                attacking[2] = false;
            }
            else
            {
                fast3 = "IDLE";
            }
        }
    }

    private void clearEnemySprites()
    {
        slow1 = "OFF";
        slow2 = "OFF";
        slow3 = "OFF";
        med1 = "OFF";
        med2 = "OFF";
        med3 = "OFF";
        fast1 = "OFF";
        fast2 = "OFF";
        fast3 = "OFF";
    }

    private void updateEnemySprites()
    {/*
        animator.SetString("slow1", slow1);
        animator.SetString("slow2", slow2);
        animator.SetString("slow3", slow3);

        animator.SetString("med1", med1);
        animator.SetString("med2", med2);
        animator.SetString("med3", med3);

        animator.SetString("fast1", fast1);
        animator.SetString("fast2", fast2);
        animator.SetString("fast3", fast3);*/
    }

//**********************************************************************************************************************
// Ship Status
//**********************************************************************************************************************

    void updateHoleSprites()
    {
        for (int i = 0; i < 14; i++)
        {
            // TO-DO PLAY CORRECT SPRITE BASED ON HOLE STATUSES
        }
    }

    private void updateOxygen()
    {
        if (numHoles > 0)
        {
            oxygenLevel -= numHoles / 2;
        }
    }

    public void updateGunCharge()
    {
        if (blastWasPressed)
        {
            currentGunCharge++;
        }

        if (currentGunCharge == 5)
        {
            gunReady = true;
        }
    }


}
