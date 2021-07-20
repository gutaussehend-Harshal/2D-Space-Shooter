using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class BGScroll : MonoBehaviour
    {
        [SerializeField] private float scrollSpeed = 0.1f;
        MeshRenderer meshRenderer;
        private float xScroll;
        void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        void Update()
        {
            Scroll();
        }

        void Scroll()
        {
            xScroll = Time.time * scrollSpeed;
            Vector2 offset = new Vector2(xScroll, 0f);
            meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
        }
    }
}