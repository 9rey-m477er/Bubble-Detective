using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameplayScript : MonoBehaviour
{
    public Image dialogueBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonTest()
    {
        dialogueBox.gameObject.SetActive(true);
    }
}
