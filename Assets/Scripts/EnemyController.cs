using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController
{

    public float numberOfHolesCreated = 0;

    private ArrayList possibleEncountersTypes = new ArrayList();
    private ArrayList possibleEncountersStats = new ArrayList();

    private int[] encounter1Types = { 1, 1, 1};
    private bool[] encounter1Stat = { true, true, true };

    private int[] encounter2Types = { 2, 2, 2 };
    private bool[] encounter2Stat = { true, true, true };

    private int[] encounter3Types = { 3, 3, 3 };
    private bool[] encounter3Stat = { true, true, true };

    private int[] encounter4Types = { 2, 1, 1 };
    private bool[] encounter4Stat = { true, true, true };

    private int[] encounter5Types = { 2, 1, 2 };
    private bool[] encounter5Stat = { true, true, true };

    private int[] encounter6Types = { 2, 2, 3 };
    private bool[] encounter6Stat = { true, true, true };

    private int[] encounter7Types = { 1, 2, 3 };
    private bool[] encounter7Stat = { true, true, true };

    private int[] encounter8Types = { 1, 1, 3 };
    private bool[] encounter8Stat = { true, true, true };

    private int[] encounter9Types = { 0, 3, 1 };
    private bool[] encounter9Stat = { false, true, true };

    private int[] encounter10Types = { 1, 0, 3 };
    private bool[] encounter10Stat = { true, false, true };

    private int[] encounter11Types = { 3, 0, 2 };
    private bool[] encounter11Stat = { true, false, true };

    private int[] encounter12Types = { 2, 2, 1 };
    private bool[] encounter12Stat = { true, true, true };



    int nextHit = 0;

    public bool[] attacking = { false, false, false };


    float currentDistance = 0;

    //0 = no enemies
    //1 = narwhal
    //2 = hammerheads
    //3 = whale
    public int[] typesOfEnemiesInSlots = { 0, 0, 0 };


    //creates the class, this will start the encounter as soon as its constructed
    public EnemyController(float distance)
    {
        possibleEncountersTypes.Add(encounter1Types);
        possibleEncountersTypes.Add(encounter2Types);
        possibleEncountersTypes.Add(encounter3Types);
        possibleEncountersTypes.Add(encounter4Types);
        possibleEncountersTypes.Add(encounter5Types);
        possibleEncountersTypes.Add(encounter6Types);
        possibleEncountersTypes.Add(encounter7Types);
        possibleEncountersTypes.Add(encounter8Types);
        possibleEncountersTypes.Add(encounter9Types);
        possibleEncountersTypes.Add(encounter10Types);
        possibleEncountersTypes.Add(encounter11Types);

        possibleEncountersStats.Add(encounter1Stat);
        possibleEncountersStats.Add(encounter2Stat);
        possibleEncountersStats.Add(encounter3Stat);
        possibleEncountersStats.Add(encounter4Stat);
        possibleEncountersStats.Add(encounter5Stat);
        possibleEncountersStats.Add(encounter6Stat);
        possibleEncountersStats.Add(encounter7Stat);
        possibleEncountersStats.Add(encounter8Stat);
        possibleEncountersStats.Add(encounter9Stat);
        possibleEncountersStats.Add(encounter10Stat);
        possibleEncountersStats.Add(encounter11Stat);
        this.setup(distance);
        this.createEncounter();
    }

    /// <summary>
    /// Called in constructor, sets up the various variables for tracking
    /// </summary>
    void setup(float distance)
    {
        this.currentDistance = distance;
        for (int i = 0; i < 3; i++)
        {
            typesOfEnemiesInSlots[i] = 0;
            attacking[i] = false;
        }
        numberOfHolesCreated = 0;

    }


    //need to create encounter based off score/distance travelled for difficulty.
    //for now, always spawn 3 narwhals in slot 0
    /// <summary>
    /// Generates the enemies for the encounter, for now 1 narwhal in each slot
    /// </summary>
    void createEncounter()
    {
        int encounterIndex = Random.Range(0, 12);

        //int[] tempType = (int[])possibleEncountersTypes[encounterIndex];

        //possibleEncountersTypes[encounterIndex].CopyTo(typesOfEnemiesInSlots);
        //possibleEncountersStats[encounterIndex].CopyTo(attacking);


        attacking = (bool[])possibleEncountersStats[encounterIndex];
        typesOfEnemiesInSlots = (int[])possibleEncountersTypes[encounterIndex];

/*
        int numOfNarwhals;
        int numOfHammerheads;
        int totalNumOfEnemies;
        int bias;
        bool spawnWhale = false;

        //spawn 2 always (1 narwhal, 1 hammerhead), 50% chance for whale
        attacking[0] = true;
        attacking[1] = true;

        typesOfEnemiesInSlots[0] = 1;
        typesOfEnemiesInSlots[1] = 2;

        if(Random.Range(1, 2) < 2)  //50% chance to spawn whale
        {
            attacking[2] = true;
            typesOfEnemiesInSlots[2] = 3;
        }
        else
        {
            attacking[2] = false;
            typesOfEnemiesInSlots[2] = 0;
        }
*/

    }

    /// <summary>
    /// Should be called by gameController once per update. This processes the enemy attacks
    /// </summary>
    public void updateAttack()
    {
        nextHit++;
        //narwhals shoot every 4 seconds
        //hammerheads shoot every 9 seconds
        //whales shoot every 15 seconds

        int numOfNarwhals = 0;
        int numOfHammerHeads = 0;
        int numberOfWhales = 0;

        for(int i = 0; i < 3; i++)
        {
            if (typesOfEnemiesInSlots[i] == 1)
                numOfNarwhals++;
            else if (typesOfEnemiesInSlots[i] == 2)
                numOfHammerHeads++;
            else if (typesOfEnemiesInSlots[i] == 3)
                numberOfWhales++;
        }


        if((nextHit % 200 == 0) && numOfNarwhals > 0)   //narwhal shoot
        {
            for(int i = 0; i < 3; i++)
            {
                if (typesOfEnemiesInSlots[i] == 1)   //narwhal
                {
                    attacking[i] = true;
                    narwhalAttack();
                }
            }
        }
        
        if((nextHit % 400 == 0) && numOfHammerHeads > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                if (typesOfEnemiesInSlots[i] == 2)   //hammerhead
                {
                    attacking[i] = true;
                    hammerheadAttack();
                }
            }

        }

        if((nextHit % 600 == 0) && numberOfWhales > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                if (typesOfEnemiesInSlots[i] == 3)   //whale
                {
                    attacking[i] = true;
                    whaleAttack();
                }
            }
        }

    }

    /// <summary>
    /// Simple helper to successfully do a narwhal attack
    /// </summary>
    /// <returns></returns> The number of holes created (narwhal is always 1)
    private void narwhalAttack()
    {
        numberOfHolesCreated += 1;
    }

    /// <summary>
    /// Simple helper to successfully do a hammerhead attack
    /// </summary>
    /// <returns></returns> The number of holes created
    private void hammerheadAttack()
    {
        numberOfHolesCreated += 1;
    }

    /// <summary>
    /// Simple helper to successfully do a whale attack
    /// </summary>
    /// <returns></returns> The number of holes created (whale is special, assume 1 hit for now)
    private void whaleAttack()
    {
        numberOfHolesCreated += 1;
    }

}
