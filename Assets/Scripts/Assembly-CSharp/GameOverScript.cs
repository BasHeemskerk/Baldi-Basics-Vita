using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000020 RID: 32
public class GameOverScript : MonoBehaviour
{
	// Token: 0x06000099 RID: 153 RVA: 0x0000547C File Offset: 0x0000387C
	private void Start()
	{
		this.image = base.GetComponent<Image>();
		this.audioDevice = base.GetComponent<AudioSource>();
		this.delay = 5f;
		this.chance = UnityEngine.Random.Range(1f, 99f);
		if (this.chance < 98f)
		{
			int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 4f));
			this.image.sprite = this.images[num];
		}
		else
		{
			this.image.sprite = this.rare;
		}
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00005510 File Offset: 0x00003910
	private void Update()
	{
		this.delay -= 1f * Time.deltaTime;
		if (this.delay <= 0f)
		{
			if (this.chance < 98f)
			{
				SceneManager.LoadScene("MainMenu");
			}
			else
			{
				this.image.transform.localScale = new Vector3(5f, 5f, 1f);
				this.image.color = Color.red;
				if (!this.audioDevice.isPlaying)
				{
					this.audioDevice.Play();
				}
				if (this.delay <= -5f)
				{
					Application.Quit();
				}
			}
		}
	}

	// Token: 0x040000ED RID: 237
	private Image image;

	// Token: 0x040000EE RID: 238
	private float delay;

	// Token: 0x040000EF RID: 239
	public Sprite[] images = new Sprite[5];

	// Token: 0x040000F0 RID: 240
	public Sprite rare;

	// Token: 0x040000F1 RID: 241
	private float chance;

	// Token: 0x040000F2 RID: 242
	private AudioSource audioDevice;
}
