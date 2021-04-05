using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float gameSpeed = 7.0f;
    public float speedIncreaser = 0.001f;

    public GameObject destroyEffect;
    public Color color;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("k");

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<SpriteRenderer>().color = color;
                Instantiate(destroyEffect, hit.collider.transform.position, Quaternion.identity);

            }
        }
    }

    private void FixedUpdate()
    {
        gameSpeed += speedIncreaser;
    }
}
