using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x0200001F RID: 31
public class GameControllerScript : MonoBehaviour
{
	// Token: 0x06000080 RID: 128 RVA: 0x0000438C File Offset: 0x0000278C
	public GameControllerScript()
	{
		int[] array = new int[3];
		array[0] = -80;
		array[1] = -40;
		this.itemSelectOffset = array;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00004448 File Offset: 0x00002848
	private void Start()
	{
		this.audioDevice = base.GetComponent<AudioSource>(); //Get the Audio Source
		this.mode = PlayerPrefs.GetString("CurrentMode"); //Get the current mode
		if (this.mode == "endless") //If it is endless mode
		{
			this.baldiScrpt.endless = true; //Set Baldi use his slightly changed endless anger system
		}
		this.schoolMusic.Play(); //Play the school music
		this.LockMouse(); //Prevent the mouse from moving
		this.UpdateNotebookCount(); //Update the notebook count
		this.itemSelected = 0; //Set selection to item slot 0(the first item slot)
		this.gameOverDelay = 0.5f;
	}

	// Token: 0x06000082 RID: 130 RVA: 0x000044BC File Offset: 0x000028BC
	private void Update()
	{
		if (!this.learningActive)
		{
			if (Input.GetButtonDown("Pause"))
			{
				if (!this.gamePaused)
				{
					this.PauseGame();
				}
				else
				{
					this.UnpauseGame();
				}
			}
			if (Input.GetKeyDown(KeyCode.JoystickButton4) & this.gamePaused)
			{
				SceneManager.LoadScene("MainMenu");
			}
			else if (Input.GetKeyDown(KeyCode.JoystickButton5) & this.gamePaused)
			{
				this.UnpauseGame();
			}
			if (!this.gamePaused & Time.timeScale != 1f)
			{
				Time.timeScale = 1f;
			}
			if (Input.GetKeyDown(KeyCode.JoystickButton2))
			{
				this.UseItem();
			}
			if (Input.GetKeyDown(KeyCode.JoystickButton11))
			{
				this.DecreaseItemSelection();
			}
			else if (Input.GetKeyDown(KeyCode.JoystickButton9))
			{
				this.IncreaseItemSelection();
			}
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				this.itemSelected = 0;
				this.UpdateItemSelection();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				this.itemSelected = 1;
				this.UpdateItemSelection();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				this.itemSelected = 2;
				this.UpdateItemSelection();
			}
		}
		else if (Time.timeScale != 0f)
		{
			Time.timeScale = 0f;
		}
		if (this.player.stamina < 0f & !this.warning.activeSelf)
		{
			this.warning.SetActive(true); //Set the warning text to be visible
		}
		else if (this.player.stamina > 0f & this.warning.activeSelf)
		{
			this.warning.SetActive(false); //Set the warning text to be invisible
		}
		if (this.player.gameOver)
		{
			Time.timeScale = 0f; //Pause the game
			this.gameOverDelay -= Time.unscaledDeltaTime;
			this.audioDevice.PlayOneShot(this.aud_buzz); //Play the jumpscare sound
			if (this.gameOverDelay <= 0f)
			{
				if (this.mode == "endless") //If it is in endless
				{
					if (this.notebooks > PlayerPrefs.GetInt("HighBooks")) //If the player achieved a new score
					{
						PlayerPrefs.SetInt("HighBooks", this.notebooks); //Update the high score
						PlayerPrefs.SetInt("HighTime", Mathf.FloorToInt(this.time)); //(Unused) Update the time
						this.highScoreText.SetActive(true); // "WOW KAZOW! THATS A NEW HIGH SCORE!"
					}
					else if (this.notebooks == PlayerPrefs.GetInt("HighBooks") & Mathf.FloorToInt(this.time) > PlayerPrefs.GetInt("HighTime")) //(Unused) If the player has a brand new record for time
					{
						PlayerPrefs.SetInt("HighTime", Mathf.FloorToInt(this.time)); //Update the high time
						this.highScoreText.SetActive(true); // "WOW KAZOW! THATS A NEW HIGH SCORE!"
                    }
					PlayerPrefs.SetInt("CurrentBooks", this.notebooks); //Update the high score
                    PlayerPrefs.SetInt("CurrentTime", Mathf.FloorToInt(this.time)); //(Unused) Update the time
					PlayerPrefs.Save();
				}
				Time.timeScale = 1f; // Unpause the game
				SceneManager.LoadScene("GameOver"); // Go to the game over screen
			}
		}
		if (this.finaleMode && !this.audioDevice.isPlaying && this.exitsReached == 3) //Play the weird sound after getting some exits
        {
			this.audioDevice.clip = this.aud_MachineLoop;
			this.audioDevice.loop = true;
			this.audioDevice.Play();
		}
		this.time += Time.deltaTime;
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00004828 File Offset: 0x00002C28
	private void UpdateNotebookCount()
	{
		if (this.mode == "story")
		{
			this.notebookCount.text = this.notebooks.ToString() + "/7 Notebooks";
		}
		else
		{
			this.notebookCount.text = this.notebooks.ToString() + " Notebooks";
		}
		if (this.notebooks == 7 & this.mode == "story")
		{
			this.ActivateFinaleMode();
		}
	}

	// Token: 0x06000084 RID: 132 RVA: 0x000048C0 File Offset: 0x00002CC0
	public void CollectNotebook()
	{
		this.notebooks++;
		this.UpdateNotebookCount();
		this.time = 0f;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x000048E1 File Offset: 0x00002CE1
	public void LockMouse()
	{
		/*
		if (!this.learningActive)
		{
			this.cursorController.LockCursor(); //Prevent the cursor from moving
			this.mouseLocked = true;
			this.reticle.SetActive(true);
		}
		*/

		this.cursorController.LockCursor(); //Prevent the cursor from moving
		this.mouseLocked = true;
		this.reticle.SetActive(true);
	}

	// Token: 0x06000086 RID: 134 RVA: 0x0000490C File Offset: 0x00002D0C
	public void UnlockMouse()
	{
		this.cursorController.UnlockCursor(); //Allow the cursor to move
		this.mouseLocked = false;
		this.reticle.SetActive(false);
	}

	// Token: 0x06000087 RID: 135 RVA: 0x0000492C File Offset: 0x00002D2C
	private void PauseGame()
	{
		Time.timeScale = 0f;
		this.gamePaused = true;
		this.pauseText.SetActive(true);
		this.baldiNod.SetActive(true);
		this.baldiShake.SetActive(true);
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00004963 File Offset: 0x00002D63
	private void UnpauseGame()
	{
		Time.timeScale = 1f;
		this.gamePaused = false;
		this.pauseText.SetActive(false);
		this.baldiNod.SetActive(false);
		this.baldiShake.SetActive(false);
	}

	// Token: 0x06000089 RID: 137 RVA: 0x0000499C File Offset: 0x00002D9C
	public void ActivateSpoopMode()
	{
		this.spoopMode = true; //Tells the game its time for spooky
		this.entrance_0.Lower(); //Lowers all the exits
		this.entrance_1.Lower();
		this.entrance_2.Lower();
		this.entrance_3.Lower();
		this.baldiTutor.SetActive(false); //Turns off Baldi(The one that you see at the start of the game)
		this.baldi.SetActive(true); //Turns on Baldi
        this.principal.SetActive(true); //Turns on Principal
        this.crafters.SetActive(true); //Turns on Crafters
        this.playtime.SetActive(true); //Turns on Playtime
        this.gottaSweep.SetActive(true); //Turns on Gotta Sweep
        this.bully.SetActive(true); //Turns on Bully
        this.firstPrize.SetActive(true); //Turns on First-Prize
        this.audioDevice.PlayOneShot(this.aud_Hang); //Plays the hang sound
		this.learnMusic.Stop(); //Stop all the music
		this.schoolMusic.Stop();
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00004A63 File Offset: 0x00002E63
	private void ActivateFinaleMode()
	{
		this.finaleMode = true;
		this.entrance_0.Raise(); //Raise all the enterances(make them appear)
		this.entrance_1.Raise();
		this.entrance_2.Raise();
		this.entrance_3.Raise();
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00004A98 File Offset: 0x00002E98
	public void GetAngry(float value) //Make Baldi get angry
	{
		if (!this.spoopMode)
		{
			this.ActivateSpoopMode();
		}
		this.baldiScrpt.GetAngry(value);
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00004AB7 File Offset: 0x00002EB7
	public void ActivateLearningGame()
	{
		this.learningActive = true; 
		this.UnlockMouse(); //Unlock the mouse
		this.tutorBaldi.Stop(); //Make tutor Baldi stop talking
		if (!this.spoopMode) //If the player hasn't gotten a question wrong
		{
			this.schoolMusic.Stop(); //Start playing the learn music
			this.learnMusic.Play();
		}
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00004AF4 File Offset: 0x00002EF4
	public void DeactivateLearningGame(GameObject subject)
	{
		this.learningActive = false; 
		UnityEngine.Object.Destroy(subject);
		this.LockMouse(); //Prevent the mouse from moving
		if (this.player.stamina < 100f) //Reset Stamina
		{
			this.player.stamina = 100f;
		}
		if (!this.spoopMode) //If it isn't spoop mode, play the school music
		{
			this.schoolMusic.Play();
			this.learnMusic.Stop();
		}
		if (this.notebooks == 1 & !this.spoopMode) // If this is the players first notebook and they didn't get any questions wrong, reward them with a quarter
		{
			this.quarter.SetActive(true);
			this.tutorBaldi.PlayOneShot(this.aud_Prize);
		}
		else if (this.notebooks == 7 & this.mode == "story") // Plays the all 7 notebook sound
		{
			this.audioDevice.PlayOneShot(this.aud_AllNotebooks, 0.8f);
		}
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00004BCC File Offset: 0x00002FCC
	private void IncreaseItemSelection()
	{
		this.itemSelected++;
		if (this.itemSelected > 2)
		{
			this.itemSelected = 0;
		}
		this.itemSelect.anchoredPosition = new Vector3((float)this.itemSelectOffset[this.itemSelected], 0f, 0f); //Moves the item selector background(the red rectangle)
		this.UpdateItemName();
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00004C30 File Offset: 0x00003030
	private void DecreaseItemSelection()
	{
		this.itemSelected--;
		if (this.itemSelected < 0)
		{
			this.itemSelected = 2;
		}
		this.itemSelect.anchoredPosition = new Vector3((float)this.itemSelectOffset[this.itemSelected], 0f, 0f); //Moves the item selector background(the red rectangle)
        this.UpdateItemName();
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00004C91 File Offset: 0x00003091
	private void UpdateItemSelection()
	{
		this.itemSelect.anchoredPosition = new Vector3((float)this.itemSelectOffset[this.itemSelected], 0f, 0f); //Moves the item selector background(the red rectangle)
        this.UpdateItemName();
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00004CC8 File Offset: 0x000030C8
	public void CollectItem(int item_ID)
	{
		if (this.item[0] == 0)
		{
			this.item[0] = item_ID; //Set the item slot to the Item_ID provided
			this.itemSlot[0].texture = this.itemTextures[item_ID]; //Set the item slot's texture to a texture in a list of textures based on the Item_ID
		}
		else if (this.item[1] == 0)
		{
			this.item[1] = item_ID; //Set the item slot to the Item_ID provided
            this.itemSlot[1].texture = this.itemTextures[item_ID]; //Set the item slot's texture to a texture in a list of textures based on the Item_ID
        }
		else if (this.item[2] == 0)
		{
			this.item[2] = item_ID; //Set the item slot to the Item_ID provided
            this.itemSlot[2].texture = this.itemTextures[item_ID]; //Set the item slot's texture to a texture in a list of textures based on the Item_ID
        }
		else //This one overwrites the currently selected slot when your inventory is full
		{
			this.item[this.itemSelected] = item_ID;
			this.itemSlot[this.itemSelected].texture = this.itemTextures[item_ID];
		}
		this.UpdateItemName();
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00004D94 File Offset: 0x00003194
	private void UseItem()
	{
		if (this.item[this.itemSelected] != 0) //If the item slot isn't empty
		{
			if (this.item[this.itemSelected] == 1)  //Zesty Bar Code
			{
				this.player.stamina = this.player.maxStamina * 2f;
				this.ResetItem(); //Remove the item
            }
			else if (this.item[this.itemSelected] == 2) //Yellow Door Lock Code
            {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.tag == "SwingingDoor" & Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) <= 10f))
				{
					raycastHit.collider.gameObject.GetComponent<SwingingDoorScript>().LockDoor(15f); //Lock the door for 15 seconds
					this.ResetItem(); //Remove the item
                }
			}
			else if (this.item[this.itemSelected] == 3) //Principal's Keys Code
			{
				Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit2;
				if (Physics.Raycast(ray2, out raycastHit2) && (raycastHit2.collider.tag == "Door" & Vector3.Distance(this.playerTransform.position, raycastHit2.transform.position) <= 10f))
				{
					raycastHit2.collider.gameObject.GetComponent<DoorScript>().UnlockDoor(); //Unlock the door
					raycastHit2.collider.gameObject.GetComponent<DoorScript>().OpenDoor(); //Open the door
					this.ResetItem(); //Remove the item
                }
			}
			else if (this.item[this.itemSelected] == 4) //Bsoda Code
			{
				UnityEngine.Object.Instantiate<GameObject>(this.bsodaSpray, this.playerTransform.position, this.cameraTransform.rotation); //Clone the BSODA Spray object
				this.ResetItem(); //Remove the item
				this.player.ResetGuilt("drink", 1f); // Makes the player guilty for drinking
				this.audioDevice.PlayOneShot(this.aud_Soda); // Play the spray sound
			}
			else if (this.item[this.itemSelected] == 5) //Quarter Code
			{
				Ray ray3 = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit3;
				if (Physics.Raycast(ray3, out raycastHit3))
				{
					if (raycastHit3.collider.name == "BSODAMachine" & Vector3.Distance(this.playerTransform.position, raycastHit3.transform.position) <= 10f)
					{
						this.ResetItem(); //Remove the item 
                        this.CollectItem(4); //Give BSODA
					}
					else if (raycastHit3.collider.name == "ZestyMachine" & Vector3.Distance(this.playerTransform.position, raycastHit3.transform.position) <= 10f)
					{
						this.ResetItem(); //Remove the item
                        this.CollectItem(1); //Give Zesty Bar
					}
					else if (raycastHit3.collider.name == "PayPhone" & Vector3.Distance(this.playerTransform.position, raycastHit3.transform.position) <= 10f)
					{
						raycastHit3.collider.gameObject.GetComponent<TapePlayerScript>().Play(); //Tell the phone to start making the noise
						this.ResetItem(); //Remove the item
                    }
				}
			}
			else if (this.item[this.itemSelected] == 6) // Baldi Anti-hearing Code
			{
				Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit4;
				if (Physics.Raycast(ray4, out raycastHit4) && (raycastHit4.collider.name == "TapePlayer" & Vector3.Distance(this.playerTransform.position, raycastHit4.transform.position) <= 10f))
				{
					raycastHit4.collider.gameObject.GetComponent<TapePlayerScript>().Play(); //Tell the tape player to start making the noise
                    this.ResetItem();
				}
			}
			else if (this.item[this.itemSelected] == 7) // Alarm Clock Code
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.alarmClock, this.playerTransform.position, this.cameraTransform.rotation); //Create a clone of the Alarm Clock
                gameObject.GetComponent<AlarmClockScript>().baldi = this.baldiScrpt; //Set the Alarm Clock's Baldi to the BaldiScript
                this.ResetItem(); //Remove the item
			}
			else if (this.item[this.itemSelected] == 8) // WD No Squee Code
			{
				Ray ray5 = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit5;
				if (Physics.Raycast(ray5, out raycastHit5) && (raycastHit5.collider.tag == "Door" & Vector3.Distance(this.playerTransform.position, raycastHit5.transform.position) <= 10f))
				{
					raycastHit5.collider.gameObject.GetComponent<DoorScript>().SilenceDoor(); // Silences the door
					this.ResetItem(); //Remove the item
					this.audioDevice.PlayOneShot(this.aud_Spray); //Plays the spray sound
				}
			}
			else if (this.item[this.itemSelected] == 9) // Safety Scissors Code
			{
				Ray ray6 = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit6;
				if (this.player.jumpRope)
				{
					this.player.DeactivateJumpRope();
					this.playtimeScript.Disappoint();
					this.ResetItem();
				}
				else if (Physics.Raycast(ray6, out raycastHit6) && raycastHit6.collider.name == "1st Prize")
				{
					this.firstPrizeScript.GoCrazy();
					this.ResetItem();
				}
			}
		}
	}

	// Token: 0x06000093 RID: 147 RVA: 0x000052F4 File Offset: 0x000036F4
	private void ResetItem()
	{
		this.item[this.itemSelected] = 0; //Resets the current item slot
		this.itemSlot[this.itemSelected].texture = this.itemTextures[0]; //Resets the current item slot texture
		this.UpdateItemName();
	}

	// Token: 0x06000094 RID: 148 RVA: 0x00005324 File Offset: 0x00003724
	public void LoseItem(int id)
	{
		this.item[id] = 0; //Resets the item slot
        this.itemSlot[id].texture = this.itemTextures[0]; //Resets the item slot texture
        this.UpdateItemName();
	}

	// Token: 0x06000095 RID: 149 RVA: 0x0000534A File Offset: 0x0000374A
	private void UpdateItemName()
	{
		this.itemText.text = this.itemNames[this.item[this.itemSelected]];
	}

	// Token: 0x06000096 RID: 150 RVA: 0x0000536C File Offset: 0x0000376C
	public void ExitReached()
	{
		this.exitsReached++;
		if (this.exitsReached == 1)
		{
			RenderSettings.ambientLight = Color.red; //Make everything red and start player the weird sound
			RenderSettings.fog = true;
			this.audioDevice.PlayOneShot(this.aud_Switch, 0.8f);
			this.audioDevice.clip = this.aud_MachineQuiet;
			this.audioDevice.loop = true;
			this.audioDevice.Play();
		}
		if (this.exitsReached == 2) //Play a sound
		{
			this.audioDevice.volume = 0.8f;
			this.audioDevice.clip = this.aud_MachineStart;
			this.audioDevice.loop = true;
			this.audioDevice.Play();
		}
		if (this.exitsReached == 3) //Play a even louder sound
		{
			this.audioDevice.clip = this.aud_MachineRev;
			this.audioDevice.loop = false;
			this.audioDevice.Play();
		}
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00005459 File Offset: 0x00003859
	public void DespawnCrafters()
	{
		this.crafters.SetActive(false); //Make Arts And Crafters Inactive
	}

	// Token: 0x040000AB RID: 171
	public CursorControllerScript cursorController;

	// Token: 0x040000AC RID: 172
	public PlayerScript player;

	// Token: 0x040000AD RID: 173
	public Transform playerTransform;

	// Token: 0x040000AE RID: 174
	public Transform cameraTransform;

	// Token: 0x040000AF RID: 175
	public EntranceScript entrance_0;

	// Token: 0x040000B0 RID: 176
	public EntranceScript entrance_1;

	// Token: 0x040000B1 RID: 177
	public EntranceScript entrance_2;

	// Token: 0x040000B2 RID: 178
	public EntranceScript entrance_3;

	// Token: 0x040000B3 RID: 179
	public GameObject baldiTutor;

	// Token: 0x040000B4 RID: 180
	public GameObject baldi;

	// Token: 0x040000B5 RID: 181
	public BaldiScript baldiScrpt;

	// Token: 0x040000B6 RID: 182
	public AudioClip aud_Prize;

	// Token: 0x040000B7 RID: 183
	public AudioClip aud_AllNotebooks;

	// Token: 0x040000B8 RID: 184
	public GameObject principal;

	// Token: 0x040000B9 RID: 185
	public GameObject crafters;

	// Token: 0x040000BA RID: 186
	public GameObject playtime;

	// Token: 0x040000BB RID: 187
	public PlaytimeScript playtimeScript;

	// Token: 0x040000BC RID: 188
	public GameObject gottaSweep;

	// Token: 0x040000BD RID: 189
	public GameObject bully;

	// Token: 0x040000BE RID: 190
	public GameObject firstPrize;

	// Token: 0x040000BF RID: 191
	public FirstPrizeScript firstPrizeScript;

	// Token: 0x040000C0 RID: 192
	public GameObject quarter;

	// Token: 0x040000C1 RID: 193
	public AudioSource tutorBaldi;

	// Token: 0x040000C2 RID: 194
	public string mode;

	// Token: 0x040000C3 RID: 195
	public int notebooks;

	// Token: 0x040000C4 RID: 196
	public GameObject[] notebookPickups;

	// Token: 0x040000C5 RID: 197
	public int failedNotebooks;

	// Token: 0x040000C6 RID: 198
	public float time;

	// Token: 0x040000C7 RID: 199
	public bool spoopMode;

	// Token: 0x040000C8 RID: 200
	public bool finaleMode;

	// Token: 0x040000C9 RID: 201
	public bool debugMode;

	// Token: 0x040000CA RID: 202
	public bool mouseLocked;

	// Token: 0x040000CB RID: 203
	public int exitsReached;

	// Token: 0x040000CC RID: 204
	public int itemSelected;

	// Token: 0x040000CD RID: 205
	public int[] item = new int[3];

	// Token: 0x040000CE RID: 206
	public RawImage[] itemSlot = new RawImage[3];

	// Token: 0x040000CF RID: 207
	private string[] itemNames = new string[]
	{
		"Nothing",
		"Energy flavored Zesty Bar",
		"Yellow Door Lock",
		"Principal's Keys",
		"BSODA",
		"Quarter",
		"Baldi Anti Hearing and Disorienting Tape",
		"Alarm Clock",
		"WD-NoSquee (Door Type)",
		"Safety Scissors"
	};

	// Token: 0x040000D0 RID: 208
	public Text itemText;

	// Token: 0x040000D1 RID: 209
	public UnityEngine.Object[] items = new UnityEngine.Object[10];

	// Token: 0x040000D2 RID: 210
	public Texture[] itemTextures = new Texture[10];

	// Token: 0x040000D3 RID: 211
	public GameObject bsodaSpray;

	// Token: 0x040000D4 RID: 212
	public GameObject alarmClock;

	// Token: 0x040000D5 RID: 213
	public Text notebookCount;

	// Token: 0x040000D6 RID: 214
	public GameObject pauseText;

	// Token: 0x040000D7 RID: 215
	public GameObject highScoreText;

	// Token: 0x040000D8 RID: 216
	public GameObject baldiNod;

	// Token: 0x040000D9 RID: 217
	public GameObject baldiShake;

	// Token: 0x040000DA RID: 218
	public GameObject warning;

	// Token: 0x040000DB RID: 219
	public GameObject reticle;

	// Token: 0x040000DC RID: 220
	public RectTransform itemSelect;

	// Token: 0x040000DD RID: 221
	private int[] itemSelectOffset;

	// Token: 0x040000DE RID: 222
	private bool gamePaused;

	// Token: 0x040000DF RID: 223
	private bool learningActive;

	// Token: 0x040000E0 RID: 224
	private float gameOverDelay;

	// Token: 0x040000E1 RID: 225
	private AudioSource audioDevice;

	// Token: 0x040000E2 RID: 226
	public AudioClip aud_Soda;

	// Token: 0x040000E3 RID: 227
	public AudioClip aud_Spray;

	// Token: 0x040000E4 RID: 228
	public AudioClip aud_buzz;

	// Token: 0x040000E5 RID: 229
	public AudioClip aud_Hang;

	// Token: 0x040000E6 RID: 230
	public AudioClip aud_MachineQuiet;

	// Token: 0x040000E7 RID: 231
	public AudioClip aud_MachineStart;

	// Token: 0x040000E8 RID: 232
	public AudioClip aud_MachineRev;

	// Token: 0x040000E9 RID: 233
	public AudioClip aud_MachineLoop;

	// Token: 0x040000EA RID: 234
	public AudioClip aud_Switch;

	// Token: 0x040000EB RID: 235
	public AudioSource schoolMusic;

	// Token: 0x040000EC RID: 236
	public AudioSource learnMusic;
}
