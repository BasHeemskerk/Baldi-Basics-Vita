using System;
using UnityEngine;

// Token: 0x02000022 RID: 34
public class NotebookScript : MonoBehaviour
{
	// Token: 0x060000A6 RID: 166 RVA: 0x00006259 File Offset: 0x00004659
	private void Start()
	{
		this.up = true;
		//playerCam = GameObject.FindObjectOfType<Camera>();
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00006264 File Offset: 0x00004664
	private void Update()
	{
		if (this.gc.mode == "endless")
		{
			if (this.respawnTime > 0f) //Respawn timer
			{
				if ((base.transform.position - this.player.position).magnitude > 60f)
				{
					this.respawnTime -= Time.deltaTime;
				}
			}
			else if (!this.up) //If the notebook isn't already respawned
			{
				base.transform.position = new Vector3(base.transform.position.x, 4f, base.transform.position.z); //Go Back up into the map
				this.up = true;
				this.audioDevice.Play();
			}
		}
		if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetMouseButtonDown(0)) // If left clicked
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			
			if (Physics.Raycast(ray, out raycastHit) && (raycastHit.transform.tag == "Notebook" & Vector3.Distance(this.player.position, base.transform.position) < this.openingDistance))
			{
				base.transform.position = new Vector3(base.transform.position.x, -20f, base.transform.position.z); // Go down
				this.up = false;
				this.respawnTime = 120f;  //Set respawn time to 2 minutes
				this.gc.CollectNotebook(); //Collect the notebook
				//GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.learningGame); //Create the learning game
				GameObject gameObject = Instantiate(learningGame);
				gameObject.GetComponent<MathGameScript>().gc = this.gc; //Define the MathGameScript's GC
				gameObject.GetComponent<MathGameScript>().baldiScript = this.bsc; // Define the MathGame's BaldiScript
				gameObject.GetComponent<MathGameScript>().playerPosition = this.player.position; //Define the MathGameScript's PlayerPosition
			}

			/*
			if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider == this.trigger & Vector3.Distance(this.player.position, base.transform.position) < this.openingDistance))
			{
				base.transform.position = new Vector3(base.transform.position.x, -20f, base.transform.position.z); // Go down
				this.up = false;
				this.respawnTime = 120f;  //Set respawn time to 2 minutes
				this.gc.CollectNotebook(); //Collect the notebook
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.learningGame); //Create the learning game
				gameObject.GetComponent<MathGameScript>().gc = this.gc; //Define the MathGameScript's GC
				gameObject.GetComponent<MathGameScript>().baldiScript = this.bsc; // Define the MathGame's BaldiScript
				gameObject.GetComponent<MathGameScript>().playerPosition = this.player.position; //Define the MathGameScript's PlayerPosition
			}
			*/
		}
	}

	// Token: 0x0400011A RID: 282
	public float openingDistance;

	// Token: 0x0400011B RID: 283
	public GameControllerScript gc;

	// Token: 0x0400011C RID: 284
	public BaldiScript bsc;

	// Token: 0x0400011D RID: 285
	public float respawnTime;

	// Token: 0x0400011E RID: 286
	public bool up;

	// Token: 0x0400011F RID: 287
	public Transform player;

	// Token: 0x04000120 RID: 288
	public GameObject learningGame;

	// Token: 0x04000121 RID: 289
	public AudioSource audioDevice;

	public CapsuleCollider trigger;

	//public Camera playerCam;
}
