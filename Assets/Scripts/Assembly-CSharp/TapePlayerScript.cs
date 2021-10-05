using System;
using UnityEngine;

// Token: 0x02000033 RID: 51
public class TapePlayerScript : MonoBehaviour
{
	// Token: 0x060000FE RID: 254 RVA: 0x000084CF File Offset: 0x000068CF
	private void Start()
	{
		this.audioDevice = base.GetComponent<AudioSource>();
	}

	// Token: 0x060000FF RID: 255 RVA: 0x000084E0 File Offset: 0x000068E0
	private void Update()
	{
		if (this.audioDevice.isPlaying & Time.timeScale == 0f)
		{
			this.audioDevice.Pause(); //Pause the audio device
		}
		else if (Time.timeScale > 0f & this.baldi.antiHearingTime > 0f)
		{
			this.audioDevice.UnPause(); //Unpause the audio device
        }
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00008549 File Offset: 0x00006949
	public void Play()
	{
		this.sprite.sprite = this.closedSprite;
		this.audioDevice.Play();
		this.baldi.ActivateAntiHearing(30f);
	}

	// Token: 0x040001B3 RID: 435
	public Sprite closedSprite;

	// Token: 0x040001B4 RID: 436
	public SpriteRenderer sprite;

	// Token: 0x040001B5 RID: 437
	public BaldiScript baldi;

	// Token: 0x040001B6 RID: 438
	private AudioSource audioDevice;
}
