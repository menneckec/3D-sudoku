using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessHandler : MonoBehaviour
{
    //private bool isCorrect;

    public void NewGuessMenu(Vector3 location)
    {
        //Enable canvas + children on NewGuessMenu GameObjects
        //int selectedItem = objectType
        //Make a copy of the inactive NumberObject at that location
        //Change the copy's objectType to one selected
        //Set active
        //Add to Guesses layer

    }

    public void ChangeGuessMenu(Vector3 location, int type)
    {
        //Enable canvas + children on ChangeGuessMenu GameObjects
        //if Change selected:
        //Destroy "guess" copy, call NewGuessMenu
        //if Remove selected:
        //Destroy "guess" copy
    }

    //Function to lock guesses if row/column/grid is complete?


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
