using System.Collections;
using UnityEngine;
using Zenject;

public class CurrentTimeUpdater : IInitializable
{
    private readonly CoroutineStarter _coroutineStarter;
    private readonly ClockSettings _clockSettings;
    private readonly ServerTimeTaker _serverTimeTaker;

    private WaitForSeconds _waitSeconds;

    public CurrentTimeUpdater(CoroutineStarter coroutineStarter, ClockSettings clockSettings, ServerTimeTaker serverTimeTaker)
    {
        _coroutineStarter = coroutineStarter;
        _clockSettings = clockSettings;
        _serverTimeTaker = serverTimeTaker;

        _waitSeconds = new WaitForSeconds(_clockSettings.TimeUpdateDelay);
    }

    public void Initialize()
    {
        _coroutineStarter.StartCoroutine(UpdateTimeCoroutine());
    }

    private IEnumerator UpdateTimeCoroutine()
    {
        while (true)
        {
            yield return _waitSeconds;
            _serverTimeTaker.FetchTime();
        }
    }
}
