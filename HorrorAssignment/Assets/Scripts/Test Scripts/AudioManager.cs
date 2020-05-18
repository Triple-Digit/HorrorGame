using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    //--- VARIABLES
    [Header("Sounds to be Played")]
    public AudioSource sound01;
    public AudioSource sound02;
    private AudioSource currentSound;
    public float fadeTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FadeIn();
		FadeOutOtherSounds();
    }

    void Triggered(int triggerID)
    {
        if (triggerID == 0)
        {
            PlaySilence();
        }
        else if (triggerID == 1)
        {
            PlaySound01();
        }
        else if (triggerID == 2)
        {
            PlaySound02();
        }
    }

	//--- SOUND FUNCTIONS

	void PlaySilence()
	{
		currentSound = null;
	}

	void PlaySound01()
	{
		StartFadeIn(sound01);
	}

	void PlaySound02()
	{
		StartFadeIn(sound02);
	}

	//--- FADE FUNCTIONS

	void StartFadeIn(AudioSource newSound)
	{
		if (currentSound != newSound)
		{
			currentSound = newSound;

			//Play sound
			currentSound.Play();
			currentSound.volume = 0;
		}
	}
	void FadeIn()
	{
		if (currentSound && currentSound.volume != 1)
		{
			currentSound.volume = Mathf.MoveTowards(currentSound.volume, 1, 1 / fadeTime * Time.deltaTime);
		}
	}
	void FadeOutOtherSounds()
	{
		if (sound01 != currentSound)
		{
			if (sound01.volume != 0)
			{
				sound01.volume = Mathf.MoveTowards(sound01.volume, 0, 1 / fadeTime * Time.deltaTime);
			}
			else if (sound01.isPlaying)
			{
				sound01.Stop();
			}
		}

		if (sound02 != currentSound)
		{
			if (sound02.volume != 0)
			{
				sound02.volume = Mathf.MoveTowards(sound02.volume, 0, 1 / fadeTime * Time.deltaTime);
			}
			else if (sound02.isPlaying)
			{
				sound02.Stop();
			}
		}
	}
}
