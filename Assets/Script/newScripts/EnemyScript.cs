using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyScript : MonoBehaviour
{
    //敵のコード
    //---------------------------------------------------------------------//
    //  敵を実装しています．
    //---------------------------------------------------------------------//
    private GameObject enemy;
    private Vector2 nowPos;
    private int ramble;

    private Animator _animator;
    private float time = 0;
    private float Theta = 0;

    [SerializeField, Range(0, 10)] private int lifePoint = 10;

    private AudioSource _audioSource;
    private TranceformCollision trCollision;
    void Start()
    {
        ramble = Random.Range(0, 36);
        enemy = this.gameObject;
        Theta += ramble * 10;

        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        trCollision = new TranceformCollision();
    }
   
    void Update()
    {
        nowPos = enemy.transform.position;
        enemy.transform.position = new Vector2(MathF.Sin(Theta / 180 * Mathf.PI)*2 , nowPos.y -= 0.01f);
        Theta += 0.05f;
        Theta %= 360;

        if (trCollision.CollisionEnterDetection(enemy, "Player", 0.5f, 0.5f))
        {
            _animator.SetTrigger("hitTrigger");
            lifePoint--;
        }
        else if (lifePoint <= 0)
        {
            lifePoint = 0;
            //_audioSource.Play();
            this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
            _audioSource.PlayOneShot(_audioSource.clip);
            Destroy(enemy, 0.75f);
        }
    }
}
