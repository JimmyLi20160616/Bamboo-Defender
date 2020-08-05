using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int numOfEnemy = 2;
    private int countOfCollision = 0;
    private GameObject firstE;

    private void Start()
    {
        numOfEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void GetResultOfEnemyFight(GameObject E)
    {
        if (firstE == null)
        {
            firstE = E;
        }
        else
        {
            int h1 = firstE.GetComponent<EnemyController>().GetHealth();
            int h2 = E.GetComponent<EnemyController>().GetHealth();
            if (h1 > h2)
            {
                firstE.GetComponent<EnemyController>().TakeDamage(h2);
                Destroy(E);
                KillEnemy(1);
            }
            else if (h2 > h1)
            {
                E.GetComponent<EnemyController>().TakeDamage(h2);
                Destroy(firstE);
                KillEnemy(1);
            }
            else
            {
                Destroy(E);
                Destroy(firstE);
                KillEnemy(2);
            }
        }
    }


    void KillEnemy(int n)   // this function will be called every time an enemy was killed
    {
        numOfEnemy -= n;
        CheckWin();
    }

    void CheckWin()
    {
        if (numOfEnemy == 0)
        {
            Debug.Log("Win");
        }
    }

    public void Lose()
    {
        Debug.Log("Lose");
    }
}
