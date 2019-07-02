using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int _health;
    private int _maxHealth;

    public bool CanTakeDamageAgain;

    private GameSettings _gameSettings;

    public void Setup(int maxHealth, GameSettings gameSettings)
    {
        _maxHealth = maxHealth;
        _health = maxHealth;
        _gameSettings = gameSettings;
    }

    public void ModifyHealth(int amount)
    {
        if (!CanTakeDamageAgain)
        {
            if (amount < 0)
            {
                CanTakeDamageAgain = true;
                StartCoroutine(InvincibilityFrame());
            }

            _health += amount;
        }
    }

    public int GetCurrentHealth()
    {
        return _health;
    }

    IEnumerator Checkers()
    {
        yield return new WaitForSeconds(0.1f);

        if (_health < 0)
            _health = 0;

        if (_health > _maxHealth)
            _health = _maxHealth;
    }

    IEnumerator InvincibilityFrame()
    {
        if(this.GetComponent<SpriteRenderer>() != null)
            this.GetComponent<SpriteRenderer>().color = Color.blue;

        yield return new WaitForSeconds(_gameSettings.InvincibilityFrameTime);

        if (this.GetComponent<SpriteRenderer>() != null)
            this.GetComponent<SpriteRenderer>().color = Color.white;

        CanTakeDamageAgain = false;
    }
}
