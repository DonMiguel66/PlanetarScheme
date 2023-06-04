using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Views;
using Object = UnityEngine.Object;

namespace Controllers
{
    public class UIController : BaseController
    {
        private MainDetailButtonView _mainDetailButton;
        private DetailPartButtonView _detailPartButton;
        private UIView _uiView;
        private List<DetailPartButtonView> _detailPartButtonViews= new List<DetailPartButtonView>();

        public event Action OnMainDetailButton;
        public event Action<DetailPartView> OnDetailPartButton;
        private bool _isExplodeViewActive;
        public UIController(DetailPartButtonView detailPartButtonView)
        {
            _uiView = Object.FindObjectOfType<UIView>();
            _mainDetailButton = _uiView.DetailButtonView;
            _detailPartButton = detailPartButtonView;
            _uiView.DetailButtonView.DetailButton.onClick.AddListener(()=> OnMainDetailButton?.Invoke());
            _uiView.DetailButtonView.DetailButton.onClick.AddListener(RerollPartButtonList);
            _uiView.BackButton.onClick.AddListener((() => SceneManager.LoadScene("Scenes/StartScene")));
        }

        private void InstantiateButton(DetailPartView detailPartView)
        {
            var partButton = Object.Instantiate(_detailPartButton.gameObject, _uiView.PlaceToInstButtons, false).GetComponent<DetailPartButtonView>();
            partButton.PartName.text = detailPartView.name;
            partButton.PartView = detailPartView;
            partButton.DetailPartButton.onClick.AddListener(()=> OnPartButtonClick(detailPartView));
            _detailPartButtonViews.Add(partButton);
            partButton.gameObject.SetActive(false);
        }

        private void RerollPartButtonList()
        {
            _isExplodeViewActive = !_isExplodeViewActive;
            foreach (var detailPartButtonView in _detailPartButtonViews)
            {
                detailPartButtonView.gameObject.SetActive(_isExplodeViewActive);
            }
        }
        
        private void OnPartButtonClick(DetailPartView detailPartView)
        {
            Debug.Log($"Clicked {detailPartView}");
            OnDetailPartButton?.Invoke(detailPartView);
        }
        
        public void InitAllPartButtons(List<DetailPartView> detailPartButtonViews)
        {
            foreach (var partButtonView in detailPartButtonViews)
            {
                InstantiateButton(partButtonView);
            }
        }
    }
}