using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private AudioSource collectCrackSoundEffect;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "player" || col.gameObject.tag == "egg")
        {
            collectCrackSoundEffect.Play();
            PlayerController.EggCount += 1;
            Destroy(gameObject);
        }
    }
}
