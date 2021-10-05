using System;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class QuarterSpawnScript : MonoBehaviour
{
	// Token: 0x06000049 RID: 73 RVA: 0x0000365E File Offset: 0x00001A5E
	private void Start()
	{
		this.wanderer.QuarterExclusive();
		base.transform.position = this.location.position + Vector3.up * 4f;
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00003695 File Offset: 0x00001A95
	private void Update()
	{
	}

	// Token: 0x04000067 RID: 103
	public AILocationSelectorScript wanderer;

	// Token: 0x04000068 RID: 104
	public Transform location;
}
