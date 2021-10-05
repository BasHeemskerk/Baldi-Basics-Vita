using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200000C RID: 12
public class EndlessTextScript : MonoBehaviour
{
	// Token: 0x0600002A RID: 42 RVA: 0x000029CB File Offset: 0x00000DCB
	private void Start()
	{
		this.text.text = "Endless Mode:\nCollect as many notebooks as you can!\nHigh Score:\n " + PlayerPrefs.GetInt("HighBooks") + " Notebooks";
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000029F6 File Offset: 0x00000DF6
	private void Update()
	{
	}

	// Token: 0x04000028 RID: 40
	public Text text;
}
