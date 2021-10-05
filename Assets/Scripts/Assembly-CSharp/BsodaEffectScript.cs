using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000006 RID: 6
public class BsodaEffectScript : MonoBehaviour
{
	// Token: 0x0600000F RID: 15 RVA: 0x00002256 File Offset: 0x00000656
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>(); //Get the object/character's AI Agent
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002264 File Offset: 0x00000664
	private void Update()
	{
		if (this.inBsoda)
		{
			this.agent.velocity = this.otherVelocity; //Set the agent's velocity to the velocity of the other object
		}
		if (this.failSave > 0f)
		{
			this.failSave -= Time.deltaTime;
		}
		else
		{
			this.inBsoda = false;
		}
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000022BB File Offset: 0x000006BB
	private void FixedUpdate()
	{
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000022C0 File Offset: 0x000006C0
	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "BSODA") //If its a BSODA
		{
			this.inBsoda = true;
			this.otherVelocity = other.GetComponent<Rigidbody>().velocity; // Set the velocity to the velocity of the BSODA
			this.failSave = 1f;
		}
		else if (other.transform.name == "Gotta Sweep") //If its Gotta Sweep
		{
			this.inBsoda = true;
			this.otherVelocity = base.transform.forward * this.agent.speed * 0.1f + other.GetComponent<NavMeshAgent>().velocity;
			this.failSave = 1f;
		}
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00002371 File Offset: 0x00000771
	private void OnTriggerExit()
	{
		this.inBsoda = false; // When they are out of the BSODA, set inBsoda to false
	}

	// Token: 0x0400000E RID: 14
	private NavMeshAgent agent;

	// Token: 0x0400000F RID: 15
	private Vector3 otherVelocity;

	// Token: 0x04000010 RID: 16
	private bool inBsoda;

	// Token: 0x04000011 RID: 17
	private float failSave;
}
