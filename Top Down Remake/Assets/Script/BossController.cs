using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BossController : MonoBehaviour
{
    [Inject] private GameSettings _gameSettings;
    [Inject] private BossFillingLifeView _bossFillingLifeView;

    [SerializeField] private float time;

    public bool UpdateBossLife;
    
    void Start()
    {
        time = 0;
        StartCoroutine(timerForBoss());
    }

    void UpdateView()
    {
        _bossFillingLifeView.BossLife.fillAmount = time;
    }

    public void TurnOnOffBossLife(bool state)
    {
        UpdateBossLife = state;
    }

    IEnumerator timerForBoss()
    {
        yield return new WaitForSeconds(0.1f);

        if (UpdateBossLife)
        {
            time += _gameSettings.bossLifeFillingSpeed;

            UpdateView();
        }

        if (time >= 1)
            time = 1;

        if (time <= 0)
            time = 0;

        StartCoroutine(timerForBoss());
    }
}
