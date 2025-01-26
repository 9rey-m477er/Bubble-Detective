using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.FilePathAttribute;

public class LocationManager : MonoBehaviour
{
    public int currentLocation = 0;
    public int currentDialogue = 0;
    public Image narrowBackground;
    public Image wideBackground;
    public DialogueController dialogueController;
    public LocationObject[] locations;
    public bool dialoguesFinished = false;
    public bool isDialogueOpen = false;

    public LocationObject l;
    public DialogueText t;

    public Queue<LocationObject> locationObjects = new Queue<LocationObject>();
    public Queue<DialogueText> dialogues = new Queue<DialogueText>();
    void Start()
    {
        for (int i=0; i<locations.Length; i++)
        {
            locationObjects.Enqueue(locations[i]);
        }
        advanceLocation();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isDialogueOpen)
            {
                advanceDialogue(t);
            }
        }
    }

    private void advanceLocation()
    {
        l = locationObjects.Dequeue();
        if (l.isInvest)
        {
            wideBackground.gameObject.SetActive(true);
            narrowBackground.gameObject.SetActive(false);
            wideBackground.sprite = l.background;
        }
        else
        {
            narrowBackground.gameObject.SetActive(true);
            wideBackground.gameObject.SetActive(false);
            narrowBackground.sprite = l.background;
        }
        loadNextDialogue(l);
    }

    private void loadDialogues(LocationObject location)
    {
        for (int i = 0; i < location.dialogues.Length; i++)
        {
            dialogues.Enqueue(location.dialogues[i]);
        }        
    }

    private void loadNextDialogue(LocationObject location)
    {
        if (dialogues.Count == 0)
        {
            if (!dialoguesFinished)
            {
                loadDialogues(location);
            }
        }

        t = dialogues.Dequeue();
        advanceDialogue(t);

        if (dialogues.Count == 0)
        {
            dialoguesFinished = true;
        }


    }

    private void advanceDialogue(DialogueText text)
    {
        if (!isDialogueOpen)
        {
            isDialogueOpen = true;
        }
        if (dialogueController.conversationEnded)
        {
            loadNextDialogue(l);
        }
        else
        {
            dialogueController.displayNextParagraph(text);
        }
    }
}
