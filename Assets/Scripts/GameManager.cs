using System;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CustomDoubleLinkedList snapshotSystem = new();

    public Player player;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }
    [Button]
    public void SaveTurn()
    {
        snapshotSystem.SaveTurn();
        Debug.Log("Saving turn: " + snapshotSystem.Count);
    }
    //[Button]
    public void LoadTurn()
    {
        snapshotSystem.LoadTurn(player);
    }
    [Button]
    public void NextTurn()
    {
        snapshotSystem.MoveForward();
        LoadTurn();
    }
    [Button]
    public void PrevTurn()
    {
        snapshotSystem.MoveBackwards();
        LoadTurn();
    }


}