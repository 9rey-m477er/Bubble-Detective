using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;
//using static UnityEditor.FilePathAttribute;

public class LocationManager : MonoBehaviour
{
    public int currentLocation = 0;
    public int currentDialogue = 0;
    public Image narrowBackground;
    public Image wideBackground;
    public DialogueController dialogueController;
    public SoundManager soundManager;
    public LocationObject[] locations;
    public ClickHandler clickHandler;
    public EndingButtons endingButtons;
    public bool selectingCulprit = false;
    private bool dialoguesFinished = false;
    private bool isDialogueOpen = false;
    private bool hasEpilogueStarted = false;

    public Button executeDawn;
    public Button executeBubba;
    public Button executeBoil;

    public AudioSource DetectiveTheme1;
    public AudioSource Interview2;
    public AudioSource Interrogation3;


    public GameObject questionSpace, testQ, fishQ, rivalQ, chefQ, spouse1Q, spouse2Q, chefFQ, fishFQ, houseEv, chefEv, epilogue;

    //HouseInvest
    public bool missKnife, body, beer, hook, jacket;

    //ChefInvest
    public bool foundKnife, dart, picture;

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
    private bool spouse2Q1, spouse2Q2;

    //chefFinalInter
    private bool chefFQ1, chefFQ2, chefFQ3, chefFQ4;

    //fishFinalInter
    private bool fishFQ1, fishFQ2, fishFQ3, fishFQ4;

    public GameObject menuCanvas;
    public GameObject gameCanvas;
    public Button ContinueButton;
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
            if (!l.isInvest && !l.isQuest && !selectingCulprit)
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

    public void advanceLocation()
    {
        if (locationObjects.Count == 0)
        {
            endGame();
            return;
        }

        l = locationObjects.Dequeue();

        if (l.isMusicChanging == true)
        {
            updateMusic();
        }
        if (l.isInvest)
        {
            Debug.Log("is invest");
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

    public void loadNextDialogue(LocationObject location)
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

    public void advanceDialogue(DialogueText text)
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

    public void addEndgameToQueue(LocationObject location)
    {
        if (locationObjects.Count == 0)
        {
            locationObjects.Enqueue(location);
            Debug.Log(locationObjects.ToString());
        }
    }

    private void endGame()
    {
        dialogueController.EndConversation();
        Debug.Log("Game Ended");
        if (!hasEpilogueStarted)
        {
            dialogues.Clear();
            locationObjects.Clear();
            Debug.Log("Epilogue Started");
            hasEpilogueStarted = true;
            selectingCulprit = true;
            epilogue.SetActive(true);
        }
        else if (hasEpilogueStarted && locationObjects.Count == 0)
        {
            Debug.Log("Resetting");
            SceneManager.LoadScene("Build");
        }
    }

    private void loadQuestionRoom(LocationObject location)
    {
        dialogueController.EndConversation();
        clickHandler.location = location;
        questionSpace.SetActive(true);
        switch (location.name)
        {
            case "TestInter1":
                testQ.SetActive(true);
                break;
            case "008 int room fisher":
                Debug.Log("008");
                fishQ.SetActive(true);
                break;
            case "010 detectives office rival chef":
                rivalQ.SetActive(true);
                fishQ.SetActive(false);
                break;
            case "015 int room chef":
                chefQ.SetActive(true);
                rivalQ.SetActive(false);
                break;
            case "017 - detective office spouse":
                Debug.Log("works");
                spouse1Q.SetActive(true);
                chefQ.SetActive(false);
                break;
            case "019 – int room spouse":
                Debug.Log("works");
                spouse2Q.SetActive(true);
                spouse1Q.SetActive(false);
                break;
            case "023 – int room chef":
                chefFQ.SetActive(true);
                spouse2Q.SetActive(false);
                break;
            case "024 – int room fisherman":
                fishFQ.SetActive(true);
                chefFQ.SetActive(false);
                break;
            default:
                Debug.Log(location.name);
                break;
        }
    }

    public void checkQuestionClick()
    {
        Debug.Log("Clicked Question");
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        string buttonName = clickedButton.name;
        Debug.Log(buttonName);

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
            case "fisherq1":
                fishQ1 = true;
                break;
            case "fisherq2":
                fishQ2 = true;
                break;
            case "fisherq3":
                fishQ3 = true;
                break;
            case "fisherq4":
                fishQ4 = true;
                break;
            case "fisherq5":
                fishQ5 = true;
                break;
            case "rivalq1":
                rivalQ1 = true;
                break;
            case "rivalq2":
                rivalQ2 = true;
                break;
            case "rivalq3":
                rivalQ3 = true;
                break;
            case "rivalq4":
                rivalQ4 = true;
                break;
            case "chefq1":
                chefQ1 = true;
                break;
            case "chefq2":
                chefQ2 = true;
                break;
            case "chefq3":
                chefQ3 = true;
                break;
            case "chefq4":
                chefQ4 = true;
                break;
            case "dawnq1":
                spouse1Q1 = true;
                break;
            case "dawnq2":
                spouse1Q2 = true;
                break;
            case "dawnq3":
                spouse1Q3 = true;
                break;
            case "dawnq4":
                spouse2Q1 = true;
                break;
            case "dawnq5":
                spouse2Q2 = true;
                break;
            case "fchef1":
                chefFQ1 = true;
                break;
            case "ffish1":
                fishFQ1 = true;
                break;
        }
        switch (l.name)
        {
            case "TestInter1":
                if (testQ1 && testQ2 && testQ3)
                {
                    ContinueButton.gameObject.SetActive(true);
                    return;
                }
                break;

            case "008 int room fisher":
                if(fishQ1 && fishQ2 && fishQ3 && fishQ4 && fishQ5)
                {
                    ContinueButton.gameObject.SetActive(true);
                }
                break;
            case "010 detectives office rival chef":
                if (rivalQ1 && rivalQ2 && rivalQ3 && rivalQ4)
                {
                    ContinueButton.gameObject.SetActive(true);
                }
                break;
            case "015 int room chef":
                if (chefQ1 && chefQ2 && chefQ3 && chefQ4)
                {
                    ContinueButton.gameObject.SetActive(true);
                }
                break;
            case "017 – detective office spouse":
                if (spouse1Q1 && spouse1Q2 && spouse1Q3)
                {
                    ContinueButton.gameObject.SetActive(true);
                }
                break;
            case "019 – int room spouse":
                if (spouse2Q1 && spouse2Q2)
                {
                    ContinueButton.gameObject.SetActive(true);
                }
                break;
            case "023 – int room chef":
                if (chefFQ1)
                {
                    ContinueButton.gameObject.SetActive(true);
                }
                break;
            case "024 – int room fisherman":
                if (fishFQ1)
                {
                    ContinueButton.gameObject.SetActive(true);
                }
                break;
            default:
                Debug.Log("l.name" + l.name);
                break;
        }
    }


    private void loadInvestigateRoom(LocationObject location)
    {
        dialogueController.EndConversation();
        clickHandler.location = location;
        if (location.name == "005 – vic house explore")
        {
            houseEv.SetActive(true);
            chefEv.SetActive(false);

        }
        if (location.name == "013 – chef shop explore")
        {
            chefEv.SetActive(true);
            houseEv.SetActive(false);
        }

    }

    public void EvidenceHandler() //buttons call this
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        string buttonName = clickedButton.name;
        string testName = l.name;
        switch (buttonName)
        {
            case "MissingKnifeButton":
                missKnife = true;
                break;
            case "BodyButton":
                body = true;
                break;
            case "HookButton":
                hook = true;
                break;
            case "BeerButton":
                beer = true;
                break;
            case "JacketButton":
                jacket = true;
                break;
            //------
            case "FoundKnifeButton":
                foundKnife = true;
                break;
            case "DartButton":
                dart = true;
                break;
            case "PhotoButton":
                picture = true;
                break;
        }
        Debug.Log(testName);
        if (l.name == "005 – vic house explore")
        {
            if (missKnife == true && body == true && beer == true && hook == true && jacket == true)
            {
                Debug.Log("house done");
                ContinueButton.gameObject.SetActive(true);
            }
        }
        if (l.name == "013 – chef shop explore")
        {
            if (foundKnife == true && dart == true && picture == true)
            {
                Debug.Log("Chef done");
                ContinueButton.gameObject.SetActive(true);
            }
        }
    }

    public void Continue()
    {
        advanceLocation();
        houseEv.SetActive(false);
        chefEv.SetActive(false);
        questionSpace.SetActive(false);
        ContinueButton.gameObject.SetActive(false);
    }

    public void updateMusic()
    {
        if(l.currentTrack == 1)
        {
            fadeMusicOut(Interrogation3);
            fadeMusicOut(Interview2);
            fadeMusicIn(DetectiveTheme1);
        }
        if(l.currentTrack == 2)
        {
            fadeMusicOut(Interrogation3);
            fadeMusicOut(DetectiveTheme1);
            fadeMusicIn(Interview2);
        }
        if(l.currentTrack == 3)
        {
            fadeMusicOut(DetectiveTheme1);
            fadeMusicOut(Interview2);
            fadeMusicIn(Interrogation3);
        }
        if(l.currentTrack == 4)
        {
            fadeMusicOut(DetectiveTheme1);
            fadeMusicOut(Interview2);
            fadeMusicOut(Interrogation3);
        }
    }


    public void fadeMusicOut(AudioSource audioSource)
    {
        StartCoroutine(soundManager.FadeOutMusic(audioSource));
    }

    public void fadeMusicIn(AudioSource audioSource)
    {
        StartCoroutine(soundManager.FadeInMusic(audioSource));
    }

}
