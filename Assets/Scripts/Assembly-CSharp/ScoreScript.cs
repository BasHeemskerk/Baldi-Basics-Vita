using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000012 RID: 18
public class ScoreScript : MonoBehaviour
{
	// Token: 0x0600004C RID: 76 RVA: 0x000036A0 File Offset: 0x00001AA0
	private void Start()
	{
		if (PlayerPrefs.GetString("CurrentMode") == "endless")
		{
			this.scoreText.SetActive(true);
			this.text.text = "Score:\n" + PlayerPrefs.GetInt("CurrentBooks") + " Notebooks";
		}
	}

	// Token: 0x0600004D RID: 77 RVA: 0x000036FB File Offset: 0x00001AFB
	private void Update()
	{
	}

	// Token: 0x04000069 RID: 105
	public GameObject scoreText;

	// Token: 0x0400006A RID: 106
	public Text text;
}
