using Zenject;

public class AppStarter : IInitializable
{
    private readonly ServerTimeTaker _serverTimeTaker;

    public AppStarter(ServerTimeTaker serverTimeTaker)
    {
        _serverTimeTaker = serverTimeTaker;
    }

    public void Initialize()
    {
        _serverTimeTaker.FetchTime();
    }
}
