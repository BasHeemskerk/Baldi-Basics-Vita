using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000021 RID: 33
public class MathGameScript : MonoBehaviour
{
	// Token: 0x0600009C RID: 156 RVA: 0x00005654 File Offset: 0x00003A54
	private void Start()
	{
		gc = GameObject.FindObjectOfType<GameControllerScript>();
		baldiScript = GameObject.FindObjectOfType<BaldiScript>();

		this.gc.ActivateLearningGame();
		if (this.gc.notebooks == 1)
		{
			this.QueueAudio(this.bal_intro); //Play the tutorial
			this.QueueAudio(this.bal_howto);
		}
		this.NewProblem();
		if (this.gc.spoopMode)
		{
			this.baldiFeedTransform.position = new Vector3(-1000f, -1000f, 0f); //Hide the Baldi Feed
		}
	}

	// Token: 0x0600009D RID: 157 RVA: 0x000056CC File Offset: 0x00003ACC
	private void Update()
	{
		if (!this.baldiAudio.isPlaying) //If baldi isn't talking
		{
			if (this.audioInQueue > 0 & !this.gc.spoopMode)
			{
				this.PlayQueue(); //Start playing the audio in the audio que
			}
			this.baldiFeed.SetBool("talking", false); //Disable the Baldi talking animation
		}
		else
		{
			this.baldiFeed.SetBool("talking", true); //Enable the Baldi talking animation
		}
		if ((Input.GetKeyDown(KeyCode.JoystickButton0) & this.questionInProgress))
		{
			this.questionInProgress = false;
			this.CheckAnswer();
		}
		if (this.problem > 3)
		{
			this.endDelay -= 1f * Time.unscaledDeltaTime;
			if (this.endDelay <= 0f)
			{
				this.ExitGame(); //Exit the Math Game. NOT the actual game
			}
		}
	}

	// Token: 0x0600009E RID: 158 RVA: 0x000057A8 File Offset: 0x00003BA8
	private void NewProblem()
	{
		//this.playerAnswer.text = string.Empty; //Remove the text in the answer box
		this.problem++;
		if (this.problem <= 3) //If it isn't the third problem
		{
			//this.impossibleMode = false;

			this.QueueAudio(this.bal_problems[this.problem - 1]);
			if ((this.gc.mode == "story" & (this.problem <= 2 || this.gc.notebooks <= 1)) || (this.gc.mode == "endless" & (this.problem <= 2 || this.gc.notebooks != 2)))
			{
				this.num1 = (float)Mathf.RoundToInt(UnityEngine.Random.Range(0f, 9f)); //Pick a random number between 0 and 9
				this.num2 = (float)Mathf.RoundToInt(UnityEngine.Random.Range(0f, 9f)); //Pick a random number between 0 and 9
                this.sign = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f)); // Picks a random sign (addition) or (subtraction)
				this.QueueAudio(this.bal_numbers[Mathf.RoundToInt(this.num1)]); //Makes Baldi say num1
				if (this.sign == 0)
				{
					this.solution = this.num1 + this.num2;
					this.questionText.text = string.Concat(new object[]
					{
						"SOLVE MATH Q",
						this.problem,
						": \n \n",
						this.num1,
						"+",
						this.num2,
						"="
					});
					this.QueueAudio(this.bal_plus);
				}
				else if (this.sign == 1)
				{
					this.solution = this.num1 - this.num2;
					this.questionText.text = string.Concat(new object[]
					{
						"SOLVE MATH Q",
						this.problem,
						": \n \n",
						this.num1,
						"-",
						this.num2,
						"="
					});
					this.QueueAudio(this.bal_minus);
				}
				this.QueueAudio(this.bal_numbers[Mathf.RoundToInt(this.num2)]); //Makes Baldi say num2
                this.QueueAudio(this.bal_equals);
			}
			else
			{
				this.impossibleMode = true; //Turns on the impossible question
				this.num1 = UnityEngine.Random.Range(1f, 9999f);
				this.num2 = UnityEngine.Random.Range(1f, 9999f);
				this.num3 = UnityEngine.Random.Range(1f, 9999f);
				this.sign = Mathf.RoundToInt((float)UnityEngine.Random.Range(0, 1)); //Picks a random sign (?+?x?) or (?-?x?)
				this.QueueAudio(this.bal_screech);
				if (this.sign == 0)
				{
					this.questionText.text = string.Concat(new object[]
					{
						"SOLVE MATH Q",
						this.problem,
						": \n",
						this.num1,
						"+(",
						this.num2,
						"X",
						this.num3,
						"="
					});
					this.QueueAudio(this.bal_plus);
					this.QueueAudio(this.bal_screech);
					this.QueueAudio(this.bal_times);
					this.QueueAudio(this.bal_screech);
				}
				else if (this.sign == 1) //Unused
				{
					this.questionText.text = string.Concat(new object[]
					{
						"SOLVE MATH Q",
						this.problem,
						": \n (",
						this.num1,
						"/",
						this.num2,
						")+",
						this.num3,
						"="
					});
					this.QueueAudio(this.bal_divided);
					this.QueueAudio(this.bal_screech);
					this.QueueAudio(this.bal_plus);
					this.QueueAudio(this.bal_screech);
				}
                //Everything below this is for the other 2 texts which are overlayed ontop of eachother to create the impossible question
				this.num1 = UnityEngine.Random.Range(1f, 9999f);
				this.num2 = UnityEngine.Random.Range(1f, 9999f);
				this.num3 = UnityEngine.Random.Range(1f, 9999f);
				this.sign = Mathf.RoundToInt((float)UnityEngine.Random.Range(0, 1));
				if (this.sign == 0)
				{
					this.questionText2.text = string.Concat(new object[]
					{
						"SOLVE MATH Q",
						this.problem,
						": \n",
						this.num1,
						"+(",
						this.num2,
						"X",
						this.num3,
						"="
					});
				}
				else if (this.sign == 1)
				{
					this.questionText2.text = string.Concat(new object[]
					{
						"SOLVE MATH Q",
						this.problem,
						": \n (",
						this.num1,
						"/",
						this.num2,
						")+",
						this.num3,
						"="
					});
				}
				this.num1 = UnityEngine.Random.Range(1f, 9999f);
				this.num2 = UnityEngine.Random.Range(1f, 9999f);
				this.num3 = UnityEngine.Random.Range(1f, 9999f);
				this.sign = Mathf.RoundToInt((float)UnityEngine.Random.Range(0, 1));
				if (this.sign == 0)
				{
					this.questionText3.text = string.Concat(new object[]
					{
						"SOLVE MATH Q",
						this.problem,
						": \n",
						this.num1,
						"+(",
						this.num2,
						"X",
						this.num3,
						"="
					});
				}
				else if (this.sign == 1)
				{
					this.questionText3.text = string.Concat(new object[]
					{
						"SOLVE MATH Q",
						this.problem,
						": \n (",
						this.num1,
						"/",
						this.num2,
						")+",
						this.num3,
						"="
					});
				}
				this.QueueAudio(this.bal_equals);
			}
			//this.playerAnswer.ActivateInputField();
			this.questionInProgress = true;
		}
		else
		{
			this.endDelay = 5f;
			if (!this.gc.spoopMode)
			{
				this.questionText.text = "WOW! YOU EXIST!";
			}
			else if (this.gc.mode == "endless" & this.problemsWrong <= 0)
			{
				int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
				this.questionText.text = this.endlessHintText[num]; //Say a random quote
			}
			else if (this.gc.mode == "story" & this.problemsWrong >= 3)
			{
				this.questionText.text = "I HEAR MATH THAT BAD";
				this.questionText2.text = string.Empty;
				this.questionText3.text = string.Empty;
				this.baldiScript.Hear(this.playerPosition, 10f);
				this.gc.failedNotebooks++;
			}
			else
			{
				int num2 = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
				this.questionText.text = this.hintText[num2]; //Say a random quote
                this.questionText2.text = string.Empty;
				this.questionText3.text = string.Empty;
			}
		}
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00006010 File Offset: 0x00004410
	public void CheckAnswer()
	{
		if (this.vAnswer.customAnswerText.text == this.solution.ToString() & !this.impossibleMode) //If the answer is correct and it isn't the impossible question
		{
			this.results[this.problem - 1].texture = this.correct; //Set the current question icon to the correct texture
			this.baldiAudio.Stop(); //Stop Baldi from talking
			this.ClearAudioQueue(); //Clear Baldi's voice que
			int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 4f)); 
			this.QueueAudio(this.bal_praises[num]); //Say a random praise
            this.NewProblem(); //Go to the next problem

			vAnswer.Answer = 0;
		}
		else /*if (this.impossibleMode || this.vAnswer.customAnswerText.text != this.solution.ToString())*/
		{
			this.results[this.problem - 1].texture = this.incorrect; //Set the current question icon to the incorrect texture
			this.problemsWrong++;
			//baldiScript.enabled = true;
			if (!this.gc.spoopMode)
			{
				baldiScript.enabled = true;
				this.baldiFeed.SetTrigger("angry"); //Make Baldi angry
				this.gc.ActivateSpoopMode(); //Activate Baldi and the gang and lower the entrances
			}
			if (this.gc.mode == "story")
			{
				if (this.problem == 3)
				{
					this.baldiScript.GetAngry(1f);
				}
				else
				{
					this.baldiScript.GetTempAngry(0.25f);
				}
			}
			else
			{
				this.baldiScript.GetAngry(1f);
			}
			this.baldiAudio.Stop(); //Stops what Baldi is currently saying
			this.ClearAudioQueue(); //Clear Baldi's Audio Que
			this.NewProblem();

			vAnswer.Answer = 0;
		}
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x00006170 File Offset: 0x00004570
	private void QueueAudio(AudioClip sound)
	{
		this.audioQueue[this.audioInQueue] = sound; //Adds sound to the audio que
		this.audioInQueue++;
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x0000618E File Offset: 0x0000458E
	private void PlayQueue()
	{
		this.baldiAudio.PlayOneShot(this.audioQueue[0]); //Play the most recent sound in the audio que
		this.UnqueueAudio();
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x000061AC File Offset: 0x000045AC
	private void UnqueueAudio()
	{
		for (int i = 1; i < this.audioInQueue; i++) //Remove the already played audio from the audio que
		{
			this.audioQueue[i - 1] = this.audioQueue[i];
		}
		this.audioInQueue--;
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x000061F0 File Offset: 0x000045F0
	private void ClearAudioQueue()
	{
		this.audioInQueue = 0;
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x000061FC File Offset: 0x000045FC
	private void ExitGame()
	{
		if (this.problemsWrong <= 0 & this.gc.mode == "endless")
		{
			this.baldiScript.GetAngry(-1f);
		}
		this.gc.DeactivateLearningGame(base.gameObject);
	}

	// Token: 0x040000F3 RID: 243
	public GameControllerScript gc;

	// Token: 0x040000F4 RID: 244
	public BaldiScript baldiScript;

	// Token: 0x040000F5 RID: 245
	public Vector3 playerPosition;

	// Token: 0x040000F6 RID: 246
	public GameObject mathGame;

	// Token: 0x040000F7 RID: 247
	public RawImage[] results = new RawImage[3];

	// Token: 0x040000F8 RID: 248
	public Texture correct;

	// Token: 0x040000F9 RID: 249
	public Texture incorrect;

	// Token: 0x040000FA RID: 250
	//public InputField playerAnswer;

	// Token: 0x040000FB RID: 251
	public Text questionText;

	// Token: 0x040000FC RID: 252
	public Text questionText2;

	// Token: 0x040000FD RID: 253
	public Text questionText3;

	// Token: 0x040000FE RID: 254
	public Animator baldiFeed;

	// Token: 0x040000FF RID: 255
	public Transform baldiFeedTransform;

	// Token: 0x04000100 RID: 256
	public AudioClip bal_plus;

	// Token: 0x04000101 RID: 257
	public AudioClip bal_minus;

	// Token: 0x04000102 RID: 258
	public AudioClip bal_times;

	// Token: 0x04000103 RID: 259
	public AudioClip bal_divided;

	// Token: 0x04000104 RID: 260
	public AudioClip bal_equals;

	// Token: 0x04000105 RID: 261
	public AudioClip bal_howto;

	// Token: 0x04000106 RID: 262
	public AudioClip bal_intro;

	// Token: 0x04000107 RID: 263
	public AudioClip bal_screech;

	// Token: 0x04000108 RID: 264
	public AudioClip[] bal_numbers = new AudioClip[10];

	// Token: 0x04000109 RID: 265
	public AudioClip[] bal_praises = new AudioClip[5];

	// Token: 0x0400010A RID: 266
	public AudioClip[] bal_problems = new AudioClip[3];

	// Token: 0x0400010B RID: 267
	private float endDelay;

	// Token: 0x0400010C RID: 268
	private int problem;

	// Token: 0x0400010D RID: 269
	private int audioInQueue;

	// Token: 0x0400010E RID: 270
	private float num1;

	// Token: 0x0400010F RID: 271
	private float num2;

	// Token: 0x04000110 RID: 272
	private float num3;

	// Token: 0x04000111 RID: 273
	private int sign;

	// Token: 0x04000112 RID: 274
	private float solution;

	// Token: 0x04000113 RID: 275
	private string[] hintText = new string[]
	{
		"I GET ANGRIER FOR EVERY PROBLEM YOU GET WRONG",
		"I HEAR EVERY DOOR YOU OPEN"
	};

	// Token: 0x04000114 RID: 276
	private string[] endlessHintText = new string[]
	{
		"That's more like it...",
		"Keep up the good work or see me after class..."
	};

	// Token: 0x04000115 RID: 277
	private bool questionInProgress;

	// Token: 0x04000116 RID: 278
	private bool impossibleMode = false;

	// Token: 0x04000117 RID: 279
	private int problemsWrong;

	// Token: 0x04000118 RID: 280
	private AudioClip[] audioQueue = new AudioClip[20];

	// Token: 0x04000119 RID: 281
	public AudioSource baldiAudio;

	public vitaAnswer vAnswer;
}
