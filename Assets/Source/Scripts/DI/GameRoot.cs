using UnityEngine;
using Zenject;

public class GameRoot : MonoInstaller
{
    [SerializeField] private ClockSettings _clockSettings;
    [SerializeField] private CoroutineStarter _coroutineStarter;
    [SerializeField] private TimeChangeButton _timeChangeButton;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<TimeChanged>();

        Container.BindInstance(_timeChangeButton);
        Container.BindInstance(_coroutineStarter);
        Container.BindInstance(_clockSettings);
        Container.BindInstance(new TimeModel());

        Container.BindInterfacesAndSelfTo<AppStarter>().AsSingle();
        Container.BindInterfacesAndSelfTo<ServerTimeTaker>().AsSingle();
        Container.BindInterfacesAndSelfTo<CurrentTimeUpdater>().AsSingle();
        Container.BindInterfacesAndSelfTo<TimerTicker>().AsSingle();
    }
}
