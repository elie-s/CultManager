using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class AltarDisplay : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private AltarData altarData;
        [SerializeField] private GameObject displayPrefab;

        private List<SpriteRenderer> parts=new List<SpriteRenderer>();
        private bool isSpawned;


        private void Update()
        {
            if (isSpawned)
            {
                Display();
            }
        }

        public void Spawn(AltarPartData[] _altarPartDatas)
        {
            for (int i = 0; i < _altarPartDatas.Length; i++)
            {
                GameObject instance = Instantiate(displayPrefab, transform.position, Quaternion.identity, transform);
                SpriteRenderer sr = instance.GetComponent<SpriteRenderer>();
                parts.Add(sr);
                parts[i].sprite = _altarPartDatas[i].altarSprite;
            }
            isSpawned = true;
        }

        void Display()
        {
            for (int i = 0; i < parts.Count; i++)
            {
                if (altarData.altarParts[i].isBought)
                {
                    if (altarData.altarParts[i].currentBuildPoints.value > 0 && altarData.altarParts[i].currentBuildPoints.ratio < 1f)
                    {
                        parts[i].color = new Color(1, 1, 1, 0.5f);
                    }
                    else
                    {
                        parts[i].color = new Color(1, 1, 1, 1f);
                    }
                }
                else
                {
                    parts[i].color = new Color(0, 0, 0, 0f);
                }
            }
        }
    }
}

