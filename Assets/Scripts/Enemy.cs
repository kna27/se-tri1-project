using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public float timeToCatchPlayer;
    public int wantednessIncrease;
    public bool playerInView;
    public float timePlayerIsInView;
    public float totalTimePlayerIsInView;
}
