using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [Inject] private GameSettings _gameSettings;
    [Inject] private PlayerView _playerView;

    public Rigidbody2D body;

    private float horizontal;
    private float vertical;
    private float moveLimiter;
    private float runSpeedHorizontal;
    private float runSpeedVectical;

    private GameManager _gameManager;
    private HealthSystem _healthSystem;

    private Vector3 _initialPosition;

    public void SetupGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    void Start()
    {
        this.gameObject.tag = Tags.Player.ToString();

        _initialPosition = this.transform.position;

        moveLimiter = _gameSettings.PlayerDiagonalSpeedLimiter;
        runSpeedHorizontal = _gameSettings.PlayerRunSpeedHorizontal;
        runSpeedVectical = _gameSettings.PlayerRunSpeedVertical;

        _healthSystem = this.gameObject.AddComponent<HealthSystem>();

        _healthSystem.Setup(_gameSettings.PlayerMaxHealth, _gameSettings);
        //Debug.Log("Player health is now = " + _healthSystem.GetCurrentHealth());
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw(_gameSettings.InputAxisHorizontalName); // -1 is left
        vertical = Input.GetAxisRaw(_gameSettings.InputAxisVerticalName); // -1 is down
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * runSpeedHorizontal, vertical * runSpeedVectical);

        UpdateView();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == Tags.RoomSwitch.ToString())
        {
            StartCoroutine(_gameManager.WaitToSwitch(0.01f));
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Tags.Enemy.ToString())
        {
            TakeDamage(_gameSettings.EnemyToPlayerCollisionDamage);
        }

        if (collision.gameObject.tag == Tags.Enemy.ToString())
        {
            TakeDamage(_gameSettings.BossToPlayerCollisionDamage);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (!_healthSystem.CanTakeDamageAgain)
        {
            _healthSystem.ModifyHealth(damageAmount);

            if (_healthSystem.GetCurrentHealth() <= 0)
            {
                //Game Over Player
                Debug.Log("Can't go lower, guess i'll die");
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            Debug.Log("Invincible, Monsters !");
        }
    }

    public void RePositionPlayer()
    {
        this.transform.position = _initialPosition;
    }

    void UpdateView()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            _playerView.IsEnable(true);
            _playerView.PlayerLifeView.fillAmount = (float)_healthSystem.GetCurrentHealth() / (float)_gameSettings.PlayerMaxHealth;
        }
        else
            _playerView.IsEnable(false);
    }
}
