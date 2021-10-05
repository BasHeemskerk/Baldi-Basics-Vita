using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200001D RID: 29
public class ExitButtonScript : MonoBehaviour
{
	// Token: 0x0600007C RID: 124 RVA: 0x000042E6 File Offset: 0x000026E6
	private void Start()
	{
		this.cursorController.UnlockCursor(); //Unlock the cursor
		this.button = base.GetComponent<Button>(); //Get the button component
		this.button.onClick.AddListener(new UnityAction(this.ExitGame)); //When you click the button, exit the game
	}

	// Token: 0x0600007D RID: 125 RVA: 0x0000431B File Offset: 0x0000271B
	private void ExitGame()
	{
		Application.Quit();
	}

	// Token: 0x040000A8 RID: 168
	public CursorControllerScript cursorController;

	// Token: 0x040000A9 RID: 169
	private Button button;
}
