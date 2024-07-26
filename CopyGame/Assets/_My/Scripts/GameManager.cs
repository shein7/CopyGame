using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    [Header ("Player")]
    public GameObject player;
    private Animator playerAnim;

    [Header ("Audio")]
    public AudioClip attackClip;
    public AudioClip hitClip;
    private AudioSource audioSource;

    [Header("Enemy")]
    public GameObject enemyObj;
    public GameObject enemySpawn;
    private bool isSpawn = true;

    private bool isAttack = false;
    private float attackTime = 0f;
    private float hitTime = 0.4f;

    private Settings setting;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = player.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        setting = GetComponent<Settings>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemySpawn();
        MouseOnClick();
    }
    private bool IsAttackChk()

    {
        if (isAttack)
        {
            attackTime += Time.deltaTime;

            if (attackTime > hitTime)
            {
                attackTime = 0f;
                isAttack = false;
            }
        }
        return isAttack;
    }
    private void EnemyAttack(RaycastHit2D hit)
    {
        Enemy enemy = hit.collider.GetComponent<Enemy>();
        AudioSource enemyAudio = hit.collider.GetComponent<AudioSource>();

        if (enemy != null)
        {
            enemy.EnemyOnClick();
            enemyAudio.clip = hitClip;
            enemyAudio.volume = 0.5f;
            enemyAudio.Play();

            playerAnim.SetTrigger("Attack");
            audioSource.clip = attackClip;
            audioSource.Play();

            isAttack = true;

            if (setting.IsEnemyDie())
            {
                enemy.EnemyDie();
                setting.GetEnemyHP();
                isSpawn = true;
            }
        }
    }
    private void MouseOnClick()
    {
        if (IsAttackChk())
            return;
        
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if(hit.collider != null&&hit.collider.tag == "Enemy")
            {
               EnemyAttack(hit);
            }
        }
    }
    private void EnemySpawn()
    {
        if (isSpawn)
        {
            StartCoroutine(EnemySpawnTime());
            isSpawn = false;
        }
    }
    IEnumerator EnemySpawnTime()
    {
        yield return new WaitForSeconds(1f);

        setting.InitEnemyHP();
        Instantiate(enemyObj,enemySpawn.transform.position,Quaternion.identity);
    }
}
