using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScrolle : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    [SerializeField] private float _speed = 1.0f;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        float y = Mathf.Repeat(Time.time * _speed, 1);
        Vector2 offset = new Vector2(0, y);
        _renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
