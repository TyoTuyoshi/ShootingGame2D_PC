using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _speed = 5;
    [SerializeField] private uint HP = 30;
    private Rigidbody2D rbody;
    
    //UIのコード
    [SerializeField] private Text _text;
    private void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody2D>();
        _text.text = "HP:" + HP.ToString();
    }

    void Update()
    {
        MoveRange();
        float v_input = Input.GetAxis("Vertical");
        float h_input = Input.GetAxis("Horizontal");
        rbody.AddForce(new Vector2(h_input, v_input) * Time.deltaTime * _speed, ForceMode2D.Impulse);
    }

    void MoveRange()
    {
        Vector2 pos = gameObject.transform.position;
        float xRange = Mathf.Clamp(pos.x, -3.0f, 3.0f);
        float yRange = Mathf.Clamp(pos.y, -4.0f, 6.0f);
        gameObject.transform.position = new Vector2(xRange, yRange);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        HP -= 1;
        if (HP <= 0)
        {
            HP = 0;
        }
        _text.text = "HP:" + HP.ToString();
        Debug.Log("damage!");
    }
}