using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vitaAnswer : MonoBehaviour {

	public Text customAnswerText;
	public float Answer;

	void Update () {
		if (Input.GetKeyDown(KeyCode.JoystickButton10))
        {
			Answer -= 1;
        }
		else if (Input.GetKeyDown(KeyCode.JoystickButton8))
		{
			Answer += 1;
		}

		customAnswerText.text = Answer.ToString();
	}
}
