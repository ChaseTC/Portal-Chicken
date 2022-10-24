using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame (){
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
      }
      
         
   public void PlayAgain(){
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
      
   public void LevelOne(){
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +5);
      }
            
   public void LevelTwo(){
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +6);
      }
   
            
   public void LevelThree(){
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +7);
      }
      
   public void QuitGame (){
         Debug.Log("QUIT");
         Application.Quit();
          }
         
   public void ExitToMainMenu(){
         SceneManager.LoadScene(0);
     
         
      }
}
