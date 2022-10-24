using UnityEngine;

public class GameManager : MonoBehaviour
{
        
    public GameObject CompleteLevel;
    
    public void EndGame(){
        
            Debug.Log("GameOver");   
            Invoke("AnimPause", 2f);
        }
        
     public void AnimPause(){
             
             
            CompleteLevel.SetActive(true);
             
             }   
}
