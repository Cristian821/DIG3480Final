using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{

    private float scrollSpeed;
    public float tileSizeZ;
    [SerializeField] private GameObject speedModObject;
    private GameController speedMod;

    private Vector3 startPosition;

    void Start()
    {
        scrollSpeed = -1;
        startPosition = transform.position;
        speedMod = speedModObject.GetComponent<GameController>();
    }

    void Update()
    {
        float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.forward * newPosition;
        if (speedMod.CurrentScore >= 200)
        {
            scrollSpeed = -50;
        }
    }

    
}
