using System;
using UnityEngine;

// Token: 0x02000026 RID: 38
public class AILocationSelectorScript : MonoBehaviour
{
	// Token: 0x060000B4 RID: 180 RVA: 0x00006694 File Offset: 0x00004A94
	public void GetNewTarget()
	{
		this.id = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 28f)); //Get a random number between 0 and 28
		base.transform.position = this.newLocation[this.id].position; //Set it's location to a position in a list of positions using the ID variable that just got set.
		this.ambience.PlayAudio(); //Play an ambience audio
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x000066E4 File Offset: 0x00004AE4
	public void GetNewTargetHallway()
	{
		this.id = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 15f)); //Get a random number between 0 and 15
		base.transform.position = this.newLocation[this.id].position; //Set it's location to a position in a list of positions using the ID variable that just got set.
        this.ambience.PlayAudio(); //Play an ambience audio
    }

	// Token: 0x060000B6 RID: 182 RVA: 0x00006733 File Offset: 0x00004B33
	public void QuarterExclusive()
	{
		this.id = Mathf.RoundToInt(UnityEngine.Random.Range(1f, 15f)); //Get a random number between 0 and 15
        base.transform.position = this.newLocation[this.id].position; //Set it's location to a position in a list of positions using the ID variable that just got set.
    }

	// Token: 0x0400012B RID: 299
	public Transform[] newLocation = new Transform[29];

	// Token: 0x0400012C RID: 300
	public AmbienceScript ambience;

	// Token: 0x0400012D RID: 301
	private int id;
}
