using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemycontroller : MonoBehaviour {
    //to do
    /*
     * clear up methods needed by roxannes class for this class to be useful, right now its a little ad hoc
     * (attack, endattack, updateenemies, createencounter, updateattack, processdamagereceived)
     * 
     * implement enemy generation
     * 
     * also use notes and music to clear your head
     * 
     * switch this to object type, so it has a start function
     * this class gets created when the encounter starts and deleted when the encounter ends
     */



    //used to access the associated arrays
    static const int narwhal = 0, hammerhead = 1, whale = 2;

    //number of holes created by enemy attacks, should get passed to gamecontroller
    int numofcreatedholes;

    float currentdistance;

    //0 = no enemies
    //1 = narwhal
    //2 = hammerheads
    //3 = whale
    public int[] typesofenemies = { 0, 0, 0 };

    //index 0 = narwhals
    //index 1 = hammerheads
    //index 2 = whales
    public int[] numberofenemiesbytype = { 0, 0, 0 };

    //creates the class, this will start the encounter as soon as its constructed
    public enemycontroller(float distance) {
        this.currentdistance = distance;
        this.createencounter();
        this.startencounter();
    }

    //give certain score amounts for enemy types

    //begins encounter
    void startencounter() {

    }

    //called at end of encounter to clean up class
    void endattack() {

    }

    // update is called once per frame
    void updateenemies(float distance) {
        currentdistance = distance;
        if (encounteractive) //process attacks
        {

        }
    }

    //need to create encounter based off score/distance travelled for difficulty.
    //for now, always spawn 3 narwhals in slot 0
    public void createencounter() {
        encounteractive = true;
        typesofenemies[narwhal] = 1;
        numberofenemiesbytype[narwhal] = 3;
    }

    public void updateattack() { }

    public void processdamagereceived() {

    }
}
