using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
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
    [SerializeField] private GameObject damage; 
    //private AudioSource _audioSource;
    private TranceformCollision trCollision;
    void Start()
    {
        ramble = Random.Range(0, 36);
        enemy = this.gameObject;
        Theta += ramble * 10;

        //_audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        trCollision = new TranceformCollision();
        nowPos = enemy.transform.position;
    }
   
    void Update()
    {
        enemy.transform.position = new Vector2(MathF.Sin(Theta / 180 * Mathf.PI)*2 , nowPos.y -= 0.01f);
        Theta += 0.05f;
        Theta %= 360;
        nowPos = enemy.transform.position;
        if (trCollision.CollisionEnterDetection(enemy, "Player", 0.5f, 0.5f))
        {
            _animator.SetTrigger("hitTrigger");
            GameObject damageText = Instantiate(damage) as GameObject;
            TextMeshPro text = damageText.GetComponent<TextMeshPro>();
            damageText.GetComponent<RectTransform>().position = new Vector3(nowPos.x, nowPos.y - 1.5f, -3);
            text.text = Random.Range(99, 150).ToString();
            Destroy(text, 0.2f);
            lifePoint--;
        }
        else if (lifePoint <= 0)
        {
            lifePoint = 0;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
            //_audioSource.PlayOneShot(_audioSource.clip);
            Destroy(enemy, 0.75f);
        }
    }
}
