using System.Collections.Generic;
using System.Linq;
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
    public delegate void PortalTeleport(int from, Color color);

    public static int MaxFrequencies {get; set;} = 10;

    private static List<Color>[] ActiveFrequencies = new List<Color>[MaxFrequencies];

    public static event SignalCamera signalCamera;
    public static event ChickenUpdate chickenUpdate;
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

    public static void TriggerPortalTeleport(int from, Color color)
    {
        if (portalTeleport != null)
        {
            portalTeleport(from, color);
        }
    }

    public static void Activate(int frequency, Color c)
    {
        if (ActiveFrequencies[frequency] == null)
        {
            ActiveFrequencies[frequency] = new List<Color>();
        }
        ActiveFrequencies[frequency].Add(c);
    }

    public static void Deactivate(int frequency, Color c)
    {
        ActiveFrequencies[frequency].Remove(c);
    }

    public static bool isActive(int frequency)
    {
        if (ActiveFrequencies[frequency] == null)
        {
            return false;
        }
        return ActiveFrequencies[frequency].Count > 0;
    }

    public static Color GetColor(int frequency)
    {
        return ActiveFrequencies[frequency].Last();
    }

    public static void ResetActive()
    {
        ActiveFrequencies = new List<Color>[MaxFrequencies];
    }
}
