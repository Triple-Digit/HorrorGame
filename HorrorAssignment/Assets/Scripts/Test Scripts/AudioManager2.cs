using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager2 : MonoBehaviour
{
	//--- SOUND VARIABLES
	[Header("SOUND TO BE PLAYED")]
	public AudioSource sound01;
	public AudioSource sound02;
	public AudioSource sound03;
	private AudioSource currentSound;
	public float desiredSoundVolume;
	private float fadeTime = 3.0f; //time in seconds for a sound to fade in and out out, and to crossfade


	//--- BASIC FUNCTIONS
	void Update()
	{
		//Debug
		if (currentSound) { Debug.Log("current sound volume is " + currentSound.volume); }
		else if (currentSound = null) { Debug.Log("current sound volume is 0"); }

		FadeIn();
		FadeOut();
	}

	//--- TRIGGER FUNCTIONS
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
		else if (triggerID == 3)
		{
			PlaySound03();
		}
		//add another condition when adding a new sound
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

	void PlaySound03()
	{
		StartFadeIn(sound03);
	}
	//add another PlaySound0X() when adding a new sound

	void StartFadeIn(AudioSource newSound)
	{
		if (currentSound != newSound)
		{
			//Make the new sound the current sound
			currentSound = newSound;

			//Play the sound
			currentSound.Play();
			currentSound.volume = 0; //set initial value to zero to start the fade in
		}
	}

	void FadeIn()
	{
		//Fade the new sound in
		if (currentSound && currentSound.volume != 1)
		{
			currentSound.volume = Mathf.MoveTowards(currentSound.volume, desiredSoundVolume, 1 / fadeTime * Time.deltaTime);
		}
	}

	void FadeOut()
	{
		//Fade out all sound that are not the current one
		if (sound01 != currentSound)
		{
			if (sound01.volume != 0) { sound01.volume = Mathf.MoveTowards(sound01.volume, 0, 1 / fadeTime * Time.deltaTime); }
			else if (sound01.isPlaying) { sound01.Stop(); }
		}

		if (sound02 != currentSound)
		{
			if (sound02.volume != 0) { sound02.volume = Mathf.MoveTowards(sound02.volume, 0, 1 / fadeTime * Time.deltaTime); }
			else if (sound02.isPlaying) { sound02.Stop(); }
		}

		if (sound03 != currentSound)
		{
			if (sound03.volume != 0) { sound03.volume = Mathf.MoveTowards(sound03.volume, 0, 1 / fadeTime * Time.deltaTime); }
			else if (sound03.isPlaying) { sound03.Stop(); }
		}

		//add another condition when adding a new sound
	}
}
