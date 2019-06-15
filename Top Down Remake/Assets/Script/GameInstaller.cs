using Zenject;

public class GameInstaller : MonoInstaller {
    
    [Inject] private GameSettings _gameSettings;

    public override void InstallBindings()
    {
		Container.BindInterfacesAndSelfTo<PlayerController>().FromComponentInNewPrefab(_gameSettings.PlayerController).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<CameraController>().FromComponentInNewPrefab(_gameSettings.Camera).AsSingle().NonLazy();
        Container.Bind<ProjectileFactory>().AsSingle().NonLazy();
        Container.Bind<EnemyFactory>().AsSingle().NonLazy();
        Container.Bind<TileFactory>().AsSingle().NonLazy();
    }
}
