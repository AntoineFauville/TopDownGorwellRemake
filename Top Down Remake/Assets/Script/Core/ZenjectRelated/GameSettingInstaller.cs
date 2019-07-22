using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Antoine/Game Settings")]
public class GameSettingInstaller : ScriptableObjectInstaller {
    
    [SerializeField] private GameSettings GameSettings;
    [SerializeField] private DebugSettings DebugSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(GameSettings);
        Container.BindInstance(DebugSettings);
    }
}
