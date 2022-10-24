using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoreTxt;

    // Update is called once per frame
    void Update()
    {
        scoreTxt.text = PlayerController.EggCount.ToString();
        
    }
}
