using UnityEngine;

[CreateAssetMenu()]
public class GameModeSO : ScriptableObject
{
    [SerializeField] float[] _classicSpeedList;
    [SerializeField] float[] _fastSpeedList;

    [Tooltip("Timer Mode duration in seconds.")]
    [SerializeField] int _timerModeDuration;

    public float[] ClassicSpeedList => _classicSpeedList;
    public float[] FastSpeedList => _fastSpeedList;
    public int TimerModeDuration => _timerModeDuration;
}