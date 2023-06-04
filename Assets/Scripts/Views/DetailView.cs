using UnityEngine;
using System.Collections.Generic;
using Views;

public class DetailView : MonoBehaviour
{
    [SerializeField] private List<DetailPartView> _childMeshRenderers;
    /*private bool _isInExplodedView;
    public float explosionSpeed = 0.1f;
    private bool _isMoving;
    */

    public List<DetailPartView> ChildMeshRenderers
    {
        get => _childMeshRenderers;
        set => _childMeshRenderers = value;
    }

    /*
    private void Awake()
    {
        _childDetailParts = new List<DetailPartView>();
        foreach (var item in GetComponentsInChildren<MeshRenderer>())
        {
            DetailPartView mesh = new DetailPartView();
            mesh.meshRenderer = item;
            mesh.originalPosition = item.transform.position;
            mesh.explodedPosition = item.bounds.center;
            ChildMeshRenderers.Add(mesh);
        }
    }

    public void DoExplode()
    { 
        if (!_isMoving) return;
        if (_isInExplodedView)
        {
            var posCounter = .1f;
            var negCounter = .1f;
            negCounter *= ChildMeshRenderers.Count/2;
            for (var i = 0; i < ChildMeshRenderers.Count; i++)
            {
                var item = ChildMeshRenderers[i];
                if(i<ChildMeshRenderers.Count/2)
                {
                    item.explodedPosition = new Vector3(item.explodedPosition.y - negCounter, item.originalPosition.y, item.originalPosition.z);
                    item.meshRenderer.transform.position = Vector3.Lerp(
                        item.meshRenderer.transform.position,
                        item.explodedPosition,
                        explosionSpeed);
                    negCounter -= 0.1f;
                }
                else
                {
                    item.explodedPosition = new Vector3(item.explodedPosition.y +posCounter, item.originalPosition.y, item.originalPosition.z);
                    item.meshRenderer.transform.position = Vector3.Lerp(
                        item.meshRenderer.transform.position,
                        item.explodedPosition,
                        explosionSpeed);
                    posCounter += 0.1f;
                }
                if (Vector3.Distance(item.meshRenderer.transform.position, item.explodedPosition) < 0.00001f)
                {
                    _isMoving = false;
                }
            }
        }
        else
        {
            foreach (var item in ChildMeshRenderers)
            {
                item.meshRenderer.transform.position = Vector3.Lerp(item.meshRenderer.transform.position, item.originalPosition, explosionSpeed);
                if (Vector3.Distance(item.meshRenderer.transform.position, item.originalPosition) < 0.00001f)
                {
                    _isMoving = false;
                }
            }
        }
    }

    public void ToggleExplodedView()
    {
        if (_isInExplodedView)
        {
            _isInExplodedView = false;
            _isMoving = true;
        }
        else 
        {
            _isInExplodedView = true;
            _isMoving = true;
        }
    }*/

}