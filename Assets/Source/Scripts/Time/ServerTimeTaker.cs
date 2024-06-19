using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class ServerTimeTaker
{
    private readonly CoroutineStarter _coroutineStarter;
    private readonly SignalBus _signalBus;
    private readonly TimeModel _timeModel;

    public ServerTimeTaker(CoroutineStarter coroutineStarter, SignalBus signalBus, TimeModel timeModel)
    {
        _coroutineStarter = coroutineStarter;
        _signalBus = signalBus;
        _timeModel = timeModel;
    }

    public void FetchTime()
    {
        _coroutineStarter.StartCoroutine(GetTimeFromServer());
    }

    private IEnumerator GetTimeFromServer()
    {
        UnityWebRequest request = UnityWebRequest.Get(AppConstants.ServerURL);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResult = request.downloadHandler.text;
            TimeJson serverTime = JsonUtility.FromJson<TimeJson>(jsonResult);
            DateTime currentTime = serverTime.UtcDateTime;

            _timeModel.ChangeTime(currentTime);
            _signalBus.Fire(new TimeChanged(currentTime));
        }
    }
}
