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
            //Вызывем метод из скрипта PlayerMove, чтобы персонаж перещёл на следующую платформу. Точнее меняем значение будевой переменной того скрипта, и это вызывает соответствующий скрипт.
            player.continue1 = true;
        }
        else if (enemyCount == 5)
        {
            Invoke("Restart", 3); //Пререзагрузка сцены запускается с задержкой, чтобы это происходило не слишком резко
            enemyCount = 0;
        }
    }

    void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
