using System;
using System.Collections.Generic;

#region Product

/// <summary>
/// Продукт — Космічна ракета
/// </summary>
public class Rocket
{
    private readonly List<string> _stages = new();

    public string Name { get; private set; } = string.Empty;
    public string Engine { get; private set; } = string.Empty;
    public string Payload { get; private set; } = string.Empty;

    public IReadOnlyList<string> Stages => _stages.AsReadOnly();

    public void SetName(string name) => Name = name;
    public void SetEngine(string engine) => Engine = engine;
    public void SetPayload(string payload) => Payload = payload;
    public void AddStage(string stage) => _stages.Add(stage);

    public override string ToString()
    {
        return $"🚀 Ракета: {Name}\n" +
               $"  Двигун: {Engine}\n" +
               $"  Ступені: {string.Join(", ", _stages)}\n" +
               $"  Корисний вантаж: {Payload}";
    }
}

#endregion

#region Builder Abstraction

/// <summary>
/// Інтерфейс будівельника ракети
/// </summary>
public interface IRocketBuilder
{
    void Reset();
    void SetName();
    void BuildStages();
    void BuildEngine();
    void BuildPayload();
    Rocket GetRocket();
}

#endregion

#region Concrete Builders

public class CargoRocketBuilder : IRocketBuilder
{
    private Rocket _rocket = new();

    public void Reset() => _rocket = new Rocket();

    public void SetName() => _rocket.SetName("Falcon Heavy");
    public void BuildStages()
    {
        _rocket.AddStage("Перший ступінь – багаторазовий");
        _rocket.AddStage("Другий ступінь – одноразовий");
    }
    public void BuildEngine() => _rocket.SetEngine("Merlin 1D");
    public void BuildPayload() => _rocket.SetPayload("Вантажний модуль (до 60 тонн)");
    public Rocket GetRocket() => _rocket;
}

public class TouristRocketBuilder : IRocketBuilder
{
    private Rocket _rocket = new();

    public void Reset() => _rocket = new Rocket();
    public void SetName() => _rocket.SetName("Starship");
    public void BuildStages()
    {
        _rocket.AddStage("Super Heavy Booster");
        _rocket.AddStage("Starship Orbital");
    }
    public void BuildEngine() => _rocket.SetEngine("Raptor 2");
    public void BuildPayload() => _rocket.SetPayload("Модуль для туристичних польотів");
    public Rocket GetRocket() => _rocket;
}

public class SatelliteRocketBuilder : IRocketBuilder
{
    private Rocket _rocket = new();

    public void Reset() => _rocket = new Rocket();
    public void SetName() => _rocket.SetName("Electron");
    public void BuildStages()
    {
        _rocket.AddStage("Перший ступінь – легкий композит");
        _rocket.AddStage("Другий ступінь – орбітальний");
    }
    public void BuildEngine() => _rocket.SetEngine("Rutherford Electric");
    public void BuildPayload() => _rocket.SetPayload("Супутник на низьку орбіту");
    public Rocket GetRocket() => _rocket;
}

#endregion

#region Director

/// <summary>
/// Director — керує процесом побудови
/// </summary>
public class RocketDirector
{
    private readonly IRocketBuilder _builder;

    public RocketDirector(IRocketBuilder builder)
    {
        _builder = builder;
    }

    public Rocket Construct()
    {
        _builder.Reset();
        _builder.SetName();
        _builder.BuildStages();
        _builder.BuildEngine();
        _builder.BuildPayload();
        return _builder.GetRocket();
    }
}

#endregion

#region Program

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("ЛР9 — Патерн 'Будівельник' (покращена версія)\n");

        var director = new RocketDirector(new CargoRocketBuilder());
        var cargo = director.Construct();
        Console.WriteLine(cargo + "\n");

        director = new RocketDirector(new TouristRocketBuilder());
        var tourist = director.Construct();
        Console.WriteLine(tourist + "\n");

        director = new RocketDirector(new SatelliteRocketBuilder());
        var satellite = director.Construct();
        Console.WriteLine(satellite);
    }
}

#endregion
