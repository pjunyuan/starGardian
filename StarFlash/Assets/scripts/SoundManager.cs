using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance;

	public float lowPitchRange = .95f; 
    public float highPitchRange = 1.05f;
    
	 AudioSource bgm;
	AudioSource bgm2;
	 AudioSource needToPlay;
	 AudioSource LevelUp;
    [Range(0.0f, 1f)] public float BGMVolume = 0.5f;
	[Range(0.0f, 1f)] public float BGM2Volume = 0.5f;
	[Range(0.0f, 1f)] public float SoundFXVolume = 0.5f;
	public AudioResources SoundsLibrary;

	[System.Serializable]
	public class AudioResources
	{
		public AudioClip[] bgms;
		public AudioClip[] bgms2;
		public AudioClip[] ButtonSounds;
		public AudioClip[] CoinSounds;
		public AudioClip WrongClickSound;
	}

    void Update()
    {
        if (bgm != null)bgm.volume =  BGMVolume;
		if (bgm2 != null) bgm2.volume = BGM2Volume;
		if (needToPlay != null) needToPlay.volume = SoundFXVolume;
    }

    void Awake()
    {
		if (instance == null)
		{
			instance = this;
		}

        if (SceneManager.GetActiveScene().buildIndex == 0 || bgm == null)
        {
            InitializeBGM();
        }
        InitializeAudio();
    }

    public void InitializeBGM()
    {
		if (SoundsLibrary.bgms.Length != 0)
		{
			bgm = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
			bgm.playOnAwake = true;
			bgm.loop = true;
			bgm.clip = SoundsLibrary.bgms[0];
			bgm.Play();
		}

		if (SoundsLibrary.bgms.Length != 0)
		{
			bgm2 = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
			bgm2.playOnAwake = true;
			bgm2.loop = true;
			bgm2.clip = SoundsLibrary.bgms2[0];
			bgm2.Play();
		}

	}

    public void InitializeAudio()
    {
        needToPlay = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        needToPlay.playOnAwake = false;
        needToPlay.loop = false;
        LevelUp = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        LevelUp.playOnAwake = false;
        LevelUp.loop = false;
    }

	public void MuteBGM(bool option=true)
	{
		BGMVolume = option ? 0 : .5f;
	}

	public void PauseBGM()
	{
		bgm.Pause();
	}

	public void ContinueBGM()
	{
		bgm.UnPause();
	}

    public void PlaySingle(AudioClip clip)
    {
        try { needToPlay.PlayOneShot(clip); }
        catch (System.Exception) { }
    }

    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        needToPlay.pitch = randomPitch;
        needToPlay.PlayOneShot(clips[randomIndex]);
    }

    public void PlayButtonSound(int x = 0)
    {
        if (x <= SoundsLibrary.ButtonSounds.Length - 1)
        {
            PlaySingle(SoundsLibrary.ButtonSounds[x]);
        }
    }

	public void PlayCoinSound(int x = 0)
	{
		if (x <= SoundsLibrary.CoinSounds.Length - 1)
		{
			PlaySingle(SoundsLibrary.CoinSounds[x]);
		}
	}

	public void PlayWrongClickSound()
	{
		PlaySingle(SoundsLibrary.WrongClickSound);
	}

	#region Fade BGM
	Coroutine FadeCorotine;
	Coroutine SwitchCorotine;

	void FadeBGM(int direction, float duration)
	{
		if (FadeCorotine != null)
		{
			StopCoroutine(FadeCorotine); // stop current fade progress
		}
		FadeCorotine = StartCoroutine(FadeEngine(direction, duration));
	}

	IEnumerator FadeEngine(int direction, float duration)
	{
		direction = direction > 0 ? 1 : -1;
		float dest = 0f;
		bool m_on = PlayerPrefs.GetInt("music_on") == 1;
		if (m_on )
		{
			if (direction == 1)
			{
				dest = 0.5f;
			}
			else
			{
				dest = 0f;
			}
		}

		float step = 0.005f;
		int loopTimes = (int) (Mathf.Abs(dest - BGMVolume) / step);
		float interval = duration / loopTimes;
		for (int i = 0; i <loopTimes; i++)
		{
			BGMVolume += direction * step;
			yield return new WaitForSecondsRealtime(interval);
		}
		yield return null;
	}

	IEnumerator SwitchBGMEngine(float duration, int BGM_Index)
	{
		duration = duration / 2;
		FadeBGM(-1, duration);
		yield return new WaitForSecondsRealtime(duration + 0.1f);

		bgm.Stop();
		bgm.clip = SoundsLibrary.bgms[BGM_Index];
		bgm.Play();

		FadeBGM(1, duration);
	}

	public void SwitchBGM(float duration, int BGM_Index)
	{
		if (SwitchCorotine != null)
		{
			StopCoroutine(SwitchCorotine); // stop current fade progress
		}
		SwitchCorotine = StartCoroutine(SwitchBGMEngine(duration, BGM_Index));
	}
	#endregion
}