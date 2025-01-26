using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private LocationObject location;
    public bool isDialogueOpen = false;
    public bool isQuestionOpen = false;
    public GameObject questionSpace;
    private DialogueText currentText = null;

    public void OnDialogueClickInvest(DialogueText text) //invest
    {
        if (!isDialogueOpen)
        {
            isDialogueOpen = true;
            currentText = text;
        }
        if (dialogueController.conversationEnded)
        {
            isDialogueOpen = false;
            dialogueController.EndConversation();
        }
        else
        {
            dialogueController.displayNextParagraph(text);
        }
    }
    //-----------------------------------------------------------
    public void OnDialogueClickQuestion(DialogueText text) //questioning
    {
        Debug.Log("Location is " + location.name);
        if (!isQuestionOpen)
        {
            questionSpace.SetActive(false);
            isQuestionOpen = true;
            currentText = text;
        }
        if (dialogueController.conversationEnded)
        {
            isQuestionOpen = false;
            questionSpace.SetActive(true);
            dialogueController.EndConversation();
        }
        else
        {
            dialogueController.displayNextParagraph(text);
        }
    }
    //------------------------------------------------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {          
            if (location.isInvest == true)
            {
                if (isDialogueOpen)
                {
                    Debug.Log("isInvest true");
                    OnDialogueClickInvest(currentText);
                }
            }
            else if(location.isQuest == true)
            {
                if (isQuestionOpen)
                {
                    Debug.Log("isquest true");
                    OnDialogueClickQuestion(currentText);
                }
            }
        }
    }
}
