using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    public int currentLocation = 0;
    public int currentDialogue = 0;
    public LocationObject LocationObject;
    public DialogueController DialogueController;
    public List<LocationObject> locationObjects;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DialogueController.conversationEnded == true) //go to next dialogue
        {
            currentDialogue++;
            DialogueController.displayNextParagraph(locationObjects[currentLocation].dialogues[currentDialogue]);
        }
        
    }
}
