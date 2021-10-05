using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000027 RID: 39
public class BaldiScript : MonoBehaviour
{
	// Token: 0x060000B8 RID: 184 RVA: 0x00006780 File Offset: 0x00004B80
	private void Start()
	{
		this.baldiAudio = base.GetComponent<AudioSource>(); //Get The Baldi Audio Source(Used mostly for the slap sound)
		this.agent = base.GetComponent<NavMeshAgent>(); //Get the Nav Mesh Agent
		this.timeToMove = this.baseTime; //Sets timeToMove to baseTime
		this.Wander(); //Start wandering
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x000067AC File Offset: 0x00004BAC
	private void Update()
	{
		if (this.timeToMove > 0f) //If timeToMove is greater then 0, decrease it
		{
			this.timeToMove -= 1f * Time.deltaTime;
		}
		else
		{
			this.Move(); //Start moving
		}
		if (this.coolDown > 0f) //If coolDown is greater then 0, decrease it
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
		if (this.baldiTempAnger > 0f) //Slowly decrease Baldi's temporary anger over time.
		{
			this.baldiTempAnger -= 0.02f * Time.deltaTime;
		}
		else
		{
			this.baldiTempAnger = 0f; //Cap its lowest value at 0
		}
		if (this.antiHearingTime > 0f) //Decrease antiHearingTime, then when it runs out stop the effects of the antiHearing tape
		{
			this.antiHearingTime -= Time.deltaTime;
		}
		else
		{
			this.antiHearing = false;
		}
		if (this.endless) //Only activate if the player is playing on endless mode
		{
			if (this.timeToAnger > 0f) //Decrease the timeToAnger
			{
				this.timeToAnger -= 1f * Time.deltaTime;
			}
			else
			{
				this.timeToAnger = this.angerFrequency; //Set timeToAnger to angerFrequency
				this.GetAngry(this.angerRate); //Get angry based on angerRate
				this.angerRate += this.angerRateRate; //Increase angerRate for next time
			}
		}
	}

	// Token: 0x060000BA RID: 186 RVA: 0x000068E0 File Offset: 0x00004CE0
	private void FixedUpdate()
	{
		if (this.moveFrames > 0f) //Move for a certain amount of frames, and then stop moving.(Ruler slapping)
		{
			this.moveFrames -= 1f;
			this.agent.speed = this.speed;
		}
		else
		{
			this.agent.speed = 0f;
		}
		Vector3 direction = this.player.position - base.transform.position; 
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position + Vector3.up * 2f, direction, out raycastHit, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player") //Create a raycast, if the raycast hits the player, Baldi can see the player
		{
			this.db = true;
			this.TargetPlayer(); //Start attacking the player
		}
		else
		{
			this.db = false;
		}
	}

	// Token: 0x060000BB RID: 187 RVA: 0x000069B4 File Offset: 0x00004DB4
	private void Wander()
	{
		this.wanderer.GetNewTarget(); //Get a new location
		this.agent.SetDestination(this.wanderTarget.position); //Head towards the position of the wanderTarget object
		this.coolDown = 1f; //Set the cooldown
		this.currentPriority = 0f;
	}

	// Token: 0x060000BC RID: 188 RVA: 0x000069EE File Offset: 0x00004DEE
	public void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position); //Target the player
		this.coolDown = 1f;
		this.currentPriority = 0f;
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00006A20 File Offset: 0x00004E20
	private void Move()
	{
		if (base.transform.position == this.previous & this.coolDown < 0f) // If Baldi reached his destination, start wandering
		{
			this.Wander();
		}
		this.moveFrames = 10f;
		this.timeToMove = this.baldiWait - this.baldiTempAnger; 
		this.previous = base.transform.position; // Set previous to Baldi's current location
		this.baldiAudio.PlayOneShot(this.slap); //Play the slap sound
		this.baldiAnimator.SetTrigger("slap"); // Play the slap animation
	}

	// Token: 0x060000BE RID: 190 RVA: 0x00006AAC File Offset: 0x00004EAC
	public void GetAngry(float value)
	{
		this.baldiAnger += value; // Increase Baldi's anger by the value provided
		if (this.baldiAnger < 0.5f) //Cap Baldi anger at a minimum of 0.5
		{
			this.baldiAnger = 0.5f;
		}
		this.baldiWait = -3f * this.baldiAnger / (this.baldiAnger + 2f / this.baldiSpeedScale) + 3f; //Some formula I don't understand.
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00006B0E File Offset: 0x00004F0E
	public void GetTempAngry(float value)
	{
		this.baldiTempAnger += value; //Increase Baldi's Temporary Anger
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00006B1E File Offset: 0x00004F1E
	public void Hear(Vector3 soundLocation, float priority)
	{
		if (!this.antiHearing && priority >= this.currentPriority) //If anti-hearing is not active and the priority is greater then the priority of the current sound
		{
			this.agent.SetDestination(soundLocation); //Go to that sound
			this.currentPriority = priority; //Set the current priority to the priority
		}
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00006B4B File Offset: 0x00004F4B
	public void ActivateAntiHearing(float t)
	{
		this.Wander(); //Start wandering
		this.antiHearing = true; //Set the antihearing variable to true for other scripts
		this.antiHearingTime = t; //Set the time the tape's effect on baldi will last
	}

	// Token: 0x0400012E RID: 302
	public bool db;

	// Token: 0x0400012F RID: 303
	public float baseTime;

	// Token: 0x04000130 RID: 304
	public float speed;

	// Token: 0x04000131 RID: 305
	public float timeToMove;

	// Token: 0x04000132 RID: 306
	public float baldiAnger;

	// Token: 0x04000133 RID: 307
	public float baldiTempAnger;

	// Token: 0x04000134 RID: 308
	public float baldiWait;

	// Token: 0x04000135 RID: 309
	public float baldiSpeedScale;

	// Token: 0x04000136 RID: 310
	private float moveFrames;

	// Token: 0x04000137 RID: 311
	private float currentPriority;

	// Token: 0x04000138 RID: 312
	public bool antiHearing;

	// Token: 0x04000139 RID: 313
	public float antiHearingTime;

	// Token: 0x0400013A RID: 314
	public float angerRate;

	// Token: 0x0400013B RID: 315
	public float angerRateRate;

	// Token: 0x0400013C RID: 316
	public float angerFrequency;

	// Token: 0x0400013D RID: 317
	public float timeToAnger;

	// Token: 0x0400013E RID: 318
	public bool endless;

	// Token: 0x0400013F RID: 319
	public Transform player;

	// Token: 0x04000140 RID: 320
	public Transform wanderTarget;

	// Token: 0x04000141 RID: 321
	public AILocationSelectorScript wanderer;

	// Token: 0x04000142 RID: 322
	private AudioSource baldiAudio;

	// Token: 0x04000143 RID: 323
	public AudioClip slap;

	// Token: 0x04000144 RID: 324
	public AudioClip[] speech = new AudioClip[3];

	// Token: 0x04000145 RID: 325
	public Animator baldiAnimator;

	// Token: 0x04000146 RID: 326
	public float coolDown;

	// Token: 0x04000147 RID: 327
	private Vector3 previous;

	// Token: 0x04000148 RID: 328
	private NavMeshAgent agent;
}
