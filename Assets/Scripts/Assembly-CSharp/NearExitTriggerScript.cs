using System;
using UnityEngine;

// Token: 0x02000024 RID: 36
public class NearExitTriggerScript : MonoBehaviour
{
	// Token: 0x060000AC RID: 172 RVA: 0x000064BC File Offset: 0x000048BC
	private void OnTriggerEnter(Collider other)
	{
		if (this.gc.exitsReached < 3 & this.gc.finaleMode & other.tag == "Player")
		{
			this.gc.ExitReached(); //Tells the game an exit was reache
			this.es.Lower(); //Tells the exit to lower
			this.gc.baldiScrpt.Hear(base.transform.position, 5f); //Tells Baldi to go to that position
		}
	}

	// Token: 0x04000123 RID: 291
	public GameControllerScript gc;

	// Token: 0x04000124 RID: 292
	public EntranceScript es;
}
