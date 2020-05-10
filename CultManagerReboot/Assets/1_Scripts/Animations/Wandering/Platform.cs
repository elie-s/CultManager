using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private float span = 1.0f;
        [SerializeField] private float shift = 0.0f;

        public int cultistAmount { get; private set; }

        public Vector2 leftBorder => transform.position - Vector3.left * span / 2.0f + Vector3.right * shift;
        public Vector2 rightBorder => transform.position - Vector3.right * span / 2.0f + Vector3.right * shift;

        public GameObject SpawnCultist(GameObject _prefab, Transform _parent, Cultist _cultist, bool _spawnAtOrigin)
        {
            Vector2 pos = _spawnAtOrigin ? (Vector2)transform.position : Evaluate(Random.value);
            GameObject gObject = Instantiate(_prefab, pos, Quaternion.identity, _parent);
            gObject.GetComponent<CultistAnimationBehaviour>().Init(this, _cultist);

            return gObject;
        }

        public Vector2 Evaluate(float _t)
        {
            return Vector2.Lerp(leftBorder, rightBorder, _t);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawLine(leftBorder, rightBorder);
            Gizmos.DrawLine(rightBorder + Vector2.up * 0.02f, rightBorder - Vector2.up * 0.02f);
            Gizmos.DrawLine(leftBorder + Vector2.up * 0.02f, leftBorder - Vector2.up * 0.02f);
        }

        public void RegisterCultist()
        {
            cultistAmount++;
        }

        public void UnregisterCultist()
        {
            cultistAmount--;
        }
    }
}