using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private MainDetailButtonView _mainDetailButtonView;
        [SerializeField] private RectTransform _placeToInstButtons;
        [SerializeField] private Button _backButton;
        public RectTransform PlaceToInstButtons => _placeToInstButtons;

        public MainDetailButtonView DetailButtonView => _mainDetailButtonView;

        public Button BackButton => _backButton;
    }
}