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
        private GameObject playerTokenPrefab;
        [SerializeField]
        private GameObject locationsParent;
        [SerializeField]
        private GameObject LineRendererPrefab;
        [SerializeField]
        private List<GameObject> playerTokens;
        //[SerializeField]
        private GameObject[] locations;
        [SerializeField]
        private List<Vector3> lineRenderPos;
        [SerializeField]
        private GameObject locationObject;
        private int locationId;

        //private TokenBehavior tokenObjectBehavior;


        void Start()
        {
            locations = new GameObject[locationsParent.transform.childCount];
            GatherChildren(locationsParent, locations);
            /*playerTokens = new GameObject[playerTokenParent.transform.childCount];
            GatherChildren(playerTokenParent, playerTokens);*/
            DisplayAll();
        }

        void Update()
        {
        }

        void GatherChildren(GameObject parent, GameObject[] children)
        {
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                children[i] = parent.transform.GetChild(i).gameObject;
                if (children[i].GetComponent<NodeBehavior>())
                {
                    children[i].GetComponent<NodeBehavior>().node.id = i;
                }

            }
        }

        void DisplayAll()
        {
            for (int i = 0; i < puzzleManager.cultistTokens.Length; i++)
            {
                playerTokens.Add(Instantiate(playerTokenPrefab, transform.position, Quaternion.identity, playerTokenParent.transform));
                playerTokens[i].GetComponent<TokenBehavior>().puzzleNode.node.token = puzzleManager.cultistTokens[i];
            }
        }

        void DestroyAll()
        {
            for (int i = 0; i < playerTokens.Count; i++)
            {
                Destroy(playerTokens[i]);
            }
        }

        public void DisplayTokensWithTrait(NodeBehavior nodeBehavior)
        {
            //Debug.Log(nodeBehavior.node.token.cultistTraits);
            DestroyAll();
            playerTokens.Clear();
            for (int i = 0; i < puzzleManager.cultistTokens.Length; i++)
            {
                if (nodeBehavior.node.token.IncludesToken(puzzleManager.cultistTokens[i]))
                {
                    playerTokens.Add(Instantiate(playerTokenPrefab, transform.position, Quaternion.identity, playerTokenParent.transform));
                    //Debug.Log(puzzleManager.cultistTokens[i].cultistTraits);
                    playerTokens[playerTokens.Count - 1].GetComponent<TokenBehavior>().puzzleNode.node.token = puzzleManager.cultistTokens[i];
                }
            }
            locationObject = nodeBehavior.gameObject;
            locationId = nodeBehavior.node.id;
        }

        /*public void TokenSelection(int tokenID)
        {
            tokenObject = playerTokens[tokenID];
            tokenObjectBehavior = tokenObject.GetComponent<TokenBehavior>();
        }
        */

        public void PlaceThisToken(GameObject tokenObject)
        {
            if (tokenObject.transform.parent == playerTokenParent.transform)
            {
                if (locationObject)
                {
                    playerTokens.Remove(tokenObject);
                    tokenObject.transform.SetParent(locationObject.transform);
                    tokenObject.transform.position = locationObject.transform.position;
                    lineRenderPos.Add(tokenObject.transform.position);
                    tokenObject.GetComponent<TokenBehavior>().puzzleNode.node.id = locationId;
                    puzzleManager.puzzleNodes.Add(tokenObject.GetComponent<TokenBehavior>().puzzleNode);
                }
            }
            else
            {
                Destroy(tokenObject);
                DisplayTokensWithTrait(locationObject.GetComponent<NodeBehavior>());
                lineRenderPos.Remove(tokenObject.transform.position);
                tokenObject.GetComponent<TokenBehavior>().puzzleNode.node.id = 0;
                puzzleManager.puzzleNodes.Remove(tokenObject.GetComponent<TokenBehavior>().puzzleNode);
            }
        }

        public void CastSacrifice()
        {
            puzzleManager.TestCheckPatterns();
            GameObject lineRendererGO = Instantiate(LineRendererPrefab, transform.position, Quaternion.identity, puzzleManager.transform);
            LineRenderer lr = lineRendererGO.GetComponent<LineRenderer>();
            lr.positionCount = lineRenderPos.Count;
            lr.SetPositions(lineRenderPos.ToArray());
        }
    }

}
