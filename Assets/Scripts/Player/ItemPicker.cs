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
    public GameObject[] BaseballPrefab;
    public GameObject[] BaseballTrailPrefab;

    private AudioSource aSource;
    public AudioClip pickItemSfx;

    public bool HasItem => CurrentItemData != null;
    public bool CurrentItemNeedsTarget => CurrentItemData.NeedsTargetInteractable;

    private void Start()
    {
        _character = GetComponent<Character>();
        aSource = GetComponent<AudioSource>();
        _slot = FindObjectsOfType<ItemSlot>().Where(s => s.Rave == _character.CharacterColor).First();
    }

    private async void OnTriggerEnter(Collider other)
    {
        Item item;
        if (other.TryGetComponent<Item>(out item) && !HasItem)
        {
            CurrentItemData = item.Data;
            // update ui
            await item.Pick(_character.CharacterColor);
            aSource.PlayOneShot(pickItemSfx);

            if (item.Data.Name == "Baseball Bat")
            {

                BaseballPrefab[(int)_character.CharacterColor].SetActive(true);
                _currentItemPrefab = BaseballPrefab[(int)_character.CharacterColor];
            }
        }
    }


    // to make more clean...
    public async void UseItem()
    {
        if (CurrentItemData.Type == ItemType.BaseballBat)
        {
            int delay = CurrentItemData.MilisecondsDelayToHideItem;
            LoseItem();
            await HandleBaseballBatModelVisibility(delay);
        }
        else
        {
            LoseItem();
        }
    }

    private async Task HandleBaseballBatModelVisibility(int msDelay)
    {
        if (_currentItemPrefab != null)
        {
            BaseballTrailPrefab[(int)_character.CharacterColor].SetActive(true);
            await Task.Delay(msDelay);
            _currentItemPrefab.SetActive(false);
            BaseballTrailPrefab[(int)_character.CharacterColor].SetActive(false);
        }
    }

    public void LoseItem(bool showVfx = false)
    {
        if (!HasItem) return;

        _slot.HideItem();
        CurrentItemData = null;
        if (showVfx)
        {
            // show vfx
        }
    }
}