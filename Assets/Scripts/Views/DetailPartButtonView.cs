using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class DetailPartButtonView : MonoBehaviour
    {
        [SerializeField] private Button _detailPartButton;
        [SerializeField] private TMP_Text _partName;
        [SerializeField] private DetailPartView _detailPartView;

        public TMP_Text PartName => _partName;

        public DetailPartView PartView
        {
            get => _detailPartView;
            set => _detailPartView = value;
        }

        public Button DetailPartButton => _detailPartButton;
    }
}