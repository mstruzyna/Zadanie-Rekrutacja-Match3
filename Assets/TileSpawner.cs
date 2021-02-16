using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public int xSize;
    public int ySize;
    public GameObject tile;
    public List<Sprite> tileSprite;
    private Sprite currentSprite;
    private Sprite previusSprite;


    private void Start()
    {
        Vector2 offset = tile.GetComponent<SpriteRenderer>().bounds.size;
        CreateBoard(offset.x, offset.y);
    }
    private void CreateBoard(float xOffset, float yOffset)
    {

        float startX = transform.position.x;     
        float startY = transform.position.y;

        for (int x = 0; x < xSize; x++)
        {      // 11
            for (int y = 0; y < ySize; y++)
            {

                GameObject newTile = Instantiate(tile, new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), tile.transform.rotation);
                currentSprite = tile.GetComponent<SpriteRenderer>().sprite = tileSprite[Random.Range(0, tileSprite.Count)];
                if (currentSprite == previusSprite)
                {
                    previusSprite = tile.GetComponent<SpriteRenderer>().sprite = tileSprite[Random.Range(0, tileSprite.Count)];
                }
                else
                {
                    currentSprite = tile.GetComponent<SpriteRenderer>().sprite = currentSprite;
                    previusSprite = currentSprite;
                }

            }
        }
    }


}
