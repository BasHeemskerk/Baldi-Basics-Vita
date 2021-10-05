using System;
using UnityEngine;

// Token: 0x02000019 RID: 25
public class CraftersTriggerScript : MonoBehaviour
{
	// Token: 0x06000067 RID: 103 RVA: 0x00003C7C File Offset: 0x0000207C
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			this.crafters.GiveLocation(this.goTarget.position, false);
		}
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00003CAA File Offset: 0x000020AA
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			this.crafters.GiveLocation(this.fleeTarget.position, true);
		}
	}

	// Token: 0x0400007E RID: 126
	public Transform goTarget;

	// Token: 0x0400007F RID: 127
	public Transform fleeTarget;

	// Token: 0x04000080 RID: 128
	public CraftersScript crafters;
}
