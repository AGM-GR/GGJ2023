using System;
using UnityEngine;

public abstract class Role
{
    public float Radius = 1;
    public abstract string TargetTag { get; }
    public abstract string Name { get; }

    public abstract void ExecuteAction();
}

public class PR : Role
{
    public override string TargetTag => "Player";

    public override string Name => "PR";

    public override void ExecuteAction()
    {
        // si tiene un grupo en rango, les hace ir hacia el coche propio
        Debug.Log("Execute PR action");
    }
}

public class Badass : Role
{
    public override string TargetTag => "Player";
    public override string Name => "Badass";


    public override void ExecuteAction()
    {
        // si está cerca del PR o del coche enemigo, lincharle
        Debug.Log("Execute Badass action");
    }
}

public class Dj : Role
{
    public override string TargetTag => "Car";
    public override string Name => "DJ";


    public override void ExecuteAction()
    {
        // si está cerca del coche, empezar minijuego influencia
        Debug.Log("Execute Dj action");


    }
}
