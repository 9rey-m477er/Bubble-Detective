using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private bool dialoguesFinished = false;
    private bool isDialogueOpen = false;

    //HouseInvest
    private bool missKnife, body, beer, hook, jacket;

    //ChefInvest
    private bool foundKnife, dart, picture;

    //FishInter
    private bool fishQ1, fishQ2, fishQ3, fishQ4, fishQ5;

    //RivalInter
    private bool rivalQ1, rivalQ2, rivalQ3, rivalQ4;

    //ChefInter
    private bool chefQ1, chefQ2, chefQ3, chefQ4;

    //SpouseInter1
    private bool spouse1Q1, spouse1Q2, spouse1Q3;

    //SpouseInter2
    private bool spouse2Q1, spouse2Q2, spouse2Q3, spouse2Q4, spouse2Q5;

    //chefFinalInter
    private bool chefFQ1, chefFQ2, chefFQ3, chefFQ4;

    //fishFinalInter
    private bool fishFQ1, fishFQ2, fishFQ3, fishFQ4;

    public GameObject menuCanvas;
    public GameObject gameCanvas;

    public LocationObject l;
    public DialogueText t;

    public Queue<LocationObject> locationObjects = new Queue<LocationObject>();
    public Queue<DialogueText> dialogues = new Queue<DialogueText>();

    private void Start()
    {
        gameStart();
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

    public void gameStart()
    {
        for (int i = 0; i < locations.Length; i++)
        {
            locationObjects.Enqueue(locations[i]);
        }
        advanceLocation();
    }

    private void advanceLocation()
    {

        if (locationObjects.Count == 0)
        {
            endGame();
            return;
        }

        l = locationObjects.Dequeue();
        if (l.isInvest)
        {
            wideBackground.gameObject.SetActive(true);
            narrowBackground.gameObject.SetActive(false);
            wideBackground.sprite = l.background;
            loadInvestigateRoom();
        }
        else if (l.isQuest)
        {
            narrowBackground.gameObject.SetActive(true);
            wideBackground.gameObject.SetActive(false);
            narrowBackground.sprite = l.background;
            loadQuestionRoom();
        }
        else
        {
            narrowBackground.gameObject.SetActive(true);
            wideBackground.gameObject.SetActive(false);
            narrowBackground.sprite = l.background;
            loadNextDialogue(l);
        }
    }

    private void loadDialogues(LocationObject location)
    {
        for (int i = 0; i < location.dialogues.Length; i++)
        {
            dialogues.Enqueue(location.dialogues[i]);
            Debug.Log("Enqueued " +  location.dialogues[i].name + "for Location " + location.name);
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
            else
            {
                dialoguesFinished = false;
                advanceLocation();
                return;
            }
        }
        if (dialogues.Count > 0)
        {
            t = dialogues.Dequeue();
            Debug.Log("Dequeued " + t.name);
            advanceDialogue(t);
        }

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
            dialogueController.conversationEnded = false;
            loadNextDialogue(l);
        }
        else
        {
            dialogueController.displayNextParagraph(text);
        }
    }

    private void endGame()
    {
        SceneManager.LoadScene("Build");
    }

    private void loadQuestionRoom()
    {

    }

    private void loadInvestigateRoom()
    {

    }
}
