using System;
using UnityEngine;

// Token: 0x02000031 RID: 49
public class YouWonScript : MonoBehaviour
{
	// Token: 0x060000F4 RID: 244 RVA: 0x00008296 File Offset: 0x00006696
	private void Start()
	{
		this.delay = 10f;
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x000082A3 File Offset: 0x000066A3
	private void Update()
	{
		this.delay -= Time.deltaTime;
		if (this.delay <= 0f)
		{
			Application.Quit();
		}
	}

	// Token: 0x040001A7 RID: 423
	private float delay;
}
