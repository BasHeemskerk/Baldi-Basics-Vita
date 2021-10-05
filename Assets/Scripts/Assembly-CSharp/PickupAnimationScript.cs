using System;
using UnityEngine;

// Token: 0x0200002C RID: 44
public class PickupAnimationScript : MonoBehaviour
{
	// Token: 0x060000DB RID: 219 RVA: 0x000076D0 File Offset: 0x00005AD0
	private void Start()
	{
		this.itemPosition = base.GetComponent<Transform>();
	}

	// Token: 0x060000DC RID: 220 RVA: 0x000076DE File Offset: 0x00005ADE
	private void Update()
	{
		this.itemPosition.localPosition = new Vector3(0f, Mathf.Sin((float)Time.frameCount * 0.0174532924f) / 2f + 1f, 0f);
	}

	// Token: 0x04000184 RID: 388
	private Transform itemPosition;
}
