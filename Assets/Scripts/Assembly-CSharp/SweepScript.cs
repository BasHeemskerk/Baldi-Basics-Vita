using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000032 RID: 50
public class SweepScript : MonoBehaviour
{
	// Token: 0x060000F7 RID: 247 RVA: 0x000082D4 File Offset: 0x000066D4
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>();
		this.audioDevice = base.GetComponent<AudioSource>();
		this.origin = base.transform.position;
		this.waitTime = UnityEngine.Random.Range(120f, 180f);
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x00008314 File Offset: 0x00006714
	private void Update()
	{
		if (this.coolDown > 0f)
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
		if (this.waitTime > 0f)
		{
			this.waitTime -= Time.deltaTime;
		}
		else if (!this.active)
		{
			this.active = true;
			this.wanders = 0;
			this.Wander(); // Start wandering
			this.audioDevice.PlayOneShot(this.aud_Intro); // "Looks like its sweeping time!"
		}
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x000083A0 File Offset: 0x000067A0
	private void FixedUpdate()
	{
		if ((double)this.agent.velocity.magnitude <= 0.1 & this.coolDown <= 0f & this.wanders < 5 & this.active) // If Gotta Sweep has roamed around the school 5 times
		{
			this.Wander(); // Wander
		}
		else if (this.wanders >= 5)
		{
			this.GoHome(); // Go back to the closet
		}
	}

	// Token: 0x060000FA RID: 250 RVA: 0x00008416 File Offset: 0x00006816
	private void Wander()
	{
		this.wanderer.GetNewTargetHallway();
		this.agent.SetDestination(this.wanderTarget.position);
		this.coolDown = 1f;
		this.wanders++;
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00008453 File Offset: 0x00006853
	private void GoHome()
	{
		this.agent.SetDestination(this.origin);
		this.waitTime = UnityEngine.Random.Range(120f, 180f);
		this.wanders = 0;
		this.active = false;
	}

	// Token: 0x060000FC RID: 252 RVA: 0x0000848A File Offset: 0x0000688A
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "NPC" || other.tag == "Player")
		{
			this.audioDevice.PlayOneShot(this.aud_Sweep);
		}
	}

	// Token: 0x040001A8 RID: 424
	public Transform wanderTarget;

	// Token: 0x040001A9 RID: 425
	public AILocationSelectorScript wanderer;

	// Token: 0x040001AA RID: 426
	public float coolDown;

	// Token: 0x040001AB RID: 427
	public float waitTime;

	// Token: 0x040001AC RID: 428
	public int wanders;

	// Token: 0x040001AD RID: 429
	public bool active;

	// Token: 0x040001AE RID: 430
	private Vector3 origin;

	// Token: 0x040001AF RID: 431
	public AudioClip aud_Sweep;

	// Token: 0x040001B0 RID: 432
	public AudioClip aud_Intro;

	// Token: 0x040001B1 RID: 433
	private NavMeshAgent agent;

	// Token: 0x040001B2 RID: 434
	private AudioSource audioDevice;
}
