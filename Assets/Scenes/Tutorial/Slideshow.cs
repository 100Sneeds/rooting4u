using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
 
public class Slideshow : MonoBehaviour
{
    
    public Texture[] imageArray; 
    public int currentImage;
        
    float deltaTime = 0.0f;
    
// added ergonomic functionality, 
// escape key to exit, 
// p key or right mouse to pause the timer1
// left mouse or spacebar to skip to next slide
    
    void OnGUI()
    {
        
        int w = Screen.width, h = Screen.height;
        
        Rect imageRect = new Rect(0, 0, Screen.width, Screen.height);
        
        //dont need to make button transparent but would be cool to know how to.
        //Rect buttonRect = new Rect(0, Screen.height - Screen.height / 10, Screen.width, Screen.height / 10);
        
        //GUI.Label(imageRect, imageArray[currentImage]);
        //Draw texture seems more elegant
        GUI.DrawTexture(imageRect, imageArray[currentImage]);
    
        //if(GUI.Button(buttonRect, "Next"))
        //currentImage++;
        

    }
 
    // Start is called before the first frame update
    void Start()
    {
        currentImage = 0;
     }
 
    // Update is called once per frame
    void Update()
    {
        //Cursor.visible= false;
        //Screen.lockCursor = true;
                
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            UnityEngine.Debug.Log("Pressed primary button.");
            currentImage++;

            if(currentImage >= imageArray.Length){
                currentImage--;
                SceneManager.LoadScene("MatchScene");
            }  
        }            
    }
}