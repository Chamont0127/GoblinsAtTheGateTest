using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region [Variables]
    [SerializeField] private float speed;
    [SerializeField] private int health;
    [SerializeField] private int goldRewardOnDeath;
    [SerializeField] private Vector3 startingPos;
    [SerializeField] private EnemyController enemy;
    [SerializeField] private GameManager GameManager;
    [SerializeField] private GameObject enemyIsHitEffect;

    [SerializeField] private bool enemySoundOnHit;
    [SerializeField] private AudioController audioController;

    [SerializeField] private bool canBeTargeted = true;
    #endregion

    #region [Variable Properties]
    public int Health
    {
        get => health;
        set => health = value;
    }

    public bool CanBeTargeted
    {
        get => canBeTargeted;
        set => canBeTargeted = value;
    }
    #endregion

    // sets starting position, game manager, and enemy
    void Start() 
    {  
        audioController = GameObject.Find("Audio Controller").GetComponent<AudioController>();
        GameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();

        startingPos = transform.position;
        enemy = this.gameObject.GetComponent<EnemyController>();
    }

    //Moves enemy towards castle
    //TODO need to stop the enemy just short of the castle and do one damage per second (invoke? enemyAttack method every second?)
    void Update()
    {
        transform.position += (Vector3.right * speed * Time.deltaTime);
        Vector3 pos = transform.position;
    }

    //damages the enemy and checks to see if enemy has any health left
    public void DamageEnemy(int damage)
    {
        enemy.Health -= damage;

        //Instantiates enemy is hit effect and destroys it after 0.5 seconds
        GameObject enemyIsHitEffectIns = (GameObject)(Instantiate(enemyIsHitEffect, transform.position, transform.rotation));
        Destroy (enemyIsHitEffectIns, 0.5f);

        if(enemy.Health < 1)
        {
            KillEnemy();
        }
    }

    //rewards player for killing enemy and destroys the enemy
    public void KillEnemy()
    {
        GameManager.Gold += goldRewardOnDeath;

        if(enemySoundOnHit)
        {
            audioController.PlayEnemyHitSound();
        }
        //else EnemyAudioController.StopEnemyStepSound();
   
        Destroy(gameObject);
    }

    public void EnemyIsTargeted()
    {
        print("test");
        if(this.name == "Goblin(Clone)")
        {
            canBeTargeted = false;
            print("test2");
        }
        else    
            canBeTargeted = true;
    }
}
