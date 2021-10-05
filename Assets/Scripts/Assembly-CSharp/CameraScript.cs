using System;
using UnityEngine;

// Token: 0x02000017 RID: 23
public class CameraScript : MonoBehaviour
{
	// Token: 0x06000060 RID: 96 RVA: 0x00003954 File Offset: 0x00001D54
	private void Start()
	{
		this.offset = base.transform.position - this.player.transform.position; //Defines the offset
		//this.offset(0, 1, 1);
	}

	// Token: 0x06000061 RID: 97 RVA: 0x0000397C File Offset: 0x00001D7C
	private void Update()
	{
		if (this.ps.jumpRope) //If the player is jump roping
		{
			this.velocity -= this.gravity * Time.deltaTime; //Decrease the velocity using gravity
			this.jumpHeight += this.velocity * Time.deltaTime; //Increase the jump height based on the velocity
			if (this.jumpHeight <= 0f) //When the player is on the floor, prevent the player from falling through.
			{
				this.jumpHeight = 0f;
				if (Input.GetKeyDown(KeyCode.JoystickButton8))
				{
					this.velocity = this.initVelocity; //Start the jump
				}
			}
			this.jumpHeightV3 = new Vector3(0f, this.jumpHeight, 0f); //Turn the float into a vector
		}
		else if (Input.GetButton("Look Behind"))
		{
			this.lookBehind = 180; //Look behind you
		}
		else
		{
			this.lookBehind = 0; //Don't look behind you
		}
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00003A44 File Offset: 0x00001E44
	private void LateUpdate()
	{
		base.transform.position = this.player.transform.position + this.offset; //Teleport to the player, then move based on the offset vector(if all other statements fail)
		if (!this.ps.gameOver & !this.ps.jumpRope)
		{
			base.transform.position = this.player.transform.position + this.offset; //Teleport to the player, then move based on the offset vector
            base.transform.rotation = this.player.transform.rotation * Quaternion.Euler(0f, (float)this.lookBehind, 0f); //Rotate based on player direction + lookbehind
		}
		else if (this.ps.gameOver)
		{
			base.transform.position = this.baldi.transform.position + this.baldi.transform.forward * 2f + new Vector3(0f, 5f, 0f); //Puts the camera in front of Baldi
			base.transform.LookAt(new Vector3(this.baldi.position.x, this.baldi.position.y + 5f, this.baldi.position.z)); //Makes the player look at baldi with an offset so the camera doesn't look at the feet
		}
		else if (this.ps.jumpRope)
		{
			base.transform.position = this.player.transform.position + this.offset + this.jumpHeightV3; //Apply the jump rope vector onto the normal offset
			base.transform.rotation = this.player.transform.rotation; //Rotate based on player direction
		}
	}

	// Token: 0x04000074 RID: 116
	public GameObject player;

	// Token: 0x04000075 RID: 117
	public PlayerScript ps;

	// Token: 0x04000076 RID: 118
	public Transform baldi;

	// Token: 0x04000077 RID: 119
	public float initVelocity;

	// Token: 0x04000078 RID: 120
	public float velocity;

	// Token: 0x04000079 RID: 121
	public float gravity;

	// Token: 0x0400007A RID: 122
	private int lookBehind;

	// Token: 0x0400007B RID: 123
	public Vector3 offset;

	// Token: 0x0400007C RID: 124
	public float jumpHeight;

	// Token: 0x0400007D RID: 125
	private Vector3 jumpHeightV3;
}
