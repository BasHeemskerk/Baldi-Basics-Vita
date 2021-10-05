using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class BsodaSparyScript : MonoBehaviour
{
	// Token: 0x0600005D RID: 93 RVA: 0x000038B0 File Offset: 0x00001CB0
	private void Start()
	{
		this.rb = base.GetComponent<Rigidbody>(); //Get the RigidBody
		this.rb.velocity = base.transform.forward * this.speed; //Move forward
		this.lifeSpan = 30f; //Set the lifespan
	}

	// Token: 0x0600005E RID: 94 RVA: 0x000038EC File Offset: 0x00001CEC
	private void Update()
	{
		this.rb.velocity = base.transform.forward * this.speed; //Move forward
        this.lifeSpan -= Time.deltaTime; // Decrease the lifespan variable
		if (this.lifeSpan < 0f) //When the lifespan timer ends, destroy the BSODA
		{
			UnityEngine.Object.Destroy(base.gameObject, 0f);
		}
	}

	// Token: 0x04000071 RID: 113
	public float speed;

	// Token: 0x04000072 RID: 114
	private float lifeSpan;

	// Token: 0x04000073 RID: 115
	private Rigidbody rb;
}
