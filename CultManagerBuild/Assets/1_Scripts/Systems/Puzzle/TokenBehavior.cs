using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class TokenBehavior : MonoBehaviour
    {
        public PuzzleNode puzzleNode;
        PuzzleManagerUI puzzleManagerUI;
        Transform[] children;


        void Start()
        {
            puzzleManagerUI = FindObjectOfType<PuzzleManagerUI>();
            GatherChildren();
            SetUpTraits(puzzleNode);
        }


        void Update()
        {

        }

        void GatherChildren()
        {
            children = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                children[i] = transform.GetChild(i);
            }
        }

        void SetUpTraits(PuzzleNode _puzzleNode)
        {
            for (int i = 0; i < children.Length; i++)
            {
                if (puzzleNode.node.token.cultistTraits.HasFlag((CultistTraits)Mathf.Pow(2, i)))
                {
                    children[i].gameObject.SetActive(true);
                }
                else
                {
                    children[i].gameObject.SetActive(false); ;
                }
            }
        }

        public void PlaceToken()
        {
            puzzleManagerUI.PlaceThisToken(gameObject);
        }
    }

}
