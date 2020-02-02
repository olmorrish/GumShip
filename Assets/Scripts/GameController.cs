using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour {

    public float tankDepletionDivisor = 0.1f;

    private int oxyCounter; 

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
    // Turned off in GameContoller
    public bool goWasPressed;
    public bool blastWasPressed;
    public int fillHole;

    private int timeToDecay;

    private bool underAttack;

    public float distanceTravelled;
    public int playerScore;

    // What Distance the Next Enemy Attack will be Triggered At
    // This Distance is Randomly Generated 
    // The Spawn of Enemies Increases as a Function of the Player Score
    private float spawnDistance;
    private int lowerSpawnBound;
    private int upperSpawnBound;

    int counter = 0;

    // Goes from 100 to 0 
    public int oxygenLevel;

    public int numHoles;

    // Hole Management
    // Hole States 
    // C# Arrays Default to 0 for int
    //  - 0 = OFF
    //  - 1 = Basic Hole
    //  - 2 = Gummy Hole
    //  - 3 = Plugged with Gum
    int[] holes;
    int numPossibleHoles = 12;

    Animator[] holesAnim;

    public int currentGunCharge;
    public bool gunReady;

    // Hold Animation
    private int slow1Hold;
    private int slow2Hold;
    private int slow3Hold;

    public EnemyController enemyEncounter;

    // Enemy Triggers
    // Can be set to:
    //      - ATTACK = 1
    //      - IDLE   = 2
    //      - DYING  = 3
    //      - OFF    = -1
    private int slow1 = -1;
    private int slow2 = -1;
    private int slow3 = -1;

    private int med1 = -1;
    private int med2 = -1;
    private int med3 = -1;

    private int fast1 = -1;
    private int fast2 = -1;
    private int fast3 = -1;

    // Oxygen Tank
    public GameObject oxy_tank_obj;
    private Animator oxy_tank_anim;

    public Text scoreText;

    // Hole Objects
    public GameObject hole_obj_0;
    public GameObject hole_obj_1;
    public GameObject hole_obj_2;
    public GameObject hole_obj_3;
    public GameObject hole_obj_4;
    public GameObject hole_obj_5;
    public GameObject hole_obj_6;
    public GameObject hole_obj_7;
    public GameObject hole_obj_8;
    public GameObject hole_obj_9;
    public GameObject hole_obj_10;
    public GameObject hole_obj_11;

    private Animator hole_anim_0;
    private Animator hole_anim_1;
    private Animator hole_anim_2;
    private Animator hole_anim_3;
    private Animator hole_anim_4;
    private Animator hole_anim_5;
    private Animator hole_anim_6;
    private Animator hole_anim_7;
    private Animator hole_anim_8;
    private Animator hole_anim_9;
    private Animator hole_anim_10;
    private Animator hole_anim_11;

    // Enemy Objects
    public GameObject slow_obj_1;
    public GameObject slow_obj_2;
    public GameObject slow_obj_3;

    public GameObject med_obj_1;
    public GameObject med_obj_2;
    public GameObject med_obj_3;

    public GameObject fast_obj_1;
    public GameObject fast_obj_2;
    public GameObject fast_obj_3;

    private Animator slow_anim_1;
    private Animator slow_anim_2;
    private Animator slow_anim_3;

    private Animator med_anim_1;
    private Animator med_anim_2;
    private Animator med_anim_3;

    private Animator fast_anim_1;
    private Animator fast_anim_2;
    private Animator fast_anim_3;

    // Start is called before the first frame update
    void Start() {

        oxyCounter = 0;

        underAttack = false;

        slow_anim_1 = slow_obj_1.GetComponent<Animator>();
        slow_anim_2 = slow_obj_2.GetComponent<Animator>();
        slow_anim_3 = slow_obj_3.GetComponent<Animator>();

        med_anim_1 = med_obj_1.GetComponent<Animator>();
        med_anim_2 = med_obj_2.GetComponent<Animator>();
        med_anim_3 = med_obj_3.GetComponent<Animator>();

        fast_anim_1 = fast_obj_1.GetComponent<Animator>();
        fast_anim_2 = fast_obj_2.GetComponent<Animator>();
        fast_anim_3 = fast_obj_3.GetComponent<Animator>();

        hole_anim_0 = hole_obj_0.GetComponent<Animator>();
        hole_anim_1 = hole_obj_1.GetComponent<Animator>();
        hole_anim_2 = hole_obj_2.GetComponent<Animator>();
        hole_anim_3 = hole_obj_3.GetComponent<Animator>();
        hole_anim_4 = hole_obj_4.GetComponent<Animator>();
        hole_anim_5 = hole_obj_5.GetComponent<Animator>();
        hole_anim_6 = hole_obj_6.GetComponent<Animator>();
        hole_anim_7 = hole_obj_7.GetComponent<Animator>();
        hole_anim_8 = hole_obj_8.GetComponent<Animator>();
        hole_anim_9 = hole_obj_9.GetComponent<Animator>();
        hole_anim_10 = hole_obj_10.GetComponent<Animator>();
        hole_anim_11 = hole_obj_11.GetComponent<Animator>();

        oxy_tank_anim = oxy_tank_obj.GetComponent<Animator>();

        holesAnim = new Animator[numPossibleHoles];

        holesAnim[0] = hole_anim_0;
        holesAnim[1] = hole_anim_1;
        holesAnim[2] = hole_anim_2;
        holesAnim[3] = hole_anim_3;
        holesAnim[4] = hole_anim_4;
        holesAnim[5] = hole_anim_5;
        holesAnim[6] = hole_anim_6;
        holesAnim[7] = hole_anim_7;
        holesAnim[8] = hole_anim_8;
        holesAnim[9] = hole_anim_9;
        holesAnim[10] = hole_anim_10;
        holesAnim[11] = hole_anim_11;

        goWasPressed = false;
        playerScore = 1;
        shipSpeed = 0;
        numHoles = 0;
        currentGunCharge = 0;
        oxygenLevel = 112;
        holes = new int[numPossibleHoles];
        fillHole = -1;

       oxy_tank_anim.SetInteger("TankLevel", oxygenLevel);

        timeToDecay = 0;

        updateSpawnDistance();

        slow_anim_1.SetInteger("slow1", slow1);
        slow_anim_2.SetInteger("slow2", slow2);
        slow_anim_3.SetInteger("slow3", slow3);

        med_anim_1.SetInteger("med1", med1);
        med_anim_2.SetInteger("med2", med2);
        med_anim_3.SetInteger("med3", med3);

        fast_anim_1.SetInteger("fast1", fast1);
        fast_anim_2.SetInteger("fast2", fast2);
        fast_anim_3.SetInteger("fast3", fast3);
    }

    // Update is called once per frame

    void FixedUpdate()
    {

        //Debug.Log("UpperBound: " + upperSpawnBound);
        //Debug.Log("LowerBound: " + lowerSpawnBound);
        //Debug.Log("Distance: " + distanceTravelled);
        //Debug.Log("Spawn Distance: " + spawnDistance);
        //Debug.Log("UnderAttack? " + underAttack);

        timeToDecay++;
        counter++;
        scoreText.text = playerScore.ToString();
        /*
        if (counter > 120)
        {
            goWasPressed = true;
            counter = 0;
        }
        */
        updateDistance();

        updateShipSpeed();

        // This updates the enemy encounter
        // Should set correct enemy sprites to active
        if (underAttack)
        {
            enemyEncounter.updateAttack();
            
           // Debug.Log("NUMBER OF HOLES CREATED: " + enemyEncounter.numberOfHolesCreated);
            for (int i = 0; i < 3; i++)
            {
                //Debug.Log("Attackers: " + enemyEncounter.attacking[i]);
            }
            
            setEnemySprites(enemyEncounter.attacking, enemyEncounter.typesOfEnemiesInSlots);
            if (enemyEncounter.numberOfHolesCreated > 0)
            {
                for (int i = 0; i < enemyEncounter.numberOfHolesCreated; i++)
                {
                    addNewHole();
                }
                enemyEncounter.numberOfHolesCreated = 0;
            }
        }

        if ((spawnDistance < distanceTravelled) && (underAttack == false))
        {
            underAttack = true;

            enemyEncounter = new EnemyController(playerScore);
            //Debug.Log("Num Of Holes" + enemyEncounter.numberOfHolesCreated);

            for (int i = 0; i < 3; i++)
            {
                // Debug.Log("Attackers: " + enemyEncounter.attacking[i]);
            }

            for (int i = 0; i < 3; i++)
            {
                // Debug.Log("Typers: " + enemyEncounter.typesOfEnemiesInSlots[i]);
            }

            setEnemySprites(enemyEncounter.attacking, enemyEncounter.typesOfEnemiesInSlots);
        }

        updateGunCharge();

        // If gun is ready it fires and enemy encounter ends
        // A sprite will need to be played
        // All enemy sprites should be turned off
        if (gunReady)
        {
            underAttack = false;
            //animator.setBool("Blast", true);
            clearEnemySprites();
            updateEnemySpawnRates();
            updateSpawnDistance();
            gunReady = false;
            currentGunCharge = 0;
        }

        updateHoleSprites();
        updateEnemySprites();

        oxyCounter++;
        if (oxyCounter > 60) { 

            updateOxygen();
            oxyCounter = 0;
         }

    }

    //**********************************************************************************************************************
    // Ship Movement
    //**********************************************************************************************************************

    void updateDistance() {
        distanceTravelled += shipSpeed;
        playerScore = (int)distanceTravelled;
    }

    // Ship accelerates 5x faster then it decelerates
    void updateShipSpeed() {

        if (shipSpeed < 0)
        {
            shipSpeed = 0;
        }

        //Debug.Log("Time To Decay" + timeToDecay);
       // Debug.Log("Ship Speed" + shipSpeed);
        if (goWasPressed) {
          //  Debug.Log("GOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO");
            shipSpeed += 0.1f;
            goWasPressed = false;
        }
        else if(shipSpeed > 0){
          //  Debug.Log("We Hit The Decay");
            shipSpeed -= 0.01f;
            timeToDecay = 0;
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
        //lowerSpawnBound = 80 / playerScore;
        //upperSpawnBound = 100 / playerScore;
        lowerSpawnBound = 80;
        upperSpawnBound = 100;
    }

    void setEnemySprites(bool[] attacking, int[] activeEnemies)
    {
        // First Enemy
        if (activeEnemies[0] == 1)
        {
           // Debug.Log("In Attack Loop");
            if (attacking[0])
            {
               // Debug.Log("In Attack Is Set");
                // ATTACK
                slow1 = 1;
                attacking[0] = false;
            }
            else
            {
                // IDLE
                slow1 = 2;
            }
        }
        else if (activeEnemies[0] == 2)
        {
            if (attacking[0])
            {
                // ATTACK
                med1 = 1;
                attacking[0] = false;
            }
            else
            {
                // IDLE
                med1 = 2;
            }
        }
        else if (activeEnemies[0] == 3)
        {
            if (attacking[0])
            {
                // ATTACK
                fast1 = 1;
                attacking[0] = false;
            }
            else
            {
                // IDLE
                fast1 = 2;
            }
        }

        // Second Enemy
        if (activeEnemies[1] == 1)
        {
            if (attacking[1])
            {
                // ATTACK
                slow2 = 1;
                attacking[1] = false;
            }
            else
            {
                // IDLE
                slow2 = 2;
            }
        }
        else if (activeEnemies[1] == 2)
        {
            if (attacking[1])
            {
                // ATTACK
                med2 = 1;
                attacking[1] = false;
            }
            else
            {
                // IDLE
                med2 = 2;
            }
        }
        else if (activeEnemies[1] == 3)
        {
            if (attacking[1])
            {
                // ATTACK
                fast2 = 1;
                attacking[1] = false;
            }
            else
            {
                // IDLE
                fast2 = 2;
            }
        }

        // Third Enemy
        if (activeEnemies[2] == 1)
        {
            if (attacking[2])
            {
                // ATTACK
                slow3 = 1;
                attacking[2] = false;
            }
            else
            {
                // IDLE
                slow3 = 2;
            }
        }
        else if (activeEnemies[2] == 2)
        {
            if (attacking[2])
            {
                // ATTACK
                med3 = 1;
                attacking[2] = false;
            }
            else
            {
                // IDLE
                med3 = 2;
            }
        }
        else if (activeEnemies[2] == 3)
        {
            if (attacking[2])
            {
                // ATTACK
                fast3 = 1;
                attacking[2] = false;
            }
            else
            {
                // IDLE
                fast3 = 2;
            }
        }
    }

    private void clearEnemySprites()
    {
        slow1 = -1;
        slow2 = -1;
        slow3 = -1;
        med1 =  -1;
        med2 =  -1;
        med3 =  -1;
        fast1 = -1;
        fast2 = -1;
        fast3 = -1;
    }

    private void updateEnemySprites()
    {

        if (slow1 == 1)
        {
            slow1Hold = 10;
        }
        if (slow1Hold > 0)
        {
            slow1Hold--;
            slow_anim_1.SetInteger("slow1", 1);
        }
        else
        {
            slow_anim_1.SetInteger("slow1", slow1);
        }

        //Debug.Log("Slow1: " + slow1);
        //Debug.Log("Slow2: " + slow2);
        //Debug.Log("Slow3: " + slow3);

        //Debug.Log("Med1: " + med1);
        //Debug.Log("Med2: " + med2);
        //Debug.Log("Med3: " + med3);

        //Debug.Log("Fast1: " + fast1);
        //Debug.Log("Fast2: " + fast2);
        //Debug.Log("Fast3: " + fast3);

        slow_anim_2.SetInteger("slow2", slow2);
        slow_anim_3.SetInteger("slow3", slow3);

        med_anim_1.SetInteger("med1", med1);
        med_anim_2.SetInteger("med2", med2);
        med_anim_3.SetInteger("med3", med3);

        fast_anim_1.SetInteger("fast1", fast1);
        fast_anim_2.SetInteger("fast2", fast2);
        fast_anim_3.SetInteger("fast3", fast3);
    }

//**********************************************************************************************************************
// Ship Status
//**********************************************************************************************************************

    void updateHoleSprites()
    {
        for (int i = 0; i < numPossibleHoles; i++)
        {
            holesAnim[i].SetInteger("holeState", holes[i]);
        }
    }

    void addNewHole()
    {
        int startIndice = Random.Range(0, (numPossibleHoles - 1));
        
        for (int i = 0; i < numPossibleHoles; i++)
        {
            if (startIndice + i >= numPossibleHoles)
            {
                startIndice = -i;
            }
            if (holes[startIndice + i] == 0)
            {
                holes[startIndice + i] = 1;
                numHoles++;
                return;
            }
            if (holes[startIndice + i] == 3)
            {
                holes[startIndice + i] = 2;
                numHoles++;
                return;
            }
        }
    }

    private void updateOxygen()
    {
        if (numHoles > 0)
        {
            oxygenLevel -= numHoles / 2;
            oxy_tank_anim.SetInteger("TankLevel", oxygenLevel);

        }
    }

    public void updateGunCharge()
    {
        if (blastWasPressed)
        {
            currentGunCharge++;
            blastWasPressed = false;
        }

        if (currentGunCharge == 5)
        {
            gunReady = true;
        }
    }

    public bool canBeFilled(string nameHole)
    {
        int numHole = -1;

        Debug.Log("NAAAAAAAAAAAAAAAAAA: " + nameHole);
        if ("Hole_0" == nameHole)
        {
            numHole = 0;
        }
        else if ("Hole_1" == nameHole)
        {
            numHole = 1;
        }
        else if ("Hole_2" == nameHole)
        {
            numHole = 2;
        }
        else if ("Hole_3" == nameHole)
        {
            numHole = 3;
        }
        else if ("Hole_4" == nameHole)
        {
            numHole = 4;
        }
        else if ("Hole_5" == nameHole)
        {
            numHole = 5;
        }
        else if ("Hole_6" == nameHole)
        {
            numHole = 6;
        }
        else if ("Hole_7" == nameHole)
        {
            numHole = 7;
        }
        else if ("Hole_8" == nameHole)
        {
            numHole = 8;
        }
        else if ("Hole_9" == nameHole)
        {
            numHole = 9;
        }
        else if ("Hole_10" == nameHole)
        {
            numHole = 10;
        }
        else if ("Hole_11" == nameHole)
        {
            numHole = 11;
        }

        if (numHole == -1)
        {
            return false;
        }

        
        // If there is a hole
        if ((holes[numHole] == 1) || (holes[numHole] == 2))
        {
            // Plug hole with gum
            holes[numHole] = 3;
            numHoles--;
            return true;
        }
        else
        {
            return false;
        }
    }

    /*void OnGUI()
    {
        int x = 10;
        int y = 10;
        int w = 100;
        int h = 20;
        GUI.Label(new Rect(x, y, w, h), "Score: " + playerScore);
    }*/


}
