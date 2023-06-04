using Controllers;
using UnityEngine;
using Views;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private DetailView _detailView;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _explodeSpeed = 0.1f;

    [SerializeField] private DetailPartButtonView _detailPartButtonView;

    private CameraController _cameraController;
    private ExplodeSchemeController _explodeSchemeController;
    private UIController _UIController;
    
    private void Awake()
    {
       _cameraController = new CameraController(_camera, _cameraPivot);
       _explodeSchemeController = new ExplodeSchemeController(_detailView,_cameraPivot,_explodeSpeed );
       _UIController = new UIController( _detailPartButtonView);
    }

    private void Start()
    {
        _UIController.OnMainDetailButton += _explodeSchemeController.ToggleExplodedView;
        _UIController.OnMainDetailButton += _explodeSchemeController.SetAllPartsActive;
        _UIController.InitAllPartButtons(_explodeSchemeController.ChildDetailParts);
        _UIController.OnDetailPartButton += _explodeSchemeController.SetSelectedPart;
        _explodeSchemeController.OnDetailPartSelect += _cameraController.SetPivot;
    }

    private void LateUpdate()
    {
        _explodeSchemeController.Execute();
    }
}
