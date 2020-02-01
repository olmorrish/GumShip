using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float numberOfHolesCreated = 0;

    int nextHit = 0;

    public bool[] attacking = { false, false, false };


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
