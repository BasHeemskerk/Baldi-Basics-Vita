using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200000A RID: 10
public class DetentionTextScript : MonoBehaviour
{
	// Token: 0x06000024 RID: 36 RVA: 0x00002882 File Offset: 0x00000C82
	private void Start()
	{
		this.text = base.GetComponent<Text>();
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00002890 File Offset: 0x00000C90
	private void Update()
	{
		if (this.door.lockTime > 0f)
		{
			this.text.text = "You have detention! \n" + Mathf.CeilToInt(this.door.lockTime) + " seconds remain!";
		}
		else
		{
			this.text.text = string.Empty;
		}
	}

	// Token: 0x04000022 RID: 34
	public DoorScript door;

	// Token: 0x04000023 RID: 35
	private Text text;
}
