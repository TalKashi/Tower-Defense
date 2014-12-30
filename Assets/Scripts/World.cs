using UnityEngine;
using System;

public class World
{
    public Action<string,int> NewToy;
    public Action<GameObject, int> ToyDied;
    public Action<string, int> NewSoldier;
    public Action<string, int> SoldierDied;
    public Action NewWave;
    public Action WaveEnded;
    public Action NukeActivated;
    public Action FirstAidActivated;
    public Action PackageArrived;
    public Action<string> PackageCollected;
    public Action GameOver;

    private static World s_World;

    private World()
    {
        
    }

    public static World WorldInstance
    {
        get
        {
            if (s_World == null)
                s_World = new World();
            return s_World;
        }
    }

    public void OnNewToy(string i_TypeOfToy, int i_RowIndex)
    {
        if(NewToy != null)
            NewToy(i_TypeOfToy, i_RowIndex);
    }

    public void OnToyDied(GameObject i_TypeOfToy, int i_RowIndex)
    {
        if(ToyDied != null)
            ToyDied(i_TypeOfToy, i_RowIndex);
    }

    public void OnNewSoldier(string i_TypeOfSoldier, int i_RowIndex)
    {
        if(NewSoldier != null)
            NewSoldier(i_TypeOfSoldier, i_RowIndex);
    }

    public void OnSoldierDied(string i_TypeOfSoldier, int i_RowIndex)
    {
        if(SoldierDied != null)
            SoldierDied(i_TypeOfSoldier, i_RowIndex);
    }

    public void OnNewWave()
    {
        if(NewWave != null)
            NewWave.Invoke();
    }

    public void OnWaveEnded()
    {
        if(WaveEnded != null)
            WaveEnded.Invoke();
    }

    public void OnNukeActivated()
    {
        if(NukeActivated != null)
            NukeActivated.Invoke();
    }

    public void OnFirstAidActivated()
    {
        if(FirstAidActivated != null)
            FirstAidActivated.Invoke();
    }

    public void OnPackageArrived()
    {
        if(PackageArrived != null)
            PackageArrived.Invoke();
    }

    public void OnPackageCollected(string i_TypeOfPackage)
    {
        if(PackageCollected != null)
            PackageCollected(i_TypeOfPackage);
    }

    public void OnGameOver()
    {
        if(GameOver != null)
            GameOver.Invoke();
    }
}
