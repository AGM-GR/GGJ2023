using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaverMaterials : MonoBehaviour
{
    public Renderer _renderer;

    public Material[] _playerMaterial;
    public Material _raverMaterial;

    private void OnValidate()
    {
        if (_renderer == null)
            _renderer = GetComponent<Renderer>();
    }

    public void SetPlayerMaterial(int playerIndex)
    {
        //_renderer.sharedMaterial = _playerMaterial[playerIndex];
    }

    public void SetRaverMaterial()
    {
        //_renderer.sharedMaterial = _raverMaterial;
    }
}
