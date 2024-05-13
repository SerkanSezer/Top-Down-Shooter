using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="ItemSO",menuName ="ScriptableObjects/ItemSO",order =1)]
public class ItemSO : ScriptableObject
{
    public Transform pfItem;
    public float chaseRadius;
    public float attackRadius;
    public int damageAmount;
    public AudioClip itemSound;
    //public float attackRate; Daha sonra oyuna g�re enemy lerin kulland��� silah t�rlerine g�re
    //hangi s�kl�kla ate� edecekler bu de�i�ken �zerinden karar verebilirsin. �imdilik hepsi de 1 saniye de bir attack rate i var.
}
