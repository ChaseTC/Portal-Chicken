using UnityEngine;
using UnityEngine.U2D;

public class ActiveGrass : MonoBehaviour
{
    [SerializeField] private bool inverted = false;
    [SerializeField] private bool isActive = true;
    [SerializeField] private int frequency = -1;

    private SpriteShapeRenderer sr;
    private Collider2D col;
    private void Start()
    {
        sr = GetComponent<SpriteShapeRenderer>();
        col = GetComponent<Collider2D>();
    }
    
    private void Update()
    {

        if (frequency != -1 && EventManager.isActive(frequency))
        {
            isActive = true;
        }
        else if (frequency != -1)
        {
            isActive = false;
        }
        if (isActive && !inverted)
        {
            SetActive();
        }
        else if (isActive && inverted)
        {
            SetInactive();
        }
        else if (!isActive && !inverted)
        {
            SetInactive();
        }
        else if (!isActive && inverted)
        {
            SetActive();
        }
    }

    private void SetActive()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        col.enabled = true;
    }
    private void SetInactive()
    {
        sr.color = new Color(255, sr.color.g, sr.color.b, 0.3f);
        col.enabled = false;
    }
}
