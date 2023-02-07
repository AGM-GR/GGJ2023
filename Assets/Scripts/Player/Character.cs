using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] CarColor characterColor;
    public int CharacterIndex;
    public GameObject[] Models;
    [HideInInspector] public Animator CharacterAnimator;

    public bool IsInit;

    public CarColor CharacterColor { get { return characterColor; } }

    public bool IsMale => CharacterColor == CarColor.BLUE || CharacterColor == CarColor.PURPLE;

    public void Initialize(int index)
    {
        CharacterIndex = index;
        characterColor = (CarColor)index;
        Models[CharacterIndex].SetActive(true);
        CharacterAnimator = Models[CharacterIndex].GetComponentInChildren<Animator>();

        IsInit = true;

        MoveToSpawningPoint();

        var car = FindObjectsOfType<Car>().Where(c => c.CarColor == CharacterColor).First();
        car.CharacterIndex = CharacterIndex;
    }

    private void MoveToSpawningPoint()
    {
        var spawningPoint = FindObjectsOfType<CharacterSpawningPoint>().Where(c => c.color == characterColor).First();
        transform.position = spawningPoint.transform.position;
    }
}
