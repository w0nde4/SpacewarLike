using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.02f;
    Material backgroundMaterial;
    Vector2 offset;

    private void Start()
    {
        backgroundMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0, scrollSpeed);
    }

    private void Update()
    {
        backgroundMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
