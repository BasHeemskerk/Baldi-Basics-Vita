using System;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class AudioQueueScript : MonoBehaviour
{
	// Token: 0x06000053 RID: 83 RVA: 0x00003773 File Offset: 0x00001B73
	private void Start()
	{
		this.audioDevice = base.GetComponent<AudioSource>();
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00003781 File Offset: 0x00001B81
	private void Update()
	{
		if (!this.audioDevice.isPlaying && this.audioInQueue > 0)
		{
			this.PlayQueue();
		}
	}

	// Token: 0x06000055 RID: 85 RVA: 0x000037A5 File Offset: 0x00001BA5
	public void QueueAudio(AudioClip sound)
	{
		this.audioQueue[this.audioInQueue] = sound;
		this.audioInQueue++;
	}

	// Token: 0x06000056 RID: 86 RVA: 0x000037C3 File Offset: 0x00001BC3
	private void PlayQueue()
	{
		this.audioDevice.PlayOneShot(this.audioQueue[0]);
		this.UnqueueAudio();
	}

	// Token: 0x06000057 RID: 87 RVA: 0x000037E0 File Offset: 0x00001BE0
	private void UnqueueAudio()
	{
		for (int i = 1; i < this.audioInQueue; i++)
		{
			this.audioQueue[i - 1] = this.audioQueue[i];
		}
		this.audioInQueue--;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00003824 File Offset: 0x00001C24
	public void ClearAudioQueue()
	{
		this.audioInQueue = 0;
	}

	// Token: 0x0400006D RID: 109
	private AudioSource audioDevice;

	// Token: 0x0400006E RID: 110
	private int audioInQueue;

	// Token: 0x0400006F RID: 111
	private AudioClip[] audioQueue = new AudioClip[100];
}
