using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleButtonManager : MonoBehaviour {

	public void buttonPushed(int number){
		switch(number){
			case 1:
				Debug.Log("case 1");
				SceneManager.LoadScene("Start");
				break;
			case 2:
				Debug.Log("case 2");
				SceneManager.LoadScene("HowTo");
				break;
			default:
				break;
		}
	}
}
