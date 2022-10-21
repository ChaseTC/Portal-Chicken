using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    [SerializeField] private int MaxFrequencies = 10;
    [SerializeField] private int InitialEggCount = 3;
    private void Awake()
    {
        PlayerController.EggCount = InitialEggCount;
        EventManager.MaxFrequencies = MaxFrequencies;
    }
}