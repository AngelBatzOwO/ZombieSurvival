using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;


public class ScoreUpdate: MonoBehaviour
{
    private static int score = 0; // Staic variable to tract thre score 
    private TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        // Fiond the scoretexrt  object in the scene 
        GameObject scoreTextObject = GameObject.Find("ScoreText");

        // Ensure the scoretext object exisits and has a textmeshprougi component 
        if (scoreTextObject != null)
        {
            scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("ScoreUpdate: Cannot find an object named scoretext with a textmeshproUGUI component");
        }
    }
    private void OnDestroy()
    {
            // Increment the scvore when this game object is destoryed 
            score++;

            //Update the score text if the reference exists 
            if(scoreText != null)
            {
                scoreText.text = "Score" + score;
            }
    }
    

}
