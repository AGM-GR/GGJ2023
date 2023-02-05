using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Item picker and executer
/// </summary>
public class ItemPicker : MonoBehaviour
{
    private Character _character;
    [HideInInspector] public ItemData CurrentItemData; // just one slot
    private ItemSlot _slot; // just one slot

    [Space]
    private GameObject _currentItemPrefab;
    public GameObject BaseballPrefab;
    public GameObject BaseballTrailPrefab;

    public bool HasItem => CurrentItemData != null;
    public bool CurrentItemNeedsTarget => CurrentItemData.NeedsTargetInteractable;

    private void Start()
    {
        _character = GetComponent<Character>();
        _slot = FindObjectsOfType<ItemSlot>().Where(s => s.Rave == _character.CharacterColor).First();
    }

    private void OnTriggerEnter(Collider other)
    {
        Item item;
        if (other.TryGetComponent<Item>(out item) && !HasItem)
        {
            CurrentItemData = item.Data;
            // update ui
            item.Pick(_character.CharacterColor);

            if(item.Data.Name == "Baseball Bat")
            {
                BaseballPrefab.SetActive(true);
                _currentItemPrefab = BaseballPrefab;
            }
        }
    }

    public async void UseItem()
    {
        _slot.HideItem();

        if (_currentItemPrefab != null)
        {
            BaseballTrailPrefab.SetActive(true);
            await Task.Delay(CurrentItemData.MilisecondsDelayToHideItem);
            _currentItemPrefab.SetActive(false);
            BaseballTrailPrefab.SetActive(false);
        }

        CurrentItemData = null;
    }
}