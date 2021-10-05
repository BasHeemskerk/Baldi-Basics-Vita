using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class EndlessNotebookScript : MonoBehaviour
{
	// Token: 0x06000027 RID: 39 RVA: 0x000028FE File Offset: 0x00000CFE
	private void Start()
	{
		this.gc = GameObject.Find("Game Controller").GetComponent<GameControllerScript>(); //Find the game controller object
		this.player = GameObject.Find("Player").GetComponent<Transform>(); //Find the player object
	}

	// Token: 0x06000028 RID: 40 RVA: 0x0000292C File Offset: 0x00000D2C
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetMouseButtonDown(0)) //If left clicked
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			
			if (Physics.Raycast(ray, out raycastHit) && (raycastHit.transform.tag == "Notebook" & Vector3.Distance(this.player.position, base.transform.position) < this.openingDistance)) //If it is a notebook
			{
				base.gameObject.SetActive(false); //Disable the object being clicked
				this.gc.CollectNotebook(); //Collect the notebook
				this.learningGame.SetActive(true); //Activate the learning game
			}
			
			/*
			if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider == this.trigger & Vector3.Distance(this.player.position, base.transform.position) < this.openingDistance)) //If it is a notebook
			{
				base.gameObject.SetActive(false); //Disable the object being clicked
				this.gc.CollectNotebook(); //Collect the notebook
				this.learningGame.SetActive(true); //Activate the learning game
			}
			*/
		}
	}

	// Token: 0x04000024 RID: 36
	public float openingDistance;

	// Token: 0x04000025 RID: 37
	public GameControllerScript gc;

	// Token: 0x04000026 RID: 38
	public Transform player;

	// Token: 0x04000027 RID: 39
	public GameObject learningGame;

	public CapsuleCollider trigger;
}
