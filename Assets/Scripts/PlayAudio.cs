using System;
using UnityEngine;
using UnityEngine.Audio;

public class PlayAudio : MonoBehaviour
{
	[SerializeField]
	private AudioClip[] _audioClip;

	[SerializeField]
	private AudioMixerGroup _audioMixer;

	public AudioSource _audioSource;

	[SerializeField]
	private bool _isLoop;

	[SerializeField]
	private bool _playOnStart;

	private System.Random rnd;
	private bool isStopped;

	private void Awake()
	{
		rnd = new System.Random();
		_audioSource = GetComponent<AudioSource>();
		_audioSource.outputAudioMixerGroup = _audioMixer;
		ApplyLoop();
		if (_playOnStart)
		{
			Play();
		}
	}

	public void Play()
	{
		if(_audioClip is null || _audioClip.Length == 0 || isStopped) return;
		
		if (_audioClip.Length > 1)
		{
			ChooseAVariant();
		}
		else
		{
			_audioSource.clip = _audioClip[0];
		}
		_audioSource.Play();
	}

	public void Stop()
	{
		isStopped = true;
		_audioSource.Stop();
	}

	private void ChooseAVariant()
	{
		_audioSource.clip = _audioClip[rnd.Next(0, _audioClip.Length)];
	}

	private void ApplyLoop()
	{
		_audioSource.loop = _isLoop;
	}
}
