using UnityEngine;

public class EventManager
{
    public enum ChickenUpdateType
    {
        ChickenDied,
        KillOldest
    }
    public delegate void SignalCamera(Transform t);
    public delegate void ChickenUpdate(EventManager.ChickenUpdateType u, int chickenNumber);

    public static event SignalCamera signalCamera;
    public static event ChickenUpdate chickenUpdate;

    public static void TriggerSignalCamera(Transform t)
    {
        if (signalCamera != null)
        {
            signalCamera(t);
        }
    }

    public static void TriggerChickenUpdate(ChickenUpdateType u, int chickenNumber)
    {
        if (chickenUpdate != null)
        {
            chickenUpdate(u, chickenNumber);
        }
    }
}
