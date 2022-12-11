using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefabtwst : MonoBehaviour
{
    public GameObject _obj;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= 36; i+=2)
        {
            GameObject pre = Instantiate(_obj) as GameObject;
            pre.transform.position = new Vector2(i, 0);
            //new Vector2(Mathf.Cos(i * 10 * Mathf.PI / 180), Mathf.Sin(i * 10 * Mathf.PI / 180))*2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
