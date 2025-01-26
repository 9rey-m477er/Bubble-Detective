using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "BubbleDetective/Locations/New Location")]
public class LocationObject : ScriptableObject
{
    public DialogueText[] dialogues;
    public Sprite background;
    public LocationObject nextLocation;
    public bool isInvest;
    public bool isQuest;
    public bool isMusicChanging;
    public AudioClip newMusic = null;
    public int currentTrack;
}
