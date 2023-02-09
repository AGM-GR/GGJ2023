using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations;

public class Item : MonoBehaviour
{
    public ItemData Data;
    public ItemSlot Slot;

    public ItemsSpawner Spawner;
    private Animator _animator;
    [SerializeField] private LookAtConstraint _iconLookAt;

    private void Start()
    {
        _animator = GetComponent<Animator>();


        var constraint = new ConstraintSource();
        constraint.sourceTransform = Camera.main.transform;
        constraint.weight = 1;
        _iconLookAt.AddSource(constraint);
    }

    public async Task Pick(CarColor color)
    {
        // anim, vfx, etc
        Slot = FindObjectsOfType<ItemSlot>().Where(s => s.Rave == color).First();
        Slot.ShowItem(Data);
        _animator.SetTrigger("OpenBox");
        await Task.Delay(2500);
        gameObject.SetActive(false);
        Spawner.ItemDisabled();
    }

}
