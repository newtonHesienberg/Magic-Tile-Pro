using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public Transform[] childs;

    private float tileSpeed;
    private int childCount;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        childCount = transform.childCount;

        SetTheChildArray();
        SelectARandomTile();
    }

    public void SelectARandomTile()
    {
        int index1 = UnityEngine.Random.Range(0, childCount);
        int index2 = UnityEngine.Random.Range(0, childCount);

        while(index1 == index2)
        {
            index2 = UnityEngine.Random.Range(0, childCount);
        }

        for (int i = 0; i < childCount; i++)
        {
            if (i == index1 || i == index2)
                childs[i].gameObject.SetActive(true);
            else
                childs[i].gameObject.SetActive(false);
        }
    }

    private void SetTheChildArray()
    {
        for (int i = 0; i < childCount; i++)
        {
            childs[i] = transform.GetChild(i);
        }
    }

    private void FixedUpdate()
    {
        MoveDownward();       
    }

    private void MoveDownward()
    {
        tileSpeed = GameManager.Instance.gameSpeed;
        rb.velocity = new Vector2(0.0f, tileSpeed * Time.deltaTime);
    }
}
