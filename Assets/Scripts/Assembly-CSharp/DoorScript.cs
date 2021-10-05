using System;
using UnityEngine;

// Token: 0x0200001A RID: 26
public class DoorScript : MonoBehaviour
{
	// Token: 0x0600006A RID: 106 RVA: 0x00003CE0 File Offset: 0x000020E0
	private void Start()
	{
		this.myAudio = base.GetComponent<AudioSource>();
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00003CF0 File Offset: 0x000020F0
	private void Update()
	{
		if (this.lockTime > 0f) // If the lock time is greater then 0, decrease lockTime
		{
			this.lockTime -= 1f * Time.deltaTime;
		}
		else if (this.bDoorLocked) //If the door is locked, unlock it
		{
			this.UnlockDoor();
		}
		if (this.openTime > 0f) // If the open time is greater then 0, decrease lockTime Decrease open time
        {
			this.openTime -= 1f * Time.deltaTime;
		}
		if (this.openTime <= 0f & this.bDoorOpen)
		{
			this.barrier.enabled = true; // Turn on collision
			this.invisibleBarrier.enabled = true; //Enable the invisible barrier
			this.bDoorOpen = false; //Set the door open status to false
			this.inside.sharedMaterial = this.closed; // Change one side of the door to the closed material
			this.outside.sharedMaterial = this.closed; // Change the other side of the door to the closed material
            if (this.silentOpens <= 0) //If the door isn't silent
			{
				this.myAudio.PlayOneShot(this.doorClose, 1f); //Play the door close sound
			}
		}
		if (Input.GetKeyDown(KeyCode.JoystickButton0) & Time.timeScale != 0f) //If the door is left clicked and the game isn't paused
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider == this.trigger & Vector3.Distance(this.player.position, base.transform.position) < this.openingDistance & !this.bDoorLocked))
			{
				if (this.baldi.isActiveAndEnabled & this.silentOpens <= 0)
				{
					this.baldi.Hear(base.transform.position, 1f); //If the door isn't silent, Baldi hears the door with a priority of 1.
				}
				this.OpenDoor();
				if (this.silentOpens > 0) //If the door is silent
				{
					this.silentOpens--; //Decrease the amount of opens the door will stay quite for.
				}
			}
		}
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00003EC0 File Offset: 0x000022C0
	public void OpenDoor()
	{
		if (this.silentOpens <= 0 && !this.bDoorOpen) //Play the door sound if the door isn't silent
		{
			this.myAudio.PlayOneShot(this.doorOpen, 1f);
		}
		this.barrier.enabled = false; //Disable the Barrier
		this.invisibleBarrier.enabled = false;//Disable the invisible Barrier
		this.bDoorOpen = true; //Set the door open status to false
		this.inside.sharedMaterial = this.open; //Change one side of the door to the open material
		this.outside.sharedMaterial = this.open; //Change the other side of the door to the open material
        this.openTime = 3f; //Set the open time to 3 seconds
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00003F46 File Offset: 0x00002346
	private void OnTriggerStay(Collider other)
	{
		if (!this.bDoorLocked & other.CompareTag("NPC")) //Open the door if it isn't locked and it is an NPC
		{
			this.OpenDoor();
		}
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00003F68 File Offset: 0x00002368
	public void LockDoor(float time) //Lock the door for a specified amount of time
	{
		this.bDoorLocked = true;
		this.lockTime = time;
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00003F78 File Offset: 0x00002378
	public void UnlockDoor() //Unlock the door
	{
		this.bDoorLocked = false;
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00003F81 File Offset: 0x00002381
	public void SilenceDoor() //Set the amount of times the door can be open silently
	{
		this.silentOpens = 4;
	}

	// Token: 0x04000081 RID: 129
	public float openingDistance;

	// Token: 0x04000082 RID: 130
	public Transform player;

	// Token: 0x04000083 RID: 131
	public BaldiScript baldi;

	// Token: 0x04000084 RID: 132
	public MeshCollider barrier;

	// Token: 0x04000085 RID: 133
	public MeshCollider trigger;

	// Token: 0x04000086 RID: 134
	public MeshCollider invisibleBarrier;

	// Token: 0x04000087 RID: 135
	public MeshRenderer inside;

	// Token: 0x04000088 RID: 136
	public MeshRenderer outside;

	// Token: 0x04000089 RID: 137
	public AudioClip doorOpen;

	// Token: 0x0400008A RID: 138
	public AudioClip doorClose;

	// Token: 0x0400008B RID: 139
	public Material closed;

	// Token: 0x0400008C RID: 140
	public Material open;

	// Token: 0x0400008D RID: 141
	private bool bDoorOpen;

	// Token: 0x0400008E RID: 142
	private bool bDoorLocked;

	// Token: 0x0400008F RID: 143
	public int silentOpens;

	// Token: 0x04000090 RID: 144
	private float openTime;

	// Token: 0x04000091 RID: 145
	public float lockTime;

	// Token: 0x04000092 RID: 146
	private AudioSource myAudio;
}
