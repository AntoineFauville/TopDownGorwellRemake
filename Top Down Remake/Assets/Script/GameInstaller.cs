using Zenject;

public class GameInstaller : MonoInstaller {
    
    [Inject] private GameSettings _gameSettings;

    public override void InstallBindings()
    {
	}
}
