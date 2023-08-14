using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingManager : MonoBehaviour
{
    [SerializeField] GameObject globalLight;

    private GameManager _gameManager;
    private Light2D[] _lightSources;
    private ShadowCaster2D[] _shadowCasters;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void ManageLights(bool value)
    {
        if (value)
        {
            ActivateLighting();
        } else
        {
            DeactivateLighting();
        }
    }

    private void DeactivateLighting()
    {
        // set flag in game manager
        // handle this flag in objects using light source
        // eg. deactivate light source and shadow caster if flag==true
        // the same thing will be with particles

        _lightSources = FindObjectsOfType<Light2D>();
        _shadowCasters = FindObjectsOfType<ShadowCaster2D>();

        foreach (Light2D lightSource in _lightSources)
        {
            lightSource.enabled = false;
        }
        globalLight.SetActive(true);
        foreach (ShadowCaster2D shadowCaster in _shadowCasters)
        {
            shadowCaster.enabled = false;
        }
        _gameManager.LightingEnabled = false;
    }

    private void ActivateLighting()
    {
        globalLight.SetActive(false);

        try
        {
            foreach (Light2D lightSource in _lightSources)
            {
                lightSource.enabled = true;
            }
            foreach (ShadowCaster2D shadowCaster in _shadowCasters)
            {
                shadowCaster.enabled = true;
            }
        } catch (MissingReferenceException) { }

        _gameManager.LightingEnabled = true;
    }
}
