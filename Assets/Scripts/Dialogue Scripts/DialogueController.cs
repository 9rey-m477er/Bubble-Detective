using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI npcNameText;
    [SerializeField] private TextMeshProUGUI npcDialogueText;
    [SerializeField] private Image npcPortrait;

    private SoundManager soundManager;

    public Queue<string> paragraphs = new Queue<string>();
    public Queue<string> names = new Queue<string>();
    public Queue<Sprite> sprites = new Queue<Sprite>();

    public bool conversationEnded;
    private string n;
    private string p;
    private Sprite i;

    public void Awake()
    {
        //soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    public void displayNextParagraph(DialogueText dialogueText)
    {

        if (paragraphs.Count == 0)
        {
            if (!conversationEnded)
            {
                StartConversation(dialogueText);
            }
        }


        n = names.Dequeue();
        p = paragraphs.Dequeue();
        i = sprites.Dequeue();
        //soundManager.PlaySoundClip(3);
        npcNameText.text = n;
        //npcDialogueText.text = p; //HERE <-----------------
        npcPortrait.sprite = i;

        StopAllCoroutines(); // Ensure no overlapping coroutines
        StartCoroutine(TypeText(p)); // Start the typewriter effect

        if (paragraphs.Count == 0)
        {
            conversationEnded = true;
        }
    }

    private IEnumerator TypeText(string fullText)
    {
        npcDialogueText.text = ""; // Clear the current text
        foreach (char letter in fullText.ToCharArray())
        {
            npcDialogueText.text += letter; // Add one letter at a time
            yield return new WaitForSeconds(0.035f); // Wait between letters (adjust for speed)
        }
    }

    private void StartConversation(DialogueText dialogueText)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        for (int i = 0; i < dialogueText.paragraphs.Length; i++)
        {
            paragraphs.Enqueue(dialogueText.paragraphs[i]);
        }
        for (int i = 0; i < dialogueText.speakerNames.Length; i++)
        {
            names.Enqueue(dialogueText.speakerNames[i]);
        }
        for (int i = 0; i < dialogueText.speakerImages.Length; i++)
        {
            sprites.Enqueue(dialogueText.speakerImages[i]);
        }
    }

    public void EndConversation()
    {
        Debug.Log("Conversation Ended");
        paragraphs.Clear();
        names.Clear();
        conversationEnded = false;
        if (gameObject.activeSelf)
        {
            //soundManager.PlaySoundClip(4);
            gameObject.SetActive(false);
        }
    }
}
