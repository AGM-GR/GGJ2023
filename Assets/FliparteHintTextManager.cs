using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FliparteHintTextManager : MonoBehaviour
{
    [System.Serializable]
    public struct ControlSchemeSpriteIdBinding
    {
        public string ControlScheme;
        [Space]
        public string SpriteId;
    }

    public CarColor carColor;
    [SerializeField] private ControlSchemeSpriteIdBinding[] _controlSchemeBindings;

    public void SetControlScheme(string controlScheme)
    {
        TextMeshProUGUI tmPro = GetComponent<TextMeshProUGUI>();

        if (controlScheme == "Keyboard")
        {
            tmPro.text = $"Pulsa X para FLIPARTE";
            return;
        }

        string id = _controlSchemeBindings.Where(c => c.ControlScheme == controlScheme).FirstOrDefault().SpriteId;
        tmPro.text = $"Pulsa <sprite name={id}> para FLIPARTE";
    }
}
