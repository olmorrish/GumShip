using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    float numberOfHolesCreated = 0;

    int nextHit = 0;

    bool[] attacking = { false, false, false };
 

    float currentDistance = 0;

    //0 = no enemies
    //1 = narwhal
    //2 = hammerheads
    //3 = whale
    /// <summary>
    /// This is the types of enemies present in each enemy "slot"
    /// </summary>
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
        for(int i = 0; i < 3; i++)
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
    void createEncounter() {
        //make fancy calculations to do a semi random encounter generator based off distance and random chance
        /* Early on should have overall just narwhals and simple fights
         * 
         * Im thinking distance thresholds for when to introduce new enemy types and
         * possibly another threshold
         * 
         */




        typesOfEnemiesInSlots[0] = 1;
        typesOfEnemiesInSlots[1] = 1;
        typesOfEnemiesInSlots[2] = 1;
    }

    /// <summary>
    /// Should be called by gameController once per update. This processes the enemy attacks
    /// </summary>
    public void updateAttack() {
        nextHit++;

        if (nextHit > 180)  //for now one hit every ~3 seconds
        {
            nextHit = 0;

            //go through enemies in slots, get total enemy amt
            int totalEnemies = 0;
            //index 0 = narwhal, 1 = hammerhead, 2 = whale
            int[] numberOfEnemyPerType = { 0, 0, 0 };

            //modify this to store how many enemies of each type to properly handle attacks

            for (int i = 0; i < 3; i++)
            {
                if (typesOfEnemiesInSlots[i] > 0)
                {
                    
                }
            }

            //random chance that each enemy hits, assume it is all simple narwhal for now
            for (int i = 0; i < totalEnemies; i++)
            {
                //update called ~60 times per second, im thinking a hit about once per every 3-6 seconds?
                int hitChance = Random.Range(1, 180);

                if (hitChance < 30)
                {

                }

                if (hitChance < 30)
                    numberOfHolesCreated++;
            }
        }
        
    }

    /// <summary>
    /// Simple helper to successfully do a narwhal attack
    /// </summary>
    /// <returns></returns> The number of holes created (narwhal is always 1)
    private int narwhalAttack()
    {
        return 1;
    }

    /// <summary>
    /// Simple helper to successfully do a hammerhead attack
    /// </summary>
    /// <returns></returns> The number of holes created
    private int hammerheadAttack()
    {
        return 3;
    }

    /// <summary>
    /// Simple helper to successfully do a whale attack
    /// </summary>
    /// <returns></returns> The number of holes created (whale is special, assume 1 hit for now)
    private int whaleAttack()
    {
        return 1;
    }

}

