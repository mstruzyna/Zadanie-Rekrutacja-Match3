using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour
{
    public GameObject[] tilesGO;
    public static bool tileMaching = false;
    public bool CleanBoard = false;
    public bool CheckDown;
    public bool GenerateNewColor;
    public List<int> MatchingCount;
    private int numbersRow;
    void Start()
    {
        numbersRow = GameObject.Find("TileSpawner").GetComponent<TileSpawner>().ySize;
        StartCoroutine(ScanForTiles());
    }
    private void Update()
    {
        if(tileMaching==true)
        {
            tileMaching = false;
            MatchTiles();
        }
        if(CleanBoard)
        {
            CleanBoard = false;
            StartCoroutine(CleanBoard1());
        }
        if(CheckDown)
        {
            CheckDown = false;
                StartCoroutine(DropDownTiles());
        }
        if(GenerateNewColor)
        {
            GenerateNewColor = false;
            StartCoroutine(IeGenerateNewColor());
        }
        
    }
    IEnumerator ScanForTiles()
    {
        yield return new WaitForSeconds(2);
        tilesGO = GameObject.FindGameObjectsWithTag("Tile");
        TouchManager.canClick = true;
    }
    public void MatchTiles()
    {
        for(int l=0;l<2;l++)
        {
            for (int j = 0; j < tilesGO.Length; j++)
            {
                tilesGO[j].GetComponent<TileMaching>().checkHit = true;
            }

        }
        CleanBoard = true;
    }
    IEnumerator CleanBoard1()
    {
        yield return new WaitForSeconds(0);
        for (int j = 0; j < tilesGO.Length; j++)
        {
            tilesGO[j].GetComponent<TileMaching>().CleanBoard();
        }
        CheckDown = true;
        
    }
    IEnumerator DropDownTiles()
    {
        CheckDown = false;
        for (int i=0;i< numbersRow; i++)
        {
            for (int j = 0; j < tilesGO.Length; j++)
            {

                tilesGO[j].GetComponent<TileMaching>().checkDropDown = true;

            }
            yield return new WaitForSeconds(0.01f);
        }
        GenerateNewColor = true;
        
    }
    IEnumerator IeGenerateNewColor()
    {
        GenerateNewColor = false;
            for (int j = 0; j < tilesGO.Length; j++)
            {
                tilesGO[j].GetComponent<TileMaching>().GenerateNewColorTile = true;
            }
            yield return new WaitForSeconds(0.01f);

        if(MatchingCount.Contains(2))
        {
            tileMaching = true;
        }
        else
        {
            TouchManager.canClick = true;
        }
        MatchingCount.Clear();
    }
}

/*
 * generuje sie plansza
 * mozemy ruszym klockiem
 *  puszczamy raycast i sprawdzamy kolizje
 *  puszczamy clearstage
 * przesuwamy klocki w dół
 * sprawdzamy czy są dopasowania
 * gdy nie ma:
 * generujemy w pustych miejsach cos
 * gdy sa:
 * puszczamy clearp
 * 
 * 
 * 
 */