using System;
using UnityEngine;

namespace Views
{
    [Serializable]
    public class DetailPartView : MonoBehaviour
    {
        public MeshRenderer meshRenderer;
        public Vector3 originalPosition;
        public Vector3 explodedPosition;
    }
}