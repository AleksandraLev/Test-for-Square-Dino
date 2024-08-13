using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int enemyCount = 0;
    public PlayerManager player;

    void Update()
    {
        if (enemyCount == 2) 
        {
            //������� ����� �� ������� PlayerMove, ����� �������� ������� �� ��������� ���������. ������ ������ �������� ������� ���������� ���� �������, � ��� �������� ��������������� ������.
            player.continue1 = true;
        }
        else if (enemyCount == 5)
        {
            Invoke("Restart", 3); //������������� ����� ����������� � ���������, ����� ��� ����������� �� ������� �����
            enemyCount = 0;
        }
    }

    void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
