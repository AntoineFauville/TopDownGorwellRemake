using Zenject;

public class GameInstaller : MonoInstaller {
    
    [Inject] private GameSettings _gameSettings;

    public override void InstallBindings()
    {
		Container.BindInterfacesAndSelfTo<PlayerController>().FromComponentInNewPrefab(_gameSettings.PlayerController).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerArchetypeController>().FromComponentInNewPrefab(_gameSettings.PlayerArchetypeController).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<CameraController>().FromComponentInNewPrefab(_gameSettings.Camera).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<BossController>().FromComponentInNewPrefab(_gameSettings.BossManager).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SceneController>().FromComponentInNewPrefab(_gameSettings.SceneController).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SavingController>().FromComponentInNewPrefab(_gameSettings.SavingController).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<BossFillingLifeView>().FromComponentInNewPrefab(_gameSettings.BossFillingLifeView).AsSingle().NonLazy(); 
        Container.BindInterfacesAndSelfTo<PlayerCornerLifeView>().FromComponentInNewPrefab(_gameSettings.PlayerCornerLifeView).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<TimerController>().FromComponentInNewPrefab(_gameSettings.TimerController).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<TimerVillagerView>().FromComponentInNewPrefab(_gameSettings.TimerVillagerView).AsSingle().NonLazy();
        Container.Bind<ProjectileFactory>().AsSingle().NonLazy();
        Container.Bind<EnemyFactory>().AsSingle().NonLazy();
        Container.Bind<TileFactory>().AsSingle().NonLazy();
        Container.Bind<LoadingScreenFactory>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<NavMeshController>().FromComponentInNewPrefab(_gameSettings.NavMeshController).AsSingle().NonLazy();
    }
}
