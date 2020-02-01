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
        this.startEncounter();
    }  

    /// <summary>
    /// Called in constructor, sets up the various variables for tracking
    /// </summary>
    void setup(float distance)
    {
        this.currentDistance = distance;
        typesOfEnemiesInSlots = (0, 0, 0);
        numberOfEnemiesByType = (0, 0, 0);
        numberOfHolesCreated = 0;

    }


    //need to create encounter based off score/distance travelled for difficulty.
    //for now, always spawn 3 narwhals in slot 0
    /// <summary>
    /// Generates the enemies for the encounter, for now 1 narwhal in each slot
    /// </summary>
    void createEncounter() {
        typesOfEnemiesInSlots = (1, 1, 1);
        numberOfEnemiesByType = (1, 1, 1);
    }

    /// <summary>
    /// Should be called by gameController once per update. This processes the enemy attacks
    /// </summary>
    public void updateAttack() {
        //for now we simply put 1 hole in slot 1
        numOfCreatedHolesPerSlot = (1, 0, 0);

    }

}
