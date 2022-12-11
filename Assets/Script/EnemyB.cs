using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private float t = 0;
    private float cnt = 0;
    private float r = 3;
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        this.gameObject.transform.position =
            new Vector3(2 * Mathf.Cos(r * Mathf.Cos(t) + 2), r * MathF.Sin(Mathf.Sin(t) + 2), r * Mathf.Sin(t));
        cnt++;
        if (cnt>200)
        {
            cnt = 0;
            GameObject inst = Instantiate(this.gameObject) as GameObject;
            inst.transform.position = this.gameObject.transform.position;
        }
    }
}
