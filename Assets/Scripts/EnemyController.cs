using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController
{

    public float numberOfHolesCreated = 0;

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
        int numOfNarwhals;
        int numOfHammerheads;
        int totalNumOfEnemies;
        int bias;

        if (currentDistance < 500) //easy, only narwhals 1-3 with a bias to 1
        {
            numOfNarwhals = Random.Range(1, 3);
            bias = Random.Range(1, 2);

            numOfNarwhals -= bias;

            totalNumOfEnemies = numOfNarwhals;

            //check to make sure we still have at least 1 enemy
            if(totalNumOfEnemies < 1)
            {
                totalNumOfEnemies = 1;
            }

            //assign attacking enemy to vars
            for(int i = 0; i < totalNumOfEnemies; i++)
            {
                typesOfEnemiesInSlots[i] = 1;   //just narwhals
            }

        }
        else if(currentDistance < 1000) //medium mix of narwhals and hammerheads with a bias towards narwhals
        {
            numOfNarwhals = Random.Range(2, 3);
            numOfHammerheads = 0;

            if(numOfNarwhals < 3)   //room for hammerhead
            {
                if(Random.Range(1, 2) == 1) //50% chance hammerhead spawns
                {
                    numOfHammerheads = 3 - numOfNarwhals;
                }
                
            }

            totalNumOfEnemies = numOfHammerheads + numOfNarwhals;

            //place into vars
            for(int i = 0; i < totalNumOfEnemies; i++)
            {
                //distribute narwhals and hammerheads across the slots
                if(numOfNarwhals > 0)
                {
                    typesOfEnemiesInSlots[i] = 1;
                    numOfNarwhals--;
                }
                else if(numOfHammerheads > 0)
                {
                    typesOfEnemiesInSlots[i] = 2;
                    numOfHammerheads--;
                }
            }

            

        }
        else if(currentDistance < 1500) //hard mix of narwhals and hammerheads, bias towards hammerheads
        {
            numOfNarwhals = Random.Range(1, 3);
            bias = Random.Range(1, 3);

            numOfNarwhals -= bias;

            if(numOfNarwhals < 0)
            {
                numOfNarwhals = 0;
            }

            //guarenteed to be no greater than 2 narwhals
            numOfHammerheads = 3 - numOfNarwhals;

            totalNumOfEnemies = numOfNarwhals + numOfHammerheads;

            for(int i = 0; i < totalNumOfEnemies; i++)
            {
                if(numOfNarwhals > 0)
                {
                    typesOfEnemiesInSlots[i] = 1;
                    numOfNarwhals--;
                }
                else if(numOfHammerheads > 0)
                {
                    typesOfEnemiesInSlots[i] = 2;
                    numOfHammerheads--;
                }
            }

        }
        else    //expert similar to hard but with a chance to spawn 1 whale
        {
            int numOfWhale = 0;

            numOfNarwhals = Random.Range(1, 2);
            bias = Random.Range(1, 2);

            numOfNarwhals -= bias;

            if (numOfNarwhals < 0)
            {
                numOfNarwhals = 0;
            }


            //guarenteed to be no greater than 2 narwhals
            numOfHammerheads = Random.Range(1, 3);

            totalNumOfEnemies = numOfNarwhals + numOfHammerheads;
            if(totalNumOfEnemies > 3)
            {
                numOfNarwhals = 0;
                totalNumOfEnemies = numOfNarwhals + numOfHammerheads;
            }

            if(totalNumOfEnemies < 3)   //DEPLOY W H A L E
            {
                numOfWhale = 1;
                totalNumOfEnemies += numOfWhale;
            }


            for (int i = 0; i < totalNumOfEnemies; i++)
            {
                if (numOfNarwhals > 0)
                {
                    typesOfEnemiesInSlots[i] = 1;
                    numOfNarwhals--;
                }
                else if (numOfHammerheads > 0)
                {
                    typesOfEnemiesInSlots[i] = 2;
                    numOfHammerheads--;
                }
                else if(numOfWhale > 0)
                {
                    typesOfEnemiesInSlots[i] = 3;
                    numOfWhale--;
                }
            }
        }

    }

    /// <summary>
    /// Should be called by gameController once per update. This processes the enemy attacks
    /// </summary>
    public void updateAttack()
    {
        nextHit++;

        if (nextHit > 180)  //for now one hit every ~3 seconds
        {
            nextHit = 0;    //reset nexthit

            //if enemy exists in slot, set to attacking
            for (int i = 0; i < 3; i++)
            {
                if (typesOfEnemiesInSlots[i] > 0)
                {
                    attacking[i] = true;
                }
            }

            //enemy in the i'th slot is now attacking based on its type
            for (int i = 0; i < 3; i++)
            {
                if (attacking[i])
                {
                    switch (typesOfEnemiesInSlots[i])
                    {
                        case 1: //narwhal
                            narwhalAttack();
                            break;
                        case 2: //hammerhead
                            hammerheadAttack();
                            break;
                        case 3: //whale
                            whaleAttack();
                            break;
                    }
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
        numberOfHolesCreated += 3;
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
