using System;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class Billboard : MonoBehaviour
{
	// Token: 0x0600005A RID: 90 RVA: 0x00003835 File Offset: 0x00001C35
	private void Start()
	{
		this.m_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}

	// Token: 0x0600005B RID: 91 RVA: 0x0000384C File Offset: 0x00001C4C
	private void LateUpdate()
	{
		base.transform.LookAt(base.transform.position + this.m_Camera.transform.rotation * Vector3.forward, this.m_Camera.transform.rotation * Vector3.up); // Look towards the player
	}

	// Token: 0x04000070 RID: 112
	private Camera m_Camera;
}
