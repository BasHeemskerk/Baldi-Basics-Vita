using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000025 RID: 37
public class AgentTest : MonoBehaviour
{
	// Token: 0x060000AE RID: 174 RVA: 0x0000653A File Offset: 0x0000493A
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>(); // Define the AI Agent
		this.Wander(); //Start wandering
	}

	// Token: 0x060000AF RID: 175 RVA: 0x0000654E File Offset: 0x0000494E
	private void Update()
	{
		if (this.coolDown > 0f)
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x00006578 File Offset: 0x00004978
	private void FixedUpdate()
	{
		Vector3 direction = this.player.position - base.transform.position; 
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position, direction, out raycastHit, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player") //Check if its the player
		{
			this.db = true;
			this.TargetPlayer(); //Head towards the player
		}
		else
		{
			this.db = false;
			if (this.agent.velocity.magnitude <= 1f & this.coolDown <= 0f)
			{
				this.Wander(); //Just wander
			}
		}
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x00006629 File Offset: 0x00004A29
	private void Wander()
	{
		this.wanderer.GetNewTarget(); //Get a new target on the map
		this.agent.SetDestination(this.wanderTarget.position); //Set its destination to position of the wanderTarget
		this.coolDown = 1f;
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00006658 File Offset: 0x00004A58
	private void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position); //Set it's destination to the player
		this.coolDown = 1f;
	}

	// Token: 0x04000125 RID: 293
	public bool db;

	// Token: 0x04000126 RID: 294
	public Transform player;

	// Token: 0x04000127 RID: 295
	public Transform wanderTarget;

	// Token: 0x04000128 RID: 296
	public AILocationSelectorScript wanderer;

	// Token: 0x04000129 RID: 297
	public float coolDown;

	// Token: 0x0400012A RID: 298
	private NavMeshAgent agent;
}
