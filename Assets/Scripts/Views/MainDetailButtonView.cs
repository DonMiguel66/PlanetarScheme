using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class MainDetailButtonView : MonoBehaviour, IDetailButton
    {
        [SerializeField] private Button _detailButton;
        //[SerializeField] private DetailView _detailView;

        /*public DetailView DetailView
        {
            get=>_detailView; 
            set=> _detailView = value;
        }*/

        public Button DetailButton
        {
            get => _detailButton;
            set => _detailButton = value;
        }
    }
}