using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    public int currentLocation = 0;
    public int currentDialogue = 0;
    public DialogueController DialogueController;
    public Queue<LocationObject> locationObjects = new Queue<LocationObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(DialogueController.conversationEnded == true) //go to next dialogue
        //{
        //    currentDialogue++;
        //    DialogueController.displayNextParagraph(locationObjects[currentLocation].dialogues[currentDialogue]);
        //}
        //if(LocationObject.dialogues.Count == currentDialogue)
        //{

        //}
    }

    private void loadLocation(LocationObject location)
    {

        for (int i = 0; i < location.dialogues.Length; i++)
        {

        }
        //Conner put your fade here.
    }
}
