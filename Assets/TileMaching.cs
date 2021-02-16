using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMaching : MonoBehaviour
{
    public List<GameObject> hitedTilesHorizontal;
    public List<GameObject> hitedTilesVertical;
    public Transform rayUp;
    public Transform rayDown;
    public Transform rayLeft;
    public Transform rayRight;
    public bool checkHit;
    public bool checkDropDown;
    public List<Sprite> tileSprite;
    public bool GenerateNewColorTile;
    GameControler GC;
    private void Start()
    {
        GC = GameObject.Find("GameController").GetComponent<GameControler>();
        tileSprite = GameObject.Find("TileSpawner").GetComponent<TileSpawner>().tileSprite;
    }
    private void Update()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(rayLeft.position, Vector2.left, 1f);
        RaycastHit2D hitRight = Physics2D.Raycast(rayRight.position, Vector2.right, 1f);
        RaycastHit2D hitUp = Physics2D.Raycast(rayUp.position, Vector2.up, 1f);
        RaycastHit2D hitDown = Physics2D.Raycast(rayDown.position, Vector2.down, 1f);
        if (checkHit)
        {
            checkHit = false;
            if (hitLeft.collider != null)
            {
                if (gameObject.GetComponent<SpriteRenderer>().sprite == hitLeft.collider.gameObject.GetComponent<SpriteRenderer>().sprite)
                {
                    hitedTilesVertical.Add(hitLeft.collider.gameObject);
                }
            }
            if (hitRight.collider != null)
            {
                if (gameObject.GetComponent<SpriteRenderer>().sprite == hitRight.collider.gameObject.GetComponent<SpriteRenderer>().sprite)
                {
                    hitedTilesVertical.Add(hitRight.collider.gameObject);
                }
            }
            if (hitUp.collider != null)
            {
                if (gameObject.GetComponent<SpriteRenderer>().sprite == hitUp.collider.gameObject.GetComponent<SpriteRenderer>().sprite)
                {
                    hitedTilesHorizontal.Add(hitUp.collider.gameObject);
                }
            }
            if (hitDown.collider != null)
            {
                if (gameObject.GetComponent<SpriteRenderer>().sprite == hitDown.collider.gameObject.GetComponent<SpriteRenderer>().sprite)
                {
                    hitedTilesHorizontal.Add(hitDown.collider.gameObject);
                }
            }
            GC.MatchingCount.Add(hitedTilesHorizontal.Count);
            GC.MatchingCount.Add(hitedTilesVertical.Count);

        }
        if (checkDropDown)
        {
            checkDropDown = false;
            if (hitDown.collider != null)
            {
                if (hitDown.collider.gameObject.GetComponent<SpriteRenderer>().sprite == null)
                {
                    DropDownTile(hitDown);
                }
            }
        }
        if(GenerateNewColorTile)
        {
            GenerateNewColorTile = false;
            if(gameObject.GetComponent<SpriteRenderer>().sprite==null)
            {
                GenerateNewColor();
            }
        }
    }

    public void CleanBoard()
    {
        if (hitedTilesHorizontal.Count == 2)
        {
            for (int i = 0; i < hitedTilesHorizontal.Count; i++)
            {

                hitedTilesHorizontal[i].GetComponent<SpriteRenderer>().sprite = null;
                gameObject.GetComponent<SpriteRenderer>().sprite = null;
            }
        }
        if (hitedTilesVertical.Count == 2)
        {
            for (int i = 0; i < hitedTilesVertical.Count; i++)
            {
                hitedTilesVertical[i].GetComponent<SpriteRenderer>().sprite = null;
                gameObject.GetComponent<SpriteRenderer>().sprite = null;
            }
        }
        hitedTilesHorizontal.Clear();
        hitedTilesVertical.Clear();
    }

    public void DropDownTile(RaycastHit2D rayDown)
    {
        Sprite bufor = gameObject.GetComponent<SpriteRenderer>().sprite;
        rayDown.collider.gameObject.GetComponent<SpriteRenderer>().sprite = bufor;
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
    }
    public void GenerateNewColor()
    {

        gameObject.GetComponent<SpriteRenderer>().sprite = tileSprite[Random.Range(0, tileSprite.Count)];
    }


}
