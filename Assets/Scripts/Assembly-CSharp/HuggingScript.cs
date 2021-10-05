using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x0200000E RID: 14
public class HuggingScript : MonoBehaviour
{
	// Token: 0x06000036 RID: 54 RVA: 0x00002F91 File Offset: 0x00001391
	private void Start()
	{
		this.rb = base.GetComponent<Rigidbody>(); //Get the rigid body
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00002F9F File Offset: 0x0000139F
	private void Update()
	{
		if (this.failSave > 0f) // Decrease failsafe until it reaches 0
		{
			this.failSave -= Time.deltaTime;
		}
		else // When failsafe timer is over, set inBsoda to false
		{
			this.inBsoda = false;
		}
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00002FCF File Offset: 0x000013CF
	private void FixedUpdate()
	{
		if (this.inBsoda) //When inBsoda is true
		{
			this.rb.velocity = this.otherVelocity; //Set the objects velocity to the velocity of first prize
		}
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00002FF0 File Offset: 0x000013F0
	private void OnTriggerStay(Collider other)
	{
		if (other.transform.name == "1st Prize") //If touching first prize
		{
			this.inBsoda = true; //InBsoda = true
			this.otherVelocity = this.rb.velocity * 0.1f + other.GetComponent<NavMeshAgent>().velocity;
			this.failSave = 1f; //Set failsafe to 1 second
		}
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00003054 File Offset: 0x00001454
	private void OnTriggerExit()
	{
		this.inBsoda = false;
	}

	// Token: 0x04000042 RID: 66
	private Rigidbody rb;

	// Token: 0x04000043 RID: 67
	private Vector3 otherVelocity;

	// Token: 0x04000044 RID: 68
	public bool inBsoda;

	// Token: 0x04000045 RID: 69
	private float failSave;
}
