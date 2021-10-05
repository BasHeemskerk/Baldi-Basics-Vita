using System;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class ClickableTest : MonoBehaviour
{
	// Token: 0x06000064 RID: 100 RVA: 0x00003C15 File Offset: 0x00002015
	private void Start()
	{
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00003C18 File Offset: 0x00002018
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.JoystickButton0)) //Left click (cross)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit) && raycastHit.transform.name == "MathNotebook") // If you are looking at a notebook
			{
				base.gameObject.SetActive(false); //Disable the notebook
			}
		}
	}
}
