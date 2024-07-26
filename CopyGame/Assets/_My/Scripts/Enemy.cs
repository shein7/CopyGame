using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    

    public void EnemyOnClick()
    {
        anim.SetTrigger("Hit");
    }
    public void EnemyDie()
    {
        Destroy(gameObject);
        Debug.Log("Die!");
    }
}
