using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "player" || col.gameObject.tag == "egg")
        {
            // Finish Logic Here
            Debug.Log("Finished Level");
        }
    }
}
