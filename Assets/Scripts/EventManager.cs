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
    public delegate void WireSignal(int frequency, Color color);
    public delegate void PortalTeleport(int from, Color color);

    public static event SignalCamera signalCamera;
    public static event ChickenUpdate chickenUpdate;
    public static event WireSignal wireSignal;
    public static event PortalTeleport portalTeleport;

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
    public static void TriggerWireSignal(int frequency, Color color)
    {
        if (wireSignal != null)
        {
            wireSignal(frequency, color);
        }
    }

    public static void TriggerPortalTeleport(int from, Color color)
    {
        if (portalTeleport != null)
        {
            portalTeleport(from, color);
        }
    }
}
