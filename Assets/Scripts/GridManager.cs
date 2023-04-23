using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private float width, height, offset;
    
    [SerializeField] private Tile tilePrefab;
    public GameObject TileHolder;
    private void Start()
    {
        GenerateGrid();

    }
    void GenerateGrid()
    {
        for(float x = 0; x < width; x+=offset)
        {
            for (float y = 0; y < height; y+=offset)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x, tilePrefab.transform.position.y, y), tilePrefab.transform.rotation);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.parent = TileHolder.transform;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && TileHolder.activeSelf == false)
        {
            TileHolder.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            TileHolder.SetActive(false);
        }
    }
}
