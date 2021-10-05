using System;
using UnityEngine;

// Token: 0x0200002A RID: 42
public class FirstPrizeSpriteScript : MonoBehaviour
{
	// Token: 0x060000CE RID: 206 RVA: 0x00006F65 File Offset: 0x00005365
	private void Start()
	{
		this.sprite = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00006F74 File Offset: 0x00005374
	private void Update()
	{
		this.angleF = Mathf.Atan2(this.cam.position.z - this.body.position.z, this.cam.position.x - this.body.position.x) * 57.29578f;
		if (this.angleF < 0f)
		{
			this.angleF += 360f;
		}
		this.debug = this.body.eulerAngles.y;
		this.angleF += this.body.eulerAngles.y;
		this.angle = Mathf.RoundToInt(this.angleF / 22.5f);
		while (this.angle < 0 || this.angle >= 16)
		{
			this.angle += (int)(-16f * Mathf.Sign((float)this.angle));
		}
		this.sprite.sprite = this.sprites[this.angle];
	}

	// Token: 0x0400015D RID: 349
	public float debug;

	// Token: 0x0400015E RID: 350
	public int angle;

	// Token: 0x0400015F RID: 351
	public float angleF;

	// Token: 0x04000160 RID: 352
	private SpriteRenderer sprite;

	// Token: 0x04000161 RID: 353
	public Transform cam;

	// Token: 0x04000162 RID: 354
	public Transform body;

	// Token: 0x04000163 RID: 355
	public Sprite[] sprites = new Sprite[16];
}
