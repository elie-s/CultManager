using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CultManager
{
    [ExecuteAlways]
    public class BloodColorHandler : MonoBehaviour
    {
        [SerializeField] private BloodColor data = default;
        [SerializeField] private BloodType type = default;
        [SerializeField] private Image imageComponent = default;
        [SerializeField] private SpriteRenderer spriteRendererComponent = default;

        private void OnEnable()
        {
            if (imageComponent) imageComponent.color = data.GetColor(type);
            if (spriteRendererComponent) spriteRendererComponent.color = data.GetColor(type);
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (imageComponent) imageComponent.color = data.GetColor(type);
            if (spriteRendererComponent) spriteRendererComponent.color = data.GetColor(type);
        }
#endif
    }
}