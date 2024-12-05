using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimerMode : MonoBehaviour
{
    [SerializeField] TMP_Text _timerText;
    [SerializeField] GameModeSO _gameMode;

    int _remainingDuration;

    public event Action OnTimerEnd;

    private void Start()
    {
        _remainingDuration = _gameMode.TimerModeDuration;
        Begin();
    }

    public void Reset()
    {
        StopAllCoroutines();
    }

    void UpdateUI(int seconds)
    {
        _timerText.text = string.Format("{0:D2}:{1:D2}", seconds / 60, seconds % 60);
    }

    void Begin()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (_remainingDuration > 0)
        {
            UpdateUI(_remainingDuration);
            _remainingDuration--;

            yield return new WaitForSeconds(1f);
        }

        UpdateUI(_remainingDuration);
        End();
    }

    private void End()
    {
        OnTimerEnd?.Invoke();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
