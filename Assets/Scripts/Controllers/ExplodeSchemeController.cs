using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Views;

namespace Controllers
{
    public class ExplodeSchemeController : BaseController, IExecute
    {
        private DetailView _detailView;
        private List<DetailPartView> _childDetailParts;
        private List<GameObject> _detailPartsGO = new List<GameObject>();
        private bool _isInExplodedView;
        private float _explosionSpeed = 0.1f;
        private bool _isMoving;
        private bool _isAllActive;
        private Transform _mainPivot;
        
        private DetailPartView _currentActiveDetailPart;
        public List<DetailPartView> ChildDetailParts => _childDetailParts;

        public event Action<Transform, bool> OnDetailPartSelect;
        
        public ExplodeSchemeController(DetailView detailView, Transform mainPivot,float explosionSpeed)
        {
            _mainPivot = mainPivot;
            _detailView = detailView;
            _explosionSpeed = explosionSpeed;
            _childDetailParts = new List<DetailPartView>();
            foreach (var item in _detailView.GetComponentsInChildren<MeshRenderer>())
            {
                var mesh = item.gameObject.AddComponent<DetailPartView>();
                mesh.meshRenderer = item;
                mesh.originalPosition = item.transform.position;
                mesh.explodedPosition = item.bounds.center;
                _childDetailParts.Add(mesh);
                _detailPartsGO.Add(mesh.gameObject);
            }
            _detailView.ChildMeshRenderers = ChildDetailParts;
            _isAllActive = true;
        }


        public void Execute()
        {
            DoExplode();
        }
        
        private void DoExplode()
        {
            //SetAllPartsActive();
            if (!_isMoving) return;
            if (_isInExplodedView)
            {
                var posCounter = .1f;
                var negCounter = .1f;
                negCounter *= ChildDetailParts.Count/2;
                for (var i = 0; i < ChildDetailParts.Count; i++)
                {
                    var item = ChildDetailParts[i];
                    if(i<ChildDetailParts.Count/2)
                    {
                        item.explodedPosition = new Vector3(item.explodedPosition.y - negCounter, item.originalPosition.y, item.originalPosition.z);
                        item.meshRenderer.transform.position = Vector3.Lerp(
                            item.meshRenderer.transform.position,
                            item.explodedPosition,
                            _explosionSpeed);
                        negCounter -= 0.1f;
                    }
                    else
                    {
                        item.explodedPosition = new Vector3(item.explodedPosition.y +posCounter, item.originalPosition.y, item.originalPosition.z);
                        item.meshRenderer.transform.position = Vector3.Lerp(
                            item.meshRenderer.transform.position,
                            item.explodedPosition,
                            _explosionSpeed);
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
                foreach (var item in ChildDetailParts)
                {
                    item.meshRenderer.transform.position = Vector3.Lerp(item.meshRenderer.transform.position, item.originalPosition, _explosionSpeed);
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
        }

        public void SetAllPartsActive()
        {
            if (_isAllActive) return;
            foreach (var detailPartView in ChildDetailParts)
            {
                detailPartView.gameObject.SetActive(true);
            }
            _isAllActive = true;
            OnDetailPartSelect?.Invoke(_mainPivot, true);
        }
        
        public void SetSelectedPart(DetailPartView detailPartView)
        {
            if(_isAllActive)
            {
                _currentActiveDetailPart = detailPartView;
                foreach (var partView in ChildDetailParts)
                {
                    Debug.Log($"Check {partView.gameObject.name}");
                    partView.gameObject.SetActive(false);
                }
                detailPartView.gameObject.SetActive(true);
                OnDetailPartSelect?.Invoke(detailPartView.transform, false);
                _isAllActive = false;
            }
            else if (!_isAllActive && _currentActiveDetailPart != detailPartView)
            {
                _currentActiveDetailPart.gameObject.SetActive(false);
                _currentActiveDetailPart = detailPartView;
                _currentActiveDetailPart.gameObject.SetActive(true);
                OnDetailPartSelect?.Invoke(detailPartView.transform, false);
            }
            else if(!_isAllActive)
            {
                SetAllPartsActive();
            }
        }
    }
}