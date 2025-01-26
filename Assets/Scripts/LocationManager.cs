using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
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

    public GameObject questionSpace, testQ, fishQ, rivalQ, chefQ, spouse1Q, spouse2Q, chefFQ, fishFQ;

    //HouseInvest
    private bool missKnife, body, beer, hook, jacket;

    //ChefInvest
    private bool foundKnife, dart, picture;

    //TestInter
    public bool testQ1, testQ2, testQ3;
    
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
            if (!l.isInvest && !l.isQuest)
            {
                if (isDialogueOpen)
                {
                    advanceDialogue(t);
                }
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
            loadInvestigateRoom(l);
        }
        else if (l.isQuest)
        {
            narrowBackground.gameObject.SetActive(true);
            wideBackground.gameObject.SetActive(false);
            narrowBackground.sprite = l.background;
            loadQuestionRoom(l);
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

    private void loadQuestionRoom(LocationObject location)
    {
        dialogueController.EndConversation();
        questionSpace.SetActive(true);
        switch (location.name)
        {
            case "TestInter":
                break;
        }
    }

    public void checkQuestionClick()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        string buttonName = clickedButton.name;

        switch (buttonName)
        {
            case "TopButton":
                testQ1 = true;
                break;
            case "MiddleButton":
                testQ2 = true;
                break;
            case "BottomButton":
                testQ3 = true;
                break;
            default:
                break;
        }
        switch (l.name)
        {
            case "TestInter1":
                if (testQ1 && testQ2 && testQ3)
                {
                    Debug.Log("I'm Done Now");
                    return;
                }
                break;
            default:
                break;
        }
    }

    private void loadInvestigateRoom(LocationObject location)
    {

    }
}
