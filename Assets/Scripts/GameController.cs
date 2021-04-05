using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float minumumYValue;

    public Transform[] tileGroups;

    public Transform topGroup;
    public Transform bottomGroup;

    private int bottomTileGroupIndex = 4;

    private void Start()
    {
        topGroup = tileGroups[0];
        bottomGroup = tileGroups[bottomTileGroupIndex];
    }

    private void Update()
    {
        if (bottomGroup.position.y <= minumumYValue)
        {
            ResetGroup(bottomGroup);
            PickRandomTile(bottomGroup);

            bottomGroup.position = topGroup.position;
            bottomGroup.position += new Vector3(0, 3.1f, 0);
            topGroup = bottomGroup;
            bottomTileGroupIndex--;

            if (bottomTileGroupIndex < 0)
                bottomTileGroupIndex = tileGroups.Length - 1;

            bottomGroup = tileGroups[bottomTileGroupIndex];
        }
    }

    private void PickRandomTile(Transform group)
    {
        int index1 = UnityEngine.Random.Range(0, group.childCount);
        int index2 = UnityEngine.Random.Range(0, group.childCount);

        while(index1 == index2)
        {
             index2 = UnityEngine.Random.Range(0, group.childCount);
        }

        for (int i = 0; i < group.childCount; i++)
        {
            Transform child = group.GetChild(i);

            if (i == index1 || i == index2)
                child.gameObject.SetActive(true);
            else
                child.gameObject.SetActive(false);
        }
    }

    private void ResetGroup(Transform group)
    {
        for (int i = 0; i < group.childCount; i++)
        {
            Transform child = group.GetChild(i);
            child.gameObject.SetActive(true);
            child.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
