using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace CultManager
{
    public class PuzzleManagerUI : MonoBehaviour
    {
        public PuzzleManager puzzleManager;
        [SerializeField]
        private GameObject playerTokenParent;
        [SerializeField]
        private GameObject locationsParent;
        //[SerializeField]
        private GameObject[] playerTokens;
        //[SerializeField]
        private GameObject[] locations;
        private GameObject tokenObject;


        void Start()
        {
            locations = new GameObject[locationsParent.transform.childCount];
            GatherChildren(locationsParent, locations);
            playerTokens = new GameObject[playerTokenParent.transform.childCount];
            GatherChildren(playerTokenParent, playerTokens);
        }

        void Update()
        {
        }

        void GatherChildren(GameObject parent, GameObject[] children)
        {
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                children[i] = parent.transform.GetChild(i).gameObject;
            }
        }

        public void TokenSelection(int tokenID)
        {
            
            tokenObject = playerTokens[tokenID];
        }

        public void PlaceToken(int locationId)
        {
            if (tokenObject)
            {
                tokenObject.transform.SetParent(locations[locationId].transform);
                tokenObject.transform.position = locations[locationId].transform.position;
                AddNode(locationId);
            }
            
        }

        void AddNode(int a)
        {
            tokenObject.GetComponent<TokenBehavior>().puzzleNode.node.id = a;
            puzzleManager.puzzleNodes.Add(tokenObject.GetComponent<TokenBehavior>().puzzleNode);
        }
    }

}
