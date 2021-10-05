using System;
using UnityEngine;

// Token: 0x02000029 RID: 41
public class FacultyTriggerScript : MonoBehaviour
{
	// Token: 0x060000CA RID: 202 RVA: 0x00006F14 File Offset: 0x00005314
	private void Start()
	{
		this.hitBox = base.GetComponent<BoxCollider>();
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00006F22 File Offset: 0x00005322
	private void Update()
	{
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00006F24 File Offset: 0x00005324
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player")) //If it is a player
		{
			this.ps.ResetGuilt("faculty", 1f); //Make the player guilty of entering school faculty for 1 second
		}
	}

	// Token: 0x0400015B RID: 347
	public PlayerScript ps;

	// Token: 0x0400015C RID: 348
	private BoxCollider hitBox;
}
