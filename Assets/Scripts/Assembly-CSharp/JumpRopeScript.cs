using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200000F RID: 15
public class JumpRopeScript : MonoBehaviour
{
	// Token: 0x0600003C RID: 60 RVA: 0x00003068 File Offset: 0x00001468
	private void OnEnable()
	{
		this.jumpDelay = 1f;
		this.ropeHit = true; 
		this.jumpStarted = false; 
		this.jumps = 0;
		this.jumpCount.text = 0 + "/5";
		this.playtime.audioDevice.PlayOneShot(this.playtime.aud_ReadyGo);
	}

	// Token: 0x0600003D RID: 61 RVA: 0x000030CC File Offset: 0x000014CC
	private void Update()
	{
		if (this.jumpDelay > 0f) //Decrease jumpDelay countdown
		{
			this.jumpDelay -= Time.deltaTime;
		}
		else if (!this.jumpStarted) //If the jump hasn't started
		{
			this.jumpStarted = true; //Start the jump
			this.ropePosition = 1f; //Set the rope position to 1f
			this.rope.SetTrigger("ActivateJumpRope"); //Activate the jumprope
			this.ropeHit = false;
		}
		if (this.ropePosition > 0f) 
		{
			this.ropePosition -= Time.deltaTime;
		}
		else if (!this.ropeHit) //If the player has not tried to hit the rope
		{
			this.RopeHit();
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x0000316C File Offset: 0x0000156C
	private void RopeHit()
	{
		this.ropeHit = true; //Set ropehit to true
		if (this.cs.jumpHeight <= 0.2f)
		{
			this.Fail(); //Fail
		}
		else
		{
			this.Success(); //Succeed
		}
		this.jumpStarted = false;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x000031A4 File Offset: 0x000015A4
	private void Success()
	{
		this.playtime.audioDevice.Stop(); //Stop all of the lines playtime is currently speaking
		this.playtime.audioDevice.PlayOneShot(this.playtime.aud_Numbers[this.jumps]);
		this.jumps++;
		this.jumpCount.text = this.jumps + "/5";
		this.jumpDelay = 0.5f;
		if (this.jumps >= 5) //If players complete the minigame
		{
			this.playtime.audioDevice.Stop(); //Stop playtime from talking
			this.playtime.audioDevice.PlayOneShot(this.playtime.aud_Congrats);
			this.ps.DeactivateJumpRope(); //Deactivate the jumprope
		}
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00003260 File Offset: 0x00001660
	private void Fail()
	{
		this.jumps = 0; //Reset jumps
		this.jumpCount.text = this.jumps + "/5";
		this.jumpDelay = 2f; //Set the jump delay to 2 seconds to allow playtime to finish her line before the rope starts again
		this.playtime.audioDevice.PlayOneShot(this.playtime.aud_Oops);
	}

	// Token: 0x04000046 RID: 70
	public Text jumpCount;

	// Token: 0x04000047 RID: 71
	public Animator rope;

	// Token: 0x04000048 RID: 72
	public CameraScript cs;

	// Token: 0x04000049 RID: 73
	public PlayerScript ps;

	// Token: 0x0400004A RID: 74
	public PlaytimeScript playtime;

	// Token: 0x0400004B RID: 75
	public int jumps;

	// Token: 0x0400004C RID: 76
	public float jumpDelay;

	// Token: 0x0400004D RID: 77
	public float ropePosition;

	// Token: 0x0400004E RID: 78
	public bool ropeHit;

	// Token: 0x0400004F RID: 79
	public bool jumpStarted;
}
