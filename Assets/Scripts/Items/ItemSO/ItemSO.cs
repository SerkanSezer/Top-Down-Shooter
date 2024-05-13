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
    //public float attackRate; Daha sonra oyuna göre enemy lerin kullandýðý silah türlerine göre
    //hangi sýklýkla ateþ edecekler bu deðiþken üzerinden karar verebilirsin. þimdilik hepsi de 1 saniye de bir attack rate i var.
}
