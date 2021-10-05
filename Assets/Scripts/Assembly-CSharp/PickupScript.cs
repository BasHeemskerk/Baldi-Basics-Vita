using System;
using UnityEngine;

// Token: 0x0200002D RID: 45
public class PickupScript : MonoBehaviour
{
	// Token: 0x060000DE RID: 222 RVA: 0x0000771F File Offset: 0x00005B1F
	private void Start()
	{
	}

	// Token: 0x060000DF RID: 223 RVA: 0x00007724 File Offset: 0x00005B24
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.JoystickButton0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit))
			{
				if (raycastHit.transform.name == "Pickup_EnergyFlavoredZestyBar" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(1);
				}
				else if (raycastHit.transform.name == "Pickup_YellowDoorLock" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(2);
				}
				else if (raycastHit.transform.name == "Pickup_Key" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(3);
				}
				else if (raycastHit.transform.name == "Pickup_BSODA" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(4);
				}
				else if (raycastHit.transform.name == "Pickup_Quarter" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(5);
				}
				else if (raycastHit.transform.name == "Pickup_Tape" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(6);
				}
				else if (raycastHit.transform.name == "Pickup_AlarmClock" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(7);
				}
				else if (raycastHit.transform.name == "Pickup_WD-3D" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(8);
				}
				else if (raycastHit.transform.name == "Pickup_SafetyScissors" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(9);
				}
			}
		}
	}

	// Token: 0x04000185 RID: 389
	public GameControllerScript gc;

	// Token: 0x04000186 RID: 390
	public Transform player;
}
