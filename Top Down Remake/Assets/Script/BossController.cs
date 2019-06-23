using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BossController : MonoBehaviour
{
    [Inject] private GameSettings _gameSettings;
    [Inject] private BossFillingLifeView _bossFillingLifeView;

    private float _life;
    private float _maxLife;

    public bool UpdateBossLife;
    
    void Start()
    {
        _life = 0;
        _maxLife = _gameSettings.BossMaxLife;
        StartCoroutine(timerForBoss());
        UpdateView();
    }

    public void UpdateView()
    {
        _bossFillingLifeView.BossLife.fillAmount = _life / _maxLife;
    }

    public void TurnOnOffBossLife(bool state)
    {
        UpdateBossLife = state;
    }

    public float GetLife()
    {
        return _life;
    }

    public void ModifyLifeView(int amount)
    {
        _life += amount;
    }

    IEnumerator timerForBoss()
    {
        yield return new WaitForSeconds(_gameSettings.bossLifeFillingSpeed);

        if (UpdateBossLife)
        {
            _life += 1f;

            UpdateView();
        }

        if (_life >= _maxLife)
            _life = _maxLife;

        if (_life <= 0)
            _life = 0;

        StartCoroutine(timerForBoss());
    }
}
