using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame (){
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
      }
      
   public void TutOne (){
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +2);
      }
      
   public void TutTwo (){
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +3);
      }
      
   public void TutThree (){
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +4);
      }
      
      
   public void QuitGame (){
         Debug.Log("QUIT");
         Application.Quit();
          }
         
   public void ExitToMainMenu(){
         SceneManager.LoadScene(0);
     
         
      }
}
