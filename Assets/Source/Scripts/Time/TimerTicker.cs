using System.Collections;
using UnityEngine;
using Zenject;

public class TimerTicker : IInitializable
{
    private readonly TimeModel _timeModel;
    private readonly SignalBus _signalBus;
    private readonly CoroutineStarter _coroutineStarter;

    private readonly WaitForSeconds _waitForSeconds;

    public TimerTicker(TimeModel timeModel, SignalBus signalBus,CoroutineStarter coroutineStarter)
    {
        _timeModel = timeModel;
        _signalBus = signalBus;
        _coroutineStarter = coroutineStarter;

        _waitForSeconds = new WaitForSeconds(1);
    }

    public void Initialize()
    {
       _coroutineStarter.StartCoroutine(TimeTick());
    }

    public IEnumerator TimeTick()
    {
        while (true)
        {
            yield return _waitForSeconds;
            _timeModel.ChangeTime(_timeModel.Time.AddSeconds(1));
            _signalBus.Fire(new TimeChanged(_timeModel.Time));
        }
    }
}
