using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour {
   
	// Help Button click
	public void  clickForHelp () {
	    SceneManager.LoadSceneAsync("HelpScene");
    }

    // Start Button click
	public void clickForStart () { 
	    SceneManager.LoadSceneAsync("GameScene");
    }

    // Return Button click
	public void clickForReturn () { 
	    SceneManager.LoadSceneAsync("StartScene");
    }
}
