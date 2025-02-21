using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource AS, Music1, Music2, Music3;
    public AudioClip Pop1, Pop2, Pop3, Pop4, Pop5, Pop6, Phone, Thud1, Thud2;

    public float maxVol;

    public void PlaySoundClip(int clip)
    {
        switch (clip)
        {
            case 0:
                AS.PlayOneShot(Pop1);
                break;
            case 1:
                AS.PlayOneShot(Pop2);
                break;
            case 2:
                AS.PlayOneShot(Pop3);
                break;
            case 3:
                AS.PlayOneShot(Pop4);
                break;
            case 4:
                AS.PlayOneShot(Pop5);
                break;
            case 5:
                AS.PlayOneShot(Pop6);
                break;
            case 6:
                AS.PlayOneShot(Phone);
                break;
            case 7:
                AS.PlayOneShot(Thud1);
                break;
            case 8:
                AS.PlayOneShot(Thud2);
                break;
        }
    }


    public IEnumerator FadeOutMusic(AudioSource musicSource, System.Action onFadeComplete = null, float duration = 1f)
    {
        float startVolume = musicSource.volume;

        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        musicSource.Stop();
        //onFadeComplete?.Invoke(); // Trigger callback after fading out
    }

    public IEnumerator FadeInMusic(AudioSource musicSource, float duration = 2f)
    {
        musicSource.volume = 0f;
        musicSource.Play();

        while (musicSource.volume < maxVol)
        {
            musicSource.volume += maxVol * Time.deltaTime / duration;
            yield return null;
        }

        musicSource.volume = maxVol; // Ensure volume is at max after fading in
    }
}
