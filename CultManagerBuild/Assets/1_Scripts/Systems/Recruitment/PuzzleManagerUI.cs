using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManagerUI : MonoBehaviour
{
    public GameObject[] playerTokens;
    public GameObject[] locations;
    public GameObject tokenObject;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TokenSelection(int tokenID)
    {
        tokenObject = playerTokens[tokenID];
    }

    public void PlaceToken(int locationId)
    {
        tokenObject.transform.parent = locations[locationId].transform;
        tokenObject.transform.position = locations[locationId].transform.position;
    }
}
