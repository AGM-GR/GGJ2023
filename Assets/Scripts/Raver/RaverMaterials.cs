using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaverMaterials : MonoBehaviour
{
    public Renderer _renderer;
    public float _initialDissolveValue;

    private Material _dissolveMaterial;

    private void Awake()
    {
        _dissolveMaterial = _renderer.material;
        _dissolveMaterial.SetFloat("_DissolveProgression", 1f);
    }

    private void OnEnable()
    {
        ResetDissolve();
    }

    public void ResetDissolve()
    {
        if (_dissolveMaterial != null)
            _dissolveMaterial.SetFloat("_DissolveProgression", 1f);
    }

    public void DoDissolve(float time)
    {
        StartCoroutine(Dissolve(time));
    }

    private IEnumerator Dissolve(float time)
    {
        yield return null;
        float alpha = 0f;
        while (alpha < 1)
        {
            alpha += Time.deltaTime * time;
            _dissolveMaterial.SetFloat("_DissolveProgression", Mathf.Lerp(1f, _initialDissolveValue, alpha));
            yield return null;
        }
    }

    /*public Material[] _playerMaterial;
    public Material _raverMaterial;

    private void OnValidate()
    {
        if (_renderer == null)
            _renderer = GetComponent<Renderer>();
    }

    public void SetPlayerMaterial(int playerIndex)
    {
        _renderer.sharedMaterial = _playerMaterial[playerIndex];
    }

    public void SetRaverMaterial()
    {
        _renderer.sharedMaterial = _raverMaterial;
    }*/
}
