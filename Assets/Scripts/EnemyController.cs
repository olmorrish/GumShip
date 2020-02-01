using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    float numberOfHolesCreated;
 

    float currentDistance;

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
        typesOfEnemiesInSlots[0] = 0;
        typesOfEnemiesInSlots[1] = 0;
        typesOfEnemiesInSlots[2] = 0;
        numberOfHolesCreated = 0;

    }


    //need to create encounter based off score/distance travelled for difficulty.
    //for now, always spawn 3 narwhals in slot 0
    /// <summary>
    /// Generates the enemies for the encounter, for now 1 narwhal in each slot
    /// </summary>
    void createEncounter() {
        typesOfEnemiesInSlots[0] = 1;
        typesOfEnemiesInSlots[1] = 1;
        typesOfEnemiesInSlots[2] = 1;
    }

    /// <summary>
    /// Should be called by gameController once per update. This processes the enemy attacks
    /// </summary>
    public void updateAttack() {
        //go through enemies in slots, get total enemy amt
        int totalEnemies = 0;

        for(int i = 0; i < 3; i++)
        {
            if (typesOfEnemiesInSlots[i] > 0)
                totalEnemies++;
        }

        //random chance that each enemy hits, assume it is all simple narwhal for now
        for(int i = 0; i < totalEnemies; i++)
        {
            int hitChance = Random.Range(1, 180);

            if (hitChance < 30)
                numberOfHolesCreated++;
        }
    }

}
