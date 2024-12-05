using UnityEngine;

[CreateAssetMenu()]
public class AudioDataSO : ScriptableObject
{
    [field: SerializeField, Space]
    public AudioClip BackgroundMusic { get; private set; }

    
    [field: SerializeField, Space]
    public AudioClip MoveClip { get; private set; }

    [field: SerializeField]
    public AudioClip RotateClip { get; private set; }

    [field: SerializeField]
    public AudioClip DropClip { get; private set; }

    [field: SerializeField]
    public AudioClip ClearLineClip { get; private set; }

    [field: SerializeField]
    public AudioClip GameOverClip { get; private set; }

    [field: SerializeField]
    public AudioClip FailClip { get; private set; }
}

public enum AudioType
{
    BACKGROUND, MOVE, ROTATE, DROP, CLEARLINE, GAMEOVER, FAIL
}
