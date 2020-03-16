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
        [SerializeField]
        private List<Vector3> lineRenderPos;
        //[SerializeField]
        private GameObject tokenObject;
        [SerializeField]
        LineRenderer lr;

        private TokenBehavior tokenObjectBehavior;


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
            tokenObjectBehavior = tokenObject.GetComponent<TokenBehavior>();
        }

        public void PlaceToken(int locationId)
        {
            if (tokenObject)
            {
                if (locations[locationId].GetComponent<NodeBehavior>().VerifyNode(tokenObjectBehavior.puzzleNode))
                {
                    tokenObject.transform.SetParent(locations[locationId].transform);
                    tokenObject.transform.position = locations[locationId].transform.position;
                    lineRenderPos.Add(tokenObject.transform.position);
                    AddNode(locationId);
                }
            }
            
        }

        public void CastSacrifice()
        {
            puzzleManager.TestCheckPatterns();
            lr.positionCount = lineRenderPos.Count;
            lr.SetPositions(lineRenderPos.ToArray());
        }

        void AddNode(int a)
        {
            tokenObject.GetComponent<TokenBehavior>().puzzleNode.node.id = a;
            puzzleManager.puzzleNodes.Add(tokenObjectBehavior.puzzleNode);

        }
    }

}
