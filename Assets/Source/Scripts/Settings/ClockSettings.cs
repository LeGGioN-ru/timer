using UnityEngine;

[CreateAssetMenu(fileName = "Clock Settings", menuName = "Game/Clock/Create Settings", order = 1)]
public class ClockSettings : ScriptableObject
{
    [SerializeField] private int _timeUpdateDelay;

    public int TimeUpdateDelay => _timeUpdateDelay;
}
