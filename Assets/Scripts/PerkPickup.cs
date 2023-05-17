using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkPickup : Interactable
{
    private int random;

    public override string GetDescription()
    {
        if (isSelected == false)
        {
            return "Press 'E' to pickup " + random;
        }
        else
        {
            return "";
        }
    }

    private void Awake()
    {
        random = Random.Range(0, 10);
        ParticleSystem currentParticleSystem = GetComponent<ParticleSystem>();
        var psMain = currentParticleSystem.main;

        var randomColor = new ParticleSystem.MinMaxGradient(new Vector4(0, 0, 0, 1), new Vector4(1, 1, 1, 1));
        randomColor.mode = ParticleSystemGradientMode.RandomColor;

        psMain.startColor = randomColor;
    }

    public override void Interact()
    {

    }
}
