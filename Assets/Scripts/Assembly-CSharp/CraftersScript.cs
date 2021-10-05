using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000028 RID: 40
public class CraftersScript : MonoBehaviour
{
	// Token: 0x060000C3 RID: 195 RVA: 0x00006B69 File Offset: 0x00004F69
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>(); // Defines the nav mesh agent
		this.audioDevice = base.GetComponent<AudioSource>(); //Gets the audio source
		this.sprite.SetActive(false); // Set arts and crafters sprite to be invisible
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x00006B90 File Offset: 0x00004F90
	private void Update()
	{
		if (this.forceShowTime > 0f)
		{
			this.forceShowTime -= Time.deltaTime;
		}
		if (this.gettingAngry) //If arts is getting agry
		{
			this.anger += Time.deltaTime; // Increase anger
			if (this.anger >= 1f & !this.angry) //If anger is greater then 1 and arts isn't angry
			{
				this.angry = true; // Get angry
				this.audioDevice.PlayOneShot(this.aud_Intro); // Do the woooosoh sound
				this.spriteImage.sprite = this.angrySprite; // Switch to the angry sprite
			}
		}
		else if (this.anger > 0f) // If anger is greater then 0, decrease.
		{
			this.anger -= Time.deltaTime;
		}
		if (!this.angry) // If not angry
		{
			if (((base.transform.position - this.agent.destination).magnitude <= 20f & (base.transform.position - this.player.position).magnitude >= 60f) || this.forceShowTime > 0f) //If close to the player and force showtime is less then 0
			{
				this.sprite.SetActive(true); // Become visible
			}
			else
			{
				this.sprite.SetActive(false); // Become invisible
			}
		}
		else
		{
			this.agent.speed = this.agent.speed + 60f * Time.deltaTime; // Increase the speed
			this.TargetPlayer(); // Target the player
			if (!this.audioDevice.isPlaying) //If the sound is not already playing
			{
				this.audioDevice.PlayOneShot(this.aud_Loop); //Play the full wooooosh sound
			}
		}
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x00006D34 File Offset: 0x00005134
	private void FixedUpdate()
	{
		if (this.gc.notebooks >= 7) // If the player has more then 7 notebooks
		{
			Vector3 direction = this.player.position - base.transform.position;
			RaycastHit raycastHit;
			if (Physics.Raycast(base.transform.position + Vector3.up * 2f, direction, out raycastHit, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player" & this.craftersRenderer.isVisible & this.sprite.activeSelf) // If Arts is Visible, and active and sees the player
			{
				this.gettingAngry = true; // Start to get angry
			}
			else
			{
				this.gettingAngry = false; // Stop getting angry
			}
		}
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x00006DE9 File Offset: 0x000051E9
	public void GiveLocation(Vector3 location, bool flee)
	{
		if (!this.angry)
		{
			this.agent.SetDestination(location);
			if (flee)
			{
				this.forceShowTime = 3f; // Make arts appear in 3 seconds
			}
		}
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x00006E14 File Offset: 0x00005214
	private void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position); // Set destination to the player
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x00006E30 File Offset: 0x00005230
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" & this.angry) // If arts is angry and is touching the player
		{
			this.player.position = new Vector3(5f, this.player.position.y, 80f); // Teleport the player to X: 5, their current Y position, Z: 80
			this.baldiAgent.Warp(new Vector3(5f, this.baldi.position.y, 125f)); // Teleport Baldi to X: 5, baldi's Y, Z: 125
			this.player.LookAt(new Vector3(this.baldi.position.x, this.player.position.y, this.baldi.position.z)); // Make the player look at baldi
			this.gc.DespawnCrafters(); // Despawn Arts And Crafters
		}
	}

	// Token: 0x04000149 RID: 329
	public bool db;

	// Token: 0x0400014A RID: 330
	public bool angry;

	// Token: 0x0400014B RID: 331
	public bool gettingAngry;

	// Token: 0x0400014C RID: 332
	public float anger;

	// Token: 0x0400014D RID: 333
	private float forceShowTime;

	// Token: 0x0400014E RID: 334
	public Transform player;

	// Token: 0x0400014F RID: 335
	public Transform playerCamera;

	// Token: 0x04000150 RID: 336
	public Transform baldi;

	// Token: 0x04000151 RID: 337
	public NavMeshAgent baldiAgent;

	// Token: 0x04000152 RID: 338
	public GameObject sprite;

	// Token: 0x04000153 RID: 339
	public GameControllerScript gc;

	// Token: 0x04000154 RID: 340
	private NavMeshAgent agent;

	// Token: 0x04000155 RID: 341
	public Renderer craftersRenderer;

	// Token: 0x04000156 RID: 342
	public SpriteRenderer spriteImage;

	// Token: 0x04000157 RID: 343
	public Sprite angrySprite;

	// Token: 0x04000158 RID: 344
	private AudioSource audioDevice;

	// Token: 0x04000159 RID: 345
	public AudioClip aud_Intro;

	// Token: 0x0400015A RID: 346
	public AudioClip aud_Loop;
}
