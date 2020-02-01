using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //TO DO
    /*
     * Clear up methods needed by roxannes class for this class to be useful, right now its a little ad hoc
     * (attack, endAttack, updateEnemies, createEncounter, updateAttack, processDamageReceived)
     * 
     * Implement enemy generation
     * 
     * also use notes and music to clear your head
     * 
     * Switch this to object type, so it has a Start function
     * This class gets created when the encounter starts and deleted when the encounter ends
     */



    //used to access the associated arrays
    static const int NARWHAL = 0, HAMMERHEAD = 1, WHALE = 2;

    //number of holes created by enemy attacks, should get passed to gamecontroller
    int numOfCreatedHoles;

    float currentDistance;

    //0 = no enemies
    //1 = narwhal
    //2 = hammerheads
    //3 = whale
    public int[] typesOfEnemies = { 0, 0, 0 };

    //index 0 = narwhals
    //index 1 = hammerheads
    //index 2 = whales
    public int[] numberOfEnemiesByType = { 0, 0, 0 };

    //creates the class, this will start the encounter as soon as its constructed
    public EnemyController(float distance)
    {
        this.currentDistance = distance;
        this.createEncounter();
        this.startEncounter();
    }

    //give certain score amounts for enemy types

    //begins encounter
    void startEncounter()
    {

    }

    //called at end of encounter to clean up class
    void endAttack()
    {

    }

    // Update is called once per frame
    void updateEnemies(float distance)
    {
        currentDistance = distance;
        if(encounterActive) //process attacks
        {

        }
    }

    //need to create encounter based off score/distance travelled for difficulty.
    //for now, always spawn 3 narwhals in slot 0
    public void createEncounter() {
        encounterActive = true;
        typesOfEnemies[NARWHAL] = 1;
        numberOfEnemiesByType[NARWHAL] = 3;
    }

    public void updateAttack() { }

    public void processDamageReceived()
    {

    }
}
