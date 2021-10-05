using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000010 RID: 16
public class PlaytimeScript : MonoBehaviour
{
	// Token: 0x06000042 RID: 66 RVA: 0x000032DB File Offset: 0x000016DB
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>(); //Get AI Agent
		this.audioDevice = base.GetComponent<AudioSource>();
		this.Wander(); //Start wandering
	}

	// Token: 0x06000043 RID: 67 RVA: 0x000032FC File Offset: 0x000016FC
	private void Update()
	{
		if (this.coolDown > 0f)
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
		if (this.playCool >= 0f)
		{
			this.playCool -= Time.deltaTime;
		}
		else if (this.animator.GetBool("disappointed"))
		{
			this.playCool = 0f;
			this.animator.SetBool("disappointed", false);
		}
	}

	// Token: 0x06000044 RID: 68 RVA: 0x0000338C File Offset: 0x0000178C
	private void FixedUpdate()
	{
		if (!this.ps.jumpRope)
		{
			Vector3 direction = this.player.position - base.transform.position;
			RaycastHit raycastHit;
			if (Physics.Raycast(base.transform.position, direction, out raycastHit, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player" & (base.transform.position - this.player.position).magnitude <= 80f & this.playCool <= 0f)
			{
                //If playtime sees the player, she chases after them
				this.playerSeen = true;
				this.TargetPlayer();
			}
			else if (this.playerSeen & this.coolDown <= 0f)
			{
                //If the player seen cooldown expires, she will just start wandering again
				this.playerSeen = false;
				this.Wander();
			}
			else if (this.agent.velocity.magnitude <= 1f & this.coolDown <= 0f)
			{
				this.Wander();
			}
			this.jumpRopeStarted = false;
		}
		else
		{
			if (!this.jumpRopeStarted)
			{
				this.agent.Warp(base.transform.position - base.transform.forward * 10f); //Teleport back after touching the player
			}
			this.jumpRopeStarted = true;
			this.agent.speed = 0f;
			this.playCool = 15f;
		}
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00003520 File Offset: 0x00001920
	private void Wander()
	{
		this.wanderer.GetNewTargetHallway();
		this.agent.SetDestination(this.wanderTarget.position);
		this.agent.speed = 15f;
		this.playerSpotted = false;
		this.audVal = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
		if (!this.audioDevice.isPlaying)
		{
			this.audioDevice.PlayOneShot(this.aud_Random[this.audVal]);
		}
		this.coolDown = 1f;
	}

	// Token: 0x06000046 RID: 70 RVA: 0x000035B4 File Offset: 0x000019B4
	private void TargetPlayer()
	{
		this.animator.SetBool("disappointed", false); //No longer be sad
		this.agent.SetDestination(this.player.position); // Go after the player
		this.agent.speed = 20f; // Speed up
		this.coolDown = 0.2f;
		if (!this.playerSpotted)
		{
			this.playerSpotted = true;
			this.audioDevice.PlayOneShot(this.aud_LetsPlay);
		}
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00003627 File Offset: 0x00001A27
	public void Disappoint()
	{
		this.animator.SetBool("disappointed", true); //Get sad
		this.audioDevice.Stop();
		this.audioDevice.PlayOneShot(this.aud_Sad);
	}

	// Token: 0x04000050 RID: 80
	public bool db;

	// Token: 0x04000051 RID: 81
	public bool playerSeen;

	// Token: 0x04000052 RID: 82
	public bool disappointed;

	// Token: 0x04000053 RID: 83
	public int audVal;

	// Token: 0x04000054 RID: 84
	public Animator animator;

	// Token: 0x04000055 RID: 85
	public Transform player;

	// Token: 0x04000056 RID: 86
	public PlayerScript ps;

	// Token: 0x04000057 RID: 87
	public Transform wanderTarget;

	// Token: 0x04000058 RID: 88
	public AILocationSelectorScript wanderer;

	// Token: 0x04000059 RID: 89
	public float coolDown;

	// Token: 0x0400005A RID: 90
	public float playCool;

	// Token: 0x0400005B RID: 91
	public bool playerSpotted;

	// Token: 0x0400005C RID: 92
	public bool jumpRopeStarted;

	// Token: 0x0400005D RID: 93
	private NavMeshAgent agent;

	// Token: 0x0400005E RID: 94
	public AudioClip[] aud_Numbers = new AudioClip[10];

	// Token: 0x0400005F RID: 95
	public AudioClip[] aud_Random = new AudioClip[2];

	// Token: 0x04000060 RID: 96
	public AudioClip aud_Instrcutions;

	// Token: 0x04000061 RID: 97
	public AudioClip aud_Oops;

	// Token: 0x04000062 RID: 98
	public AudioClip aud_LetsPlay;

	// Token: 0x04000063 RID: 99
	public AudioClip aud_Congrats;

	// Token: 0x04000064 RID: 100
	public AudioClip aud_ReadyGo;

	// Token: 0x04000065 RID: 101
	public AudioClip aud_Sad;

	// Token: 0x04000066 RID: 102
	public AudioSource audioDevice;
}
