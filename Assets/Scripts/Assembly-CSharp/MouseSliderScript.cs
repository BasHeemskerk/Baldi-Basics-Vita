using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000023 RID: 35
public class MouseSliderScript : MonoBehaviour
{
	// Token: 0x060000A9 RID: 169 RVA: 0x0000644C File Offset: 0x0000484C
	private void Start()
	{
		this.slider = base.GetComponent<Slider>();
		if (PlayerPrefs.GetFloat("MouseSensitivity") == 0f)
		{
			PlayerPrefs.SetFloat("MouseSensitvity", 1f);
		}
		this.slider.value = PlayerPrefs.GetFloat("MouseSensitivity");
	}

	// Token: 0x060000AA RID: 170 RVA: 0x0000649D File Offset: 0x0000489D
	private void Update()
	{
		PlayerPrefs.SetFloat("MouseSensitivity", this.slider.value);
		PlayerPrefs.Save();
	}

	// Token: 0x04000122 RID: 290
	public Slider slider;
}
