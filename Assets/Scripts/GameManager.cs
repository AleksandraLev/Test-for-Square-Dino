using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int enemyCount = 0;
    public PlayerManager player;

    private bool enemyCount1 = true;
    private bool enemyCount2 = true;

    private const int EnemyCountForContinue1 = 2;
    private const int EnemyCountForContinue2 = 5;
    private const string SceneName = "Game";

    private void Update()
    {
        CheckEnemyCount();
    }

    private void CheckEnemyCount()
    {
        if (enemyCount == EnemyCountForContinue1 && enemyCount1) // ����� �������� ����� "���", ����� ����� ���������� ������� ������, ����� ����� ������� �� �� ר���� ���������.
        {
            OnEnemiesDefeatedForContinue();
            enemyCount1 = false;
            enemyCount2 = true;
        }
        if (enemyCount == EnemyCountForContinue2 && enemyCount2) // ����� �������� ����� "||", ����� ����� ���������� ������� ������, ����� ����� ������� �� �� ��ר���� ���������.
        {
            OnEnemiesDefeatedForContinue();
            enemyCount1 = true;
            enemyCount2 = false;
        }
        else if (player.finish)
        {
            OnEnemiesDefeatedForRestart();
        }
    }

    private void OnEnemiesDefeatedForContinue()
    {
        player.myContinue = true;
    }

    private void OnEnemiesDefeatedForRestart()
    {
        enemyCount = 0;
        Invoke(nameof(RestartGame), 1f); // �������� ����� ������������ �����
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneName);
    }
}
