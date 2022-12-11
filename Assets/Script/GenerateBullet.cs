using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBullet : MonoBehaviour
{
    [SerializeField] private GameObject _obj;

    private float time = 0;

    void Update()
    {
        time += Time.deltaTime;
        if (time > 3)
        {
            int rand = Random.Range(-3, 3);
            GameObject bullet = Instantiate(_obj) as GameObject;
            bullet.transform.position = new Vector2(rand, 5);
            time = 0;
        }
    }
}
