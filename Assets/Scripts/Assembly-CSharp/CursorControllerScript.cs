using System;
using UnityEngine;

// Token: 0x02000008 RID: 8
public class CursorControllerScript : MonoBehaviour
{
	// Token: 0x0600001C RID: 28 RVA: 0x000027E2 File Offset: 0x00000BE2
	private void Start()
	{
		//Cursor.lockState = CursorLockMode.Locked;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x000027E4 File Offset: 0x00000BE4
	private void Update()
	{
	}

	// Token: 0x0600001E RID: 30 RVA: 0x000027E6 File Offset: 0x00000BE6
	public void LockCursor()
	{
		Cursor.lockState = CursorLockMode.Locked; // Lock the cursor(prevent it from moving)
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000027F4 File Offset: 0x00000BF4
	public void UnlockCursor()
	{
		Cursor.lockState = CursorLockMode.None; // Unlock the cursor(allow it to move)
		Cursor.visible = true;

		//LockCursor();
	}
}
