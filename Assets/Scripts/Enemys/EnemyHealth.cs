using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour , IDamageable<float>
{
    private float Health = 50f;
    public TextMeshProUGUI DamageTMP;
    public SpriteRenderer SpriteRenderer;
    public Color DefaultColor;
    public float XP;

    public event System.Action <float> GiveXP;
    private void Start()
    {
        DefaultColor = SpriteRenderer.color;
    }
    public void Damage(float DamageTaken)
    {
        //Debug.Log(gameObject.name + "Has Taken : " + DamageTaken + "And Has HP: " + Health);
        Health = Health - DamageTaken;
        StartCoroutine(Flash()); 
        StartCoroutine(DamageText(DamageTaken));
        if(Health < 0)
        {
            EnemyDeath();
        }
    }
    public void EnemyDeath()
    {
        DropXP();
        Destroy(gameObject);
        //TODO Add Death Sound and Animation
    }
    public void DropXP()
    {
        GiveXP.Invoke(XP); //System Event that is called when dies and links to the CharcterXP and gives the XP (Linked in the WaveSpawner)
    }

    IEnumerator Flash() //Damage flash
    {
        if (SpriteRenderer == null)
            yield return null;
        
        SpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.1f);
        SpriteRenderer.color = DefaultColor;
    }

    IEnumerator DamageText(float DamageTaken) //Damage Text
    {
        if (DamageTMP == null)
            yield return null;
        DamageTMP.text = DamageTaken.ToString();
        yield return new WaitForSeconds(.1f);
        DamageTMP.text = " ";
    }
    private void OnDisable()//Defencive coding was giving errors when destoied
    {
        StopAllCoroutines();
    }

    //TODO Add Damage Sound

}
