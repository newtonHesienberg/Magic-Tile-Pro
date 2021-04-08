using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float gameSpeed = 7.0f;
    public float speedIncreaser = 0.001f;

    [Header("Slider")]
    public Vector3 targetSliderScale;
    public Vector3 targetEffectScale;
    public float sliderSpeed = 2.0f;

    [Header("Tile")]
    public GameObject destroyEffect;
    public Color color;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Instance = this;
    }

    private void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);

            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Tiles"))
                        NormalTileDestroy(hit);
                }
            }

            if(Input.touchCount > 0)
            {
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Slider"))
                        Sliding(hit);
                }
            }
        }

       /* if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Tiles"))
                    NormalTileDestroy(hit);
            }
        }*/

        /*if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Slider"))
                    Sliding(hit);
            }
        }*/
    }

    private void Sliding(RaycastHit2D hit)
    {
        hit.collider.transform.GetChild(0).localScale = Vector3.Lerp(hit.collider.transform.GetChild(0).localScale, targetSliderScale, Time.deltaTime * sliderSpeed);
        hit.collider.transform.GetChild(3).localScale = Vector3.Lerp(hit.collider.transform.GetChild(3).localScale, targetEffectScale, Time.deltaTime * sliderSpeed);

        if (hit.collider.transform.GetChild(3).localScale.y > 0.85f)
        {
            Destroy(hit.collider.transform.GetChild(0).gameObject);
            Destroy(hit.collider.transform.GetChild(1).gameObject);
            Destroy(hit.collider.transform.GetChild(2).gameObject);
            Destroy(hit.collider.transform.GetChild(3).gameObject);

            hit.collider.gameObject.GetComponent<SpriteRenderer>().color = color;
            Instantiate(destroyEffect, hit.collider.transform.position, Quaternion.identity);
        }
    }
    private void NormalTileDestroy(RaycastHit2D hit)
    {
        Vibrate();
        hit.collider.gameObject.GetComponent<SpriteRenderer>().color = color;
        Instantiate(destroyEffect, hit.collider.transform.position, Quaternion.identity);
    }

    public void Vibrate()
    {
        Vibration.Vibrate(40);
    }

    private void FixedUpdate()
    {
        gameSpeed += speedIncreaser;
    }
}
