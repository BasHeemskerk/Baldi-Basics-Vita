using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x0200000D RID: 13
public class FirstPrizeScript : MonoBehaviour
{
	// Token: 0x0600002D RID: 45 RVA: 0x00002A30 File Offset: 0x00000E30
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>(); //Get the Nav Mesh Agent
		this.coolDown = 1f; //Set the cooldown to 1 second
		this.Wander(); //Start wandering
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00002A50 File Offset: 0x00000E50
	private void Update()
	{
		if (this.coolDown > 0f) //Decrease the cooldown
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
		if (this.autoBrakeCool > 0f) //Decrease the autoBrakeCool
		{
			this.autoBrakeCool -= 1f * Time.deltaTime;
		}
		else // When autoBrakeCool is less then 0
		{
			this.agent.autoBraking = true;
		}
		this.angleDiff = Mathf.DeltaAngle(base.transform.eulerAngles.y, Mathf.Atan2(this.agent.steeringTarget.x - base.transform.position.x, this.agent.steeringTarget.z - base.transform.position.z) * 57.29578f);
		if (this.crazyTime <= 0f)
		{
			if (Mathf.Abs(this.angleDiff) < 5f)
			{
				base.transform.LookAt(new Vector3(this.agent.steeringTarget.x, base.transform.position.y, this.agent.steeringTarget.z));
				this.agent.speed = this.currentSpeed;
			}
			else
			{
				base.transform.Rotate(new Vector3(0f, this.turnSpeed * Mathf.Sign(this.angleDiff) * Time.deltaTime, 0f));
				this.agent.speed = 0f;
			}
		}
		else
		{
			this.agent.speed = 0f;
			base.transform.Rotate(new Vector3(0f, 180f * Time.deltaTime, 0f));
			this.crazyTime -= Time.deltaTime;
		}
		this.motorAudio.pitch = (this.agent.velocity.magnitude + 1f) * Time.timeScale;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00002C78 File Offset: 0x00001078
	private void FixedUpdate()
	{
		Vector3 direction = this.player.position - base.transform.position;
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position, direction, out raycastHit, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player")
		{
			if (!this.playerSeen && !this.audioDevice.isPlaying)
			{
				int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
				this.audioDevice.PlayOneShot(this.aud_Found[num]);
			}
			this.playerSeen = true;
			this.TargetPlayer();
			this.currentSpeed = this.runSpeed;
		}
		else
		{
			this.currentSpeed = this.normSpeed;
			if (this.playerSeen & this.coolDown <= 0f)
			{
				if (!this.audioDevice.isPlaying)
				{
					int num2 = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
					this.audioDevice.PlayOneShot(this.aud_Lost[num2]);
				}
				this.playerSeen = false;
				this.Wander();
			}
			else if (this.agent.velocity.magnitude <= 1f & this.coolDown <= 0f & (base.transform.position - this.agent.destination).magnitude < 5f)
			{
				this.Wander();
			}
		}
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00002E14 File Offset: 0x00001214
	private void Wander()
	{
		this.wanderer.GetNewTargetHallway();
		this.agent.SetDestination(this.wanderTarget.position);
		this.hugAnnounced = false;
		int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 9f));
		if (!this.audioDevice.isPlaying & num == 0 & this.coolDown <= 0f)
		{
			int num2 = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
			this.audioDevice.PlayOneShot(this.aud_Random[num2]);
		}
		this.coolDown = 1f;
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00002EBB File Offset: 0x000012BB
	private void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position);
		this.coolDown = 0.5f;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00002EE0 File Offset: 0x000012E0
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			if (!this.audioDevice.isPlaying & !this.hugAnnounced)
			{
				int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
				this.audioDevice.PlayOneShot(this.aud_Hug[num]);
				this.hugAnnounced = true;
			}
			this.agent.autoBraking = false;
		}
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00002F5A File Offset: 0x0000135A
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			this.autoBrakeCool = 1f;
		}
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00002F7C File Offset: 0x0000137C
	public void GoCrazy()
	{
		this.crazyTime = 15f;
	}

	// Token: 0x04000029 RID: 41
	public float debug;

	// Token: 0x0400002A RID: 42
	public float turnSpeed;

	// Token: 0x0400002B RID: 43
	public float str;

	// Token: 0x0400002C RID: 44
	public float angleDiff;

	// Token: 0x0400002D RID: 45
	public float normSpeed;

	// Token: 0x0400002E RID: 46
	public float runSpeed;

	// Token: 0x0400002F RID: 47
	public float currentSpeed;

	// Token: 0x04000030 RID: 48
	public float acceleration;

	// Token: 0x04000031 RID: 49
	public float speed;

	// Token: 0x04000032 RID: 50
	public float autoBrakeCool;

	// Token: 0x04000033 RID: 51
	public float crazyTime;

	// Token: 0x04000034 RID: 52
	public Quaternion targetRotation;

	// Token: 0x04000035 RID: 53
	public float coolDown;

	// Token: 0x04000036 RID: 54
	public bool playerSeen;

	// Token: 0x04000037 RID: 55
	public bool hugAnnounced;

	// Token: 0x04000038 RID: 56
	public AILocationSelectorScript wanderer;

	// Token: 0x04000039 RID: 57
	public Transform player;

	// Token: 0x0400003A RID: 58
	public Transform wanderTarget;

	// Token: 0x0400003B RID: 59
	public AudioClip[] aud_Found = new AudioClip[2];

	// Token: 0x0400003C RID: 60
	public AudioClip[] aud_Lost = new AudioClip[2];

	// Token: 0x0400003D RID: 61
	public AudioClip[] aud_Hug = new AudioClip[2];

	// Token: 0x0400003E RID: 62
	public AudioClip[] aud_Random = new AudioClip[2];

	// Token: 0x0400003F RID: 63
	public AudioSource audioDevice;

	// Token: 0x04000040 RID: 64
	public AudioSource motorAudio;

	// Token: 0x04000041 RID: 65
	private NavMeshAgent agent;
}
