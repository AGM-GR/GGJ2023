using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] CarColor characterColor;
    public int CharacterIndex;
    public string ControlScheme;
    public GameObject[] Models;
    [HideInInspector] public Animator CharacterAnimator;
    public Transform refCameraPoint;
    [HideInInspector] public CameraZoomController ZoomController;

    public bool IsInit;

    public CarColor CharacterColor { get { return characterColor; } }

    public bool IsMale => CharacterColor == CarColor.BLUE || CharacterColor == CarColor.PURPLE;

    public void Initialize(int index, string controlScheme)
    {
        CharacterIndex = index;
        ControlScheme = controlScheme;
        characterColor = (CarColor)index;
        Models[CharacterIndex].SetActive(true);
        CharacterAnimator = Models[CharacterIndex].GetComponentInChildren<Animator>();

        IsInit = true;

        MoveToSpawningPoint();

        var car = FindObjectsOfType<Car>().Where(c => c.CarColor == CharacterColor).First();
        car.CharacterIndex = CharacterIndex;


        var cam = FindObjectsOfType<CameraZoomController>().Where(c => c.carColor == CharacterColor).First();
        cam.Setup(this);
        ZoomController = cam;

        var fliparteHint = FindObjectsOfType<FliparteHintTextManager>().Where(c => c.carColor == CharacterColor).First();
        fliparteHint.SetControlScheme(controlScheme);

        var itemSlot = FindObjectsOfType<ItemSlot>().Where(c => c.Rave == CharacterColor).First();
        itemSlot.SetControlScheme(controlScheme);
    }

    private void MoveToSpawningPoint()
    {
        var spawningPoint = FindObjectsOfType<CharacterSpawningPoint>().Where(c => c.color == characterColor).First();
        transform.position = spawningPoint.transform.position;
    }
}
