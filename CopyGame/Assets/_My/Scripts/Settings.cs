using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Settings : MonoBehaviour
{
    private BigInteger attackDMG = 1;
    private BigInteger enemyHP = 3;
    private BigInteger newEnemyHP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool IsEnemyDie()
    {
        bool resurt = false;

        enemyHP -= attackDMG;

        if(enemyHP < 0)
        {
            enemyHP = 0;
            resurt = true;
        }
        return resurt;
    }
    public void InitEnemyHP()
    {
        newEnemyHP = enemyHP;
    }
    public void GetEnemyHP()
    {
        enemyHP = newEnemyHP;
    }
}
