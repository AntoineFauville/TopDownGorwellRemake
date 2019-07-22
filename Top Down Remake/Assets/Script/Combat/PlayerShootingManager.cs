using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

public class PlayerShootingManager : MonoBehaviour
{
    [Inject] private ProjectileFactory _projectileFactory;
    [Inject] private GameSettings _gameSettings;

    public GameObject top, down, left, right;

    private float _horizontalShootingAxe;
    private float _verticalShootingAxe;

    private bool _canShootAgain;

    private ShootingTemplate _shootingTemplate;

    private ShootingTemplate STTop;
    private ShootingTemplate STDown;
    private ShootingTemplate STLeft;
    private ShootingTemplate STRight;

    public Image ImageCoolDownFeedback;

    void Start()
    {
        _shootingTemplate = _gameSettings.WarriorShooting;
        SetupShootingTemplates();
    }

    void SetupShootingTemplates()
    {
        //create the shooting template
        STTop = Object.Instantiate(_shootingTemplate, top.transform);
        //rotate the shooting template to allign it in the right direction
        STTop.transform.rotation = top.transform.rotation;

        STDown = Object.Instantiate(_shootingTemplate, down.transform);
        STDown.transform.rotation = down.transform.rotation;

        STLeft = Object.Instantiate(_shootingTemplate, left.transform);
        STLeft.transform.rotation = left.transform.rotation;

        STRight = Object.Instantiate(_shootingTemplate, right.transform);
        STRight.transform.rotation = right.transform.rotation;

    }

    void FixedUpdate()
    {
        _horizontalShootingAxe = Input.GetAxisRaw(_gameSettings.InputAxisShootHorizontal); 
        _verticalShootingAxe = Input.GetAxisRaw(_gameSettings.InputAxisShootVertical);

        if (_horizontalShootingAxe < 0)
        {
            CreateAndPush(STTop);
        }
        if(_horizontalShootingAxe > 0)
        {
            CreateAndPush(STDown);
        }
        if(_verticalShootingAxe < 0)
        {
            CreateAndPush(STLeft);
        }
        if(_verticalShootingAxe > 0)
        {
            CreateAndPush(STRight);
        }
    }

    void CreateAndPush(ShootingTemplate shootingTemplate)
    {
        if (!_canShootAgain)
        {
            //spawn projectile on each end of the shooting template
            for (int i = 0; i < shootingTemplate.ProjectileSpawnPoint.Length; i++)
            {
                Projectile projectile = _projectileFactory.CreateProjectile(shootingTemplate.ProjectileSpawnPoint[i].transform);
            }
            _canShootAgain = true;
            StartCoroutine(waitToShootAgain());
        }
    }

    IEnumerator waitToShootAgain()
    {
        ImageCoolDownFeedback.fillAmount = 1;
        yield return new WaitForSeconds(_gameSettings.ProjectileShootingCooldown/5);
        ImageCoolDownFeedback.fillAmount = 0.8f;
        yield return new WaitForSeconds(_gameSettings.ProjectileShootingCooldown / 5);
        ImageCoolDownFeedback.fillAmount = 0.6f;
        yield return new WaitForSeconds(_gameSettings.ProjectileShootingCooldown / 5);
        ImageCoolDownFeedback.fillAmount = 0.4f;
        yield return new WaitForSeconds(_gameSettings.ProjectileShootingCooldown / 5);
        ImageCoolDownFeedback.fillAmount = 0.2f;
        yield return new WaitForSeconds(_gameSettings.ProjectileShootingCooldown / 5);
        ImageCoolDownFeedback.fillAmount = 0;
        _canShootAgain = false;
    }
}
