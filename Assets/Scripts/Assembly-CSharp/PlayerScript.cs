using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// Token: 0x0200002E RID: 46
public class PlayerScript : MonoBehaviour
{
	// Token: 0x060000E1 RID: 225 RVA: 0x00007AC6 File Offset: 0x00005EC6
	private void Start()
	{
        //Yeah your on your own for this one
		this.rb = base.GetComponent<Rigidbody>();
		this.stamina = this.maxStamina;
		this.playerRotation = base.transform.rotation;
		this.mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");

		if (this.mouseSensitivity < 3f)
			this.mouseSensitivity = 3f;
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x00007B04 File Offset: 0x00005F04
	private void Update()
	{
		this.MouseMove();
		this.StaminaCheck();
		this.GuiltCheck();
		if (this.rb.velocity.magnitude > 0f)
		{
			this.gc.LockMouse();
		}
		if (this.jumpRope & (base.transform.position - this.frozenPosition).magnitude >= 1f) // If the player moves, deactivate the jumprope minigame
		{
			this.DeactivateJumpRope();
		}
		if (this.sweepingFailsave > 0f)
		{
			this.sweepingFailsave -= Time.deltaTime;
		}
		else
		{
			this.sweeping = false;
			this.hugging = false;
		}
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x00007BBD File Offset: 0x00005FBD
	private void FixedUpdate()
	{
		this.PlayerMove();
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x00007BC5 File Offset: 0x00005FC5
	private void MouseMove()
	{
		this.playerRotation.eulerAngles = this.playerRotation.eulerAngles + new Vector3(0f, Input.GetAxis("Mouse X") * this.mouseSensitivity, 0f);
		//this.playerRotation.eulerAngles = this.playerRotation.eulerAngles + new Vector3(0f, Input.GetAxis(/*Stick + */"StickH") * this.mouseSensitivity, 0f);
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x00007C00 File Offset: 0x00006000
	private void PlayerMove()
	{
		base.transform.rotation = this.playerRotation;
		Vector3 a = new Vector3(0f, 0f, 0f);
		Vector3 b = new Vector3(0f, 0f, 0f);
		this.db = Input.GetAxisRaw("Forward");
		if (this.stamina > 0f)
		{
			if (Input.GetAxisRaw("Run") > 0f)
			{
				this.playerSpeed = this.runSpeed;
				if (this.rb.velocity.magnitude > 0.1f & !this.hugging & !this.sweeping)
				{
					this.ResetGuilt("running", 0.1f);
				}
			}
			else
			{
				this.playerSpeed = this.walkSpeed;
			}
		}
		else
		{
			this.playerSpeed = this.walkSpeed;
		}
		if (Input.GetAxis("Forward") > 0f)
		{
			a = base.transform.forward;
		}
		else if (Input.GetAxis("Forward") < 0f)
		{
			a = base.transform.forward * -1f;
		}
		if (Input.GetAxis("Strafe") > 0f)
		{
			b = base.transform.right;
		}
		else if (Input.GetAxis("Strafe") < 0f)
		{
			b = base.transform.right * -1f;
		}
		if (!this.jumpRope & !this.sweeping & !this.hugging)
		{
			this.rb.velocity = (a + b).normalized * this.playerSpeed;
		}
		else if (this.sweeping)
		{
			this.rb.velocity = this.gottaSweep.velocity + (a + b).normalized * this.playerSpeed * 0.3f;
		}
		else if (this.hugging)
		{
			this.rb.velocity = this.firstPrize.velocity * 1.2f + (this.firstPrizeTransform.position + new Vector3((float)Mathf.RoundToInt(this.firstPrizeTransform.forward.x), 0f, (float)Mathf.RoundToInt(this.firstPrizeTransform.forward.z)) * 3f - base.transform.position);
		}
		else
		{
			this.rb.velocity = new Vector3(0f, 0f, 0f);
		}
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x00007EE8 File Offset: 0x000062E8
	private void StaminaCheck()
	{
		if (this.rb.velocity.magnitude > 0.1f)
		{
			if (Input.GetAxisRaw("Run") > 0f & this.stamina > 0f)
			{
				this.stamina -= this.staminaRate * Time.deltaTime;
			}
			if (this.stamina < 0f & this.stamina > -5f)
			{
				this.stamina = -5f;
			}
		}
		else if (this.stamina < this.maxStamina)
		{
			this.stamina += this.staminaRate * Time.deltaTime;
		}
		this.staminaBar.value = this.stamina / this.maxStamina * 100f;
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00007FC8 File Offset: 0x000063C8
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.name == "Baldi" & !this.gc.debugMode)
		{
			this.gameOver = true;
		}
		else if (other.transform.name == "Playtime" & !this.jumpRope & this.playtime.playCool <= 0f)
		{
			this.ActivateJumpRope();
		}
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x0000804C File Offset: 0x0000644C
	private void OnTriggerStay(Collider other)
	{
		if (other.transform.name == "Gotta Sweep")
		{
			this.sweeping = true;
			this.sweepingFailsave = 1f;
		}
		else if (other.transform.name == "1st Prize" & this.firstPrize.velocity.magnitude > 5f)
		{
			this.hugging = true;
			this.sweepingFailsave = 1f;
		}
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x000080D4 File Offset: 0x000064D4
	private void OnTriggerExit(Collider other)
	{
		if (other.transform.name == "Office Trigger")
		{
			this.ResetGuilt("escape", this.door.lockTime);
		}
		else if (other.transform.name == "Gotta Sweep")
		{
			this.sweeping = false;
		}
		else if (other.transform.name == "1st Prize")
		{
			this.hugging = false;
		}
	}

	// Token: 0x060000EA RID: 234 RVA: 0x0000815D File Offset: 0x0000655D
	public void ResetGuilt(string type, float amount)
	{
		if (amount >= this.guilt)
		{
			this.guilt = amount;
			this.guiltType = type;
		}
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00008179 File Offset: 0x00006579
	private void GuiltCheck()
	{
		if (this.guilt > 0f)
		{
			this.guilt -= Time.deltaTime;
		}
	}

	// Token: 0x060000EC RID: 236 RVA: 0x0000819D File Offset: 0x0000659D
	public void ActivateJumpRope()
	{
		this.jumpRopeScreen.SetActive(true);
		this.jumpRope = true;
		this.frozenPosition = base.transform.position;
	}

	// Token: 0x060000ED RID: 237 RVA: 0x000081C3 File Offset: 0x000065C3
	public void DeactivateJumpRope()
	{
		this.jumpRopeScreen.SetActive(false);
		this.jumpRope = false;
	}

	// Token: 0x04000187 RID: 391
	public GameControllerScript gc;

	// Token: 0x04000188 RID: 392
	public BaldiScript baldi;

	// Token: 0x04000189 RID: 393
	public DoorScript door;

	// Token: 0x0400018A RID: 394
	public PlaytimeScript playtime;

	// Token: 0x0400018B RID: 395
	public bool gameOver;

	// Token: 0x0400018C RID: 396
	public bool jumpRope;

	// Token: 0x0400018D RID: 397
	public bool sweeping;

	// Token: 0x0400018E RID: 398
	public bool hugging;

	// Token: 0x0400018F RID: 399
	public float sweepingFailsave;

	// Token: 0x04000190 RID: 400
	private Quaternion playerRotation;

	// Token: 0x04000191 RID: 401
	public Vector3 frozenPosition;

	// Token: 0x04000192 RID: 402
	public float mouseSensitivity;

	// Token: 0x04000193 RID: 403
	public float walkSpeed;

	// Token: 0x04000194 RID: 404
	public float runSpeed;

	// Token: 0x04000195 RID: 405
	public float slowSpeed;

	// Token: 0x04000196 RID: 406
	public float maxStamina;

	// Token: 0x04000197 RID: 407
	public float staminaRate;

	// Token: 0x04000198 RID: 408
	public float guilt;

	// Token: 0x04000199 RID: 409
	public float initGuilt;

	// Token: 0x0400019A RID: 410
	private float moveX;

	// Token: 0x0400019B RID: 411
	private float moveZ;

	// Token: 0x0400019C RID: 412
	private float playerSpeed;

	// Token: 0x0400019D RID: 413
	public float stamina;

	// Token: 0x0400019E RID: 414
	public Rigidbody rb;

	// Token: 0x0400019F RID: 415
	public NavMeshAgent gottaSweep;

	// Token: 0x040001A0 RID: 416
	public NavMeshAgent firstPrize;

	// Token: 0x040001A1 RID: 417
	public Transform firstPrizeTransform;

	// Token: 0x040001A2 RID: 418
	public Slider staminaBar;

	// Token: 0x040001A3 RID: 419
	public float db;

	// Token: 0x040001A4 RID: 420
	public string guiltType;

	// Token: 0x040001A5 RID: 421
	public GameObject jumpRopeScreen;
}
