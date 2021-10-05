using System;
using UnityEngine;

// Token: 0x02000009 RID: 9
public class DebugFirstPrizeScript : MonoBehaviour
{
	// Token: 0x06000021 RID: 33 RVA: 0x0000280A File Offset: 0x00000C0A
	private void Start()
	{
	}

	// Token: 0x06000022 RID: 34 RVA: 0x0000280C File Offset: 0x00000C0C
	private void Update()
	{
		base.transform.position = this.first.position + new Vector3((float)Mathf.RoundToInt(this.first.forward.x), 0f, (float)Mathf.RoundToInt(this.first.forward.z)) * 3f;
	}

	// Token: 0x04000020 RID: 32
	public Transform player;

	// Token: 0x04000021 RID: 33
	public Transform first;
}
