using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
public class Script : MonoBehaviour
{
	// Token: 0x0600004F RID: 79 RVA: 0x00003705 File Offset: 0x00001B05
	private void Start()
	{
		
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00003707 File Offset: 0x00001B07
	private void Update()
	{
		if (!this.audioDevice.isPlaying & this.played)
		{
			Application.Quit();
		}
	}

	// Token: 0x06000051 RID: 81 RVA: 0x0000372B File Offset: 0x00001B2B
	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Player" & !this.played)
		{
			this.audioDevice.Play();
			this.played = true;
		}
	}

	// Token: 0x0400006B RID: 107
	public AudioSource audioDevice;

	// Token: 0x0400006C RID: 108
	private bool played;
}
