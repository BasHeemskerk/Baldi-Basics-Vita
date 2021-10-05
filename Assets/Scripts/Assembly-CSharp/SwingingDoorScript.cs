using System;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class SwingingDoorScript : MonoBehaviour
{
	// Token: 0x06000072 RID: 114 RVA: 0x00003F92 File Offset: 0x00002392
	private void Start()
	{
		this.myAudio = base.GetComponent<AudioSource>();
		this.bDoorLocked = true;
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00003FA8 File Offset: 0x000023A8
	private void Update()
	{
		if (!this.requirementMet & this.gc.notebooks >= 2) // If the player has collected 2 notebooks, unlock the doors
		{
			this.requirementMet = true;
			this.UnlockDoor();
		}
		if (this.openTime > 0f) // Door Open time decreases over time
		{
			this.openTime -= 1f * Time.deltaTime;
		}
		if (this.lockTime > 0f)
		{
			this.lockTime -= Time.deltaTime;
		}
		else if (this.bDoorLocked & this.requirementMet) // If the door is locked and the requirement is met, unlock the door
		{
			this.UnlockDoor();
		}
		if (this.openTime <= 0f & this.bDoorOpen & !this.bDoorLocked) // Closes the door
		{
			this.bDoorOpen = false;
			this.inside.sharedMaterial = this.closed; // Sets the door material to the closed material
			this.outside.sharedMaterial = this.closed; // Sets the door material to the closed material
        }
	}

	// Token: 0x06000074 RID: 116 RVA: 0x000040A4 File Offset: 0x000024A4
	private void OnTriggerStay(Collider other)
	{
		if (!this.bDoorLocked)
		{
			this.bDoorOpen = true;
			this.inside.sharedMaterial = this.open; // Sets the door material to the opened material
            this.outside.sharedMaterial = this.open; // Sets the door material to the opened material
            this.openTime = 2f;
		}
	}

	// Token: 0x06000075 RID: 117 RVA: 0x000040F0 File Offset: 0x000024F0
	private void OnTriggerEnter(Collider other)
	{
		if (this.gc.notebooks < 2 & other.tag == "Player") // If you touch the door before they can become openable, "You need to collect 2 notebooks before you can use these doors!"
		{
			this.myAudio.PlayOneShot(this.baldiDoor, 1f);
		}
		else if (!this.bDoorLocked) // If it isn't locked, make baldi hear it. And open it(but that is handled in a different piece of code for some reason)
		{
			this.myAudio.PlayOneShot(this.doorOpen, 1f);
			if (other.tag == "Player" && this.baldi.isActiveAndEnabled)
			{
				this.baldi.Hear(base.transform.position, 1f);
			}
		}
	}

	// Token: 0x06000076 RID: 118 RVA: 0x000041A0 File Offset: 0x000025A0
	public void LockDoor(float time)
	{
		this.barrier.enabled = true; // Enable the barrier
		this.obstacle.SetActive(true); // Enable the obsticale
		this.bDoorLocked = true;
		this.lockTime = time;
		this.inside.sharedMaterial = this.locked; // Sets the door material to the locked material
        this.outside.sharedMaterial = this.locked; // Sets the door material to the locked material
    }

	// Token: 0x06000077 RID: 119 RVA: 0x000041F8 File Offset: 0x000025F8
	private void UnlockDoor()
	{
		this.barrier.enabled = false; // Disables the barrier
		this.obstacle.SetActive(false); // Disables the obsticale
		this.bDoorLocked = false;
		this.inside.sharedMaterial = this.closed; // Sets the door material to the closed material
        this.outside.sharedMaterial = this.closed; // Sets the door material to the closed material
    }

	// Token: 0x04000093 RID: 147
	public GameControllerScript gc;

	// Token: 0x04000094 RID: 148
	public BaldiScript baldi;

	// Token: 0x04000095 RID: 149
	public MeshCollider barrier;

	// Token: 0x04000096 RID: 150
	public GameObject obstacle;

	// Token: 0x04000097 RID: 151
	public MeshCollider trigger;

	// Token: 0x04000098 RID: 152
	public MeshRenderer inside;

	// Token: 0x04000099 RID: 153
	public MeshRenderer outside;

	// Token: 0x0400009A RID: 154
	public Material closed;

	// Token: 0x0400009B RID: 155
	public Material open;

	// Token: 0x0400009C RID: 156
	public Material locked;

	// Token: 0x0400009D RID: 157
	public AudioClip doorOpen;

	// Token: 0x0400009E RID: 158
	public AudioClip baldiDoor;

	// Token: 0x0400009F RID: 159
	private float openTime;

	// Token: 0x040000A0 RID: 160
	private float lockTime;

	// Token: 0x040000A1 RID: 161
	private bool bDoorOpen;

	// Token: 0x040000A2 RID: 162
	private bool bDoorLocked;

	// Token: 0x040000A3 RID: 163
	private bool requirementMet;

	// Token: 0x040000A4 RID: 164
	private AudioSource myAudio;
}
