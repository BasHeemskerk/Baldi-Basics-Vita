using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x0200002B RID: 43
public class PrincipalScript : MonoBehaviour
{
	// Token: 0x060000D1 RID: 209 RVA: 0x000070E1 File Offset: 0x000054E1
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>(); //Get the agent
		this.audioQueue = base.GetComponent<AudioQueueScript>();
		this.audioDevice = base.GetComponent<AudioSource>();
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00007108 File Offset: 0x00005508
	private void Update()
	{
		if (this.seesRuleBreak)
		{
			this.timeSeenRuleBreak += 1f * Time.deltaTime;
			if ((double)this.timeSeenRuleBreak >= 0.5 & !this.angry) // If the principal sees the player break a rule for more then 1/2 of a second
			{
				this.angry = true;
				this.seesRuleBreak = false;
				this.timeSeenRuleBreak = 0f;
				this.TargetPlayer(); //Target the player
				this.CorrectPlayer();
			}
		}
		else
		{
			this.timeSeenRuleBreak = 0f;
		}
		if (this.coolDown > 0f)
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x000071BC File Offset: 0x000055BC
	private void FixedUpdate()
	{
		if (!this.angry) // If the principal isn't angry
		{
			Vector3 direction = this.player.position - base.transform.position;
			RaycastHit raycastHit; // If he sees the player and the player has guilt
			if (Physics.Raycast(base.transform.position, direction, out raycastHit, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player" & this.playerScript.guilt > 0f & !this.inOffice & !this.angry)
			{
				this.seesRuleBreak = true;
			}
			else
			{
				this.seesRuleBreak = false;
				if (this.agent.velocity.magnitude <= 1f & this.coolDown <= 0f)
				{
					this.Wander(); // Start wandering again
				}
			}
			direction = this.bully.position - base.transform.position; // If the bully is guilty, target the bully
			if (Physics.Raycast(base.transform.position, direction, out raycastHit, float.PositiveInfinity, 3) & raycastHit.transform.name == "Its a Bully" & this.bullyScript.guilt > 0f & !this.inOffice & !this.angry)
			{
				this.TargetBully();
			}
		}
		else
		{
			this.TargetPlayer(); // If the principal is angry, target the player
		}
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00007324 File Offset: 0x00005724
	private void Wander()
	{
		this.wanderer.GetNewTarget();
		this.agent.SetDestination(this.wanderTarget.position);
		if (this.agent.isStopped)
		{
			this.agent.isStopped = false;
		}
		this.coolDown = 1f;
		if (UnityEngine.Random.Range(0f, 10f) <= 1f)
		{
			this.quietAudioDevice.PlayOneShot(this.aud_Whistle);
		}
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x000073A4 File Offset: 0x000057A4
	private void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position);
		this.coolDown = 1f;
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x000073C8 File Offset: 0x000057C8
	private void TargetBully()
	{
		if (!this.bullySeen)
		{
			this.agent.SetDestination(this.bully.position);
			this.audioQueue.QueueAudio(this.audNoBullying);
			this.bullySeen = true;
		}
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x00007404 File Offset: 0x00005804
	private void CorrectPlayer()
	{
		this.audioQueue.ClearAudioQueue();
		if (this.playerScript.guiltType == "faculty")
		{
			this.audioQueue.QueueAudio(this.audNoFaculty);
		}
		else if (this.playerScript.guiltType == "running")
		{
			this.audioQueue.QueueAudio(this.audNoRunning);
		}
		else if (this.playerScript.guiltType == "drink")
		{
			this.audioQueue.QueueAudio(this.audNoDrinking);
		}
		else if (this.playerScript.guiltType == "escape")
		{
			this.audioQueue.QueueAudio(this.audNoEscaping);
		}
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x000074D8 File Offset: 0x000058D8
	private void OnTriggerStay(Collider other)
	{
		if (other.name == "Office Trigger")
		{
			this.inOffice = true;
		}
		if (other.tag == "Player" & this.angry & !this.inOffice)
		{
			this.inOffice = true;
			this.agent.Warp(new Vector3(10f, 0f, 170f)); //Teleport the principal to the office
			this.agent.isStopped = true; //Stop the principal from moving
			player.transform.position = new Vector3(10f, 4f, 160f); // Teleport the player to the office
			player.transform.LookAt(new Vector3(base.transform.position.x, other.transform.position.y, base.transform.position.z)); // Get the plaer to look at the principal
			this.audioQueue.QueueAudio(this.aud_Delay);
			this.audioQueue.QueueAudio(this.audTimes[this.detentions]); //Play the detention time sound
			this.audioQueue.QueueAudio(this.audDetention);
			int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 2f));
			this.audioQueue.QueueAudio(this.audScolds[num]); // Say one of the other lines
			this.officeDoor.LockDoor((float)this.lockTime[this.detentions]); // Lock the door
			this.baldiScript.Hear(base.transform.position, 5f);
			this.coolDown = 5f;
			this.angry = false;
			this.detentions++;
			if (this.detentions > 4) // Prevent detention number from going above 4
			{
				this.detentions = 4;
			}
		}
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x0000768E File Offset: 0x00005A8E
	private void OnTriggerExit(Collider other)
	{
		if (other.name == "Office Trigger")
		{
			this.inOffice = false;
		}
		if (other.name == "Its a Bully")
		{
			this.bullySeen = false;
		}
	}

	// Token: 0x04000164 RID: 356
	public bool seesRuleBreak;

	// Token: 0x04000165 RID: 357
	public Transform player;

	// Token: 0x04000166 RID: 358
	public Transform bully;

	// Token: 0x04000167 RID: 359
	public bool bullySeen;

	// Token: 0x04000168 RID: 360
	public PlayerScript playerScript;

	// Token: 0x04000169 RID: 361
	public BullyScript bullyScript;

	// Token: 0x0400016A RID: 362
	public BaldiScript baldiScript;

	// Token: 0x0400016B RID: 363
	public Transform wanderTarget;

	// Token: 0x0400016C RID: 364
	public AILocationSelectorScript wanderer;

	// Token: 0x0400016D RID: 365
	public DoorScript officeDoor;

	// Token: 0x0400016E RID: 366
	public float coolDown;

	// Token: 0x0400016F RID: 367
	public float timeSeenRuleBreak;

	// Token: 0x04000170 RID: 368
	public bool angry;

	// Token: 0x04000171 RID: 369
	public bool inOffice;

	// Token: 0x04000172 RID: 370
	public int detentions;

	// Token: 0x04000173 RID: 371
	private int[] lockTime = new int[]
	{
		15,
		30,
		45,
		60,
		99
	};

	// Token: 0x04000174 RID: 372
	public AudioClip[] audTimes = new AudioClip[5];

	// Token: 0x04000175 RID: 373
	public AudioClip[] audScolds = new AudioClip[3];

	// Token: 0x04000176 RID: 374
	public AudioClip audDetention;

	// Token: 0x04000177 RID: 375
	public AudioClip audNoDrinking;

	// Token: 0x04000178 RID: 376
	public AudioClip audNoBullying;

	// Token: 0x04000179 RID: 377
	public AudioClip audNoFaculty;

	// Token: 0x0400017A RID: 378
	public AudioClip audNoLockers;

	// Token: 0x0400017B RID: 379
	public AudioClip audNoRunning;

	// Token: 0x0400017C RID: 380
	public AudioClip audNoStabbing;

	// Token: 0x0400017D RID: 381
	public AudioClip audNoEscaping;

	// Token: 0x0400017E RID: 382
	public AudioClip aud_Whistle;

	// Token: 0x0400017F RID: 383
	public AudioClip aud_Delay;

	// Token: 0x04000180 RID: 384
	private NavMeshAgent agent;

	// Token: 0x04000181 RID: 385
	private AudioQueueScript audioQueue;

	// Token: 0x04000182 RID: 386
	private AudioSource audioDevice;

	// Token: 0x04000183 RID: 387
	public AudioSource quietAudioDevice;
}
