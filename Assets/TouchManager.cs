using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public int clickCounter = 2;
    public GameObject selectect1go;
    public GameObject selectect2go;
    public Sprite selectedTile1;
    public Sprite selectedTile2;
    public static bool canClick;
    

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if(canClick==true)
            {
                canClick = false;
                Vector2 raycaastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(raycaastPosition, Vector2.zero);
                if (hit.collider != null)
                {
                    StartCoroutine(Select(hit));
                }
            }
        }
        if(clickCounter==0)
        {
            clickCounter = 3;
            StartCoroutine(Change());
        }
    }
    IEnumerator Select(RaycastHit2D hit)
    {
        //1 zazanczenie
        if (clickCounter == 2)
        {
            var tile = hit.collider.gameObject.GetComponent<SpriteRenderer>();
            tile.color = new Color(.5f, .5f, .5f, .5f);
            selectedTile1=hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite;
            selectect1go = hit.collider.gameObject;
            clickCounter -= 1;
            canClick = true;
        }
        //2 zaznczenie 
        else if (clickCounter == 1)
        {
            canClick = false;
            var tile = hit.collider.gameObject.GetComponent<SpriteRenderer>();
            tile.color = new Color(.5f, .5f, .5f, .5f);
            selectedTile2 = hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite;
            selectect2go = hit.collider.gameObject;
            clickCounter -= 1;
        }
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator Change()
    {
        Debug.Log(selectect1go.transform.position.y - selectect2go.transform.position.y);
            Debug.Log(selectect1go.transform.position.x - selectect2go.transform.position.x);
        if (selectect1go.transform.position.y - selectect2go.transform.position.y<0.6f && selectect1go.transform.position.y - selectect2go.transform.position.y > -0.6f)
        {
            float distance = Vector3.Distance(selectect1go.transform.position, selectect2go.transform.position);
            if(distance <0.8f && distance >-0.8f)
            {
                selectect1go.GetComponent<SpriteRenderer>().sprite = selectedTile2;
                selectect2go.GetComponent<SpriteRenderer>().sprite = selectedTile1;
            }
        }
        selectect1go.GetComponent<SpriteRenderer>().color=new Color(1f, 1f, 1f, 1f);
        selectect2go.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        selectect1go = new GameObject();
        selectect2go = new GameObject();
        clickCounter = 2;
        GameControler.tileMaching = true;

        yield return null;

    }
}
