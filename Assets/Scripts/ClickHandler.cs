using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private LocationObject location;
    public bool isDialogueOpen = false;
    private DialogueText currentText = null;

    public void OnDialogueClick(DialogueText text)
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isDialogueOpen)
            {
                OnDialogueClick(currentText);
            }
        }
    }
}
