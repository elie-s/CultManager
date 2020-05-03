using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CultManager
{
    public class CultistsDisplayer : MonoBehaviour
    {
        [SerializeField] private CultData data = default;
        [SerializeField] private GameObject cultistPrefab = default;
        [SerializeField] private IslandManager[] islandManagers = default;
        [SerializeField] private SacrificeIslandManager sacrificeIslandManager = default;

        [ContextMenu("Display Cultists")]
        public void DisplayCultists()
        {
            int displayed = 0;

            foreach (IslandManager island in islandManagers)
            {
                displayed += island.cultists.Count;
            }

            Debug.Log(displayed);
            List<Cultist> toDisplay = new List<Cultist>();

            foreach (Cultist cultist in data.cultists)
            {
                if (IsAlreadyDisplayed(cultist)) continue;

                toDisplay.Add(cultist);
            }

            Debug.Log(toDisplay.Count);

            int perIsland = Mathf.FloorToInt((float)toDisplay.Count / (float)islandManagers.Length);

            for (int i = 0; i < islandManagers.Length; i++)
            {
                islandManagers[i].SpawnCultists(toDisplay.GetRange(0, perIsland).ToArray(), cultistPrefab);
                toDisplay.RemoveRange(0, perIsland);
            }

            if(toDisplay.Count > 0)
            {
                islandManagers[0].SpawnCultists(toDisplay.ToArray(), cultistPrefab);
            }
        }

        public void DisplayNewCultists(Cultist[] _cultists)
        {
            List<Cultist> toDisplay = _cultists.ToList();

            int perIsland = Mathf.FloorToInt((float)toDisplay.Count / (float)islandManagers.Length);

            for (int i = 0; i < islandManagers.Length; i++)
            {
                islandManagers[i].SpawnCultists(toDisplay.GetRange(0, perIsland).ToArray(), cultistPrefab);
                toDisplay.RemoveRange(0, perIsland);
            }

            if(toDisplay.Count > 0)
            {
                islandManagers[0].SpawnCultists(toDisplay.ToArray(), cultistPrefab);
            }
        }

        public void DisplaySacrificeCultists(Cultist[] _cultists)
        {
            sacrificeIslandManager.SpawnCultists(_cultists, cultistPrefab);
        }

        public void RemoveSacrificeCultists(Cultist[] _cultists)
        {
            foreach (Cultist cultist in _cultists)
            {
                sacrificeIslandManager.RemoveCultist(cultist);
            }   
        }

        public void RemoveCultists(Cultist[] _cultist)
        {
            foreach (IslandManager island in islandManagers)
            {
                for (int i = 0; i < _cultist.Length; i++)
                {
                    island.RemoveCultist(_cultist[i]);
                }
            }
        }

        public bool IsAlreadyDisplayed(Cultist _cultist)
        {
            for (int i = 0; i < islandManagers.Length; i++)
            {
                if (islandManagers[i].HoldCultist(_cultist)) return true;
            }

            return false;
        }
    }
}