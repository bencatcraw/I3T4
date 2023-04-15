using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas howToPage;
    public Canvas titleScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        titleScreen.enabled = true;
        // hide instructions unless player clicks on the "how to play" button
        howToPage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // when player clicks on the "how to play" button, the instructions page show while the title screen hides
    void showInstrcutions() {
        howToPage.enabled = true;
        titleScreen.enabled = false;
    }

    // when the player clicks on the "back" button, the instructions page hides while the title screen shows
    void hideInstructions() {
        howToPage.enabled = false;
        titleScreen.enabled = true;
    }
}
