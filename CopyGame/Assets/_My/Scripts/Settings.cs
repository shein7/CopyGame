using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Settings : MonoBehaviour
{
    private BigInteger attackDMG = 1;
    private BigInteger enemyHP = 3;
    private BigInteger newEnemyHP;

    private BigInteger gold = 0;
    private BigInteger payGold = 1;
    private BigInteger dropGold = 1;

    public int stage = 1;
    public int enemyCount = 6;

    // Start is called before the first frame update
    void Start()
    {
        newEnemyHP = enemyHP;
    }

    public bool IsEnemyDie()
    {
        bool resurt = false;

        enemyHP -= attackDMG;

        if(enemyHP <= 0)
        {
            enemyHP = 0;
            resurt = true;
        }
        return resurt;
    }
    public void InitEnemyHP()
    {
        BigInteger hp = (BigInteger)((float)enemyHP * 1.8f);
        enemyHP = hp;
        newEnemyHP = enemyHP;
    }
    public void GetEnemyHP()
    {
        enemyHP = newEnemyHP;
    }

    public BigInteger GetGold()
    {
        dropGold = BigInteger.Pow(2,stage)/ 2;

        if (dropGold < 1)
            dropGold = 1;

        gold += dropGold;

        return gold;
    }

    public void LvUpPatGold()
    {

        if(gold >= payGold)
        {
            gold -= payGold;
            attackDMG += 1;
            payGold += (BigInteger)((float)payGold * 1.2f);
        }
    }
    public float GetEnemyHPVal()
    {
        float hp = (float)enemyHP / (float)newEnemyHP;
        return hp;
    }
    private string FormatNum(BigInteger num)
    {
        string[] units = { "", "K", "M", "Y", "T" };
        int unitindex = 0;

        while(num > 1000 && unitindex < units.Length - 1)
        {
            num /= 1000;
            unitindex++;
        }
         
        string fNum = string.Format("{0}{1}.", num.ToString(), units[unitindex]);
        return fNum;
    }
    public string stringGold()
    {
        return FormatNum(gold);
    }

    public string stringPayGold()
    {
        return FormatNum(payGold);
    }
    public void SumGold()
    {
        gold *= 999;
    }
}
