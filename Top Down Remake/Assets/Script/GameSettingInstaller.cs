using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Antoine/Game Settings")]
public class GameSettingInstaller : ScriptableObjectInstaller {
    
    [SerializeField] private GameSettings GameSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(GameSettings);
    }
}
