using System;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class EntranceScript : MonoBehaviour
{
	// Token: 0x06000079 RID: 121 RVA: 0x00004250 File Offset: 0x00002650
	public void Lower()
	{
		base.transform.position = base.transform.position - new Vector3(0f, 10f, 0f); //Go down 10 units on the Y axis
		if (this.gc.finaleMode) //If the game is in the final mode(7/7 notebooks)
		{
			this.wall.material = this.map; //Switch this wall's material to the map material
		}
	}

	// Token: 0x0600007A RID: 122 RVA: 0x000042AD File Offset: 0x000026AD
	public void Raise()
	{
		base.transform.position = base.transform.position + new Vector3(0f, 10f, 0f); //Go up 10 units on the Y axis
    }

	// Token: 0x040000A5 RID: 165
	public GameControllerScript gc;

	// Token: 0x040000A6 RID: 166
	public Material map;

	// Token: 0x040000A7 RID: 167
	public MeshRenderer wall;
}
