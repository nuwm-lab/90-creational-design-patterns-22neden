using System;
using System.Collections.Generic;

// ==== Модель ракети ====
public class Rocket
{
    public string Name { get; set; }
    public List<string> Stages { get; } = new();
    public string Engine { get; set; }
    public string Payload { get; set; }

    public override string ToString()
    {
        return $"🚀 Ракета: {Name}\n" +
               $"  Двигун: {Engine}\n" +
               $"  Ступені: {string.Join(", ", Stages)}\n" +
               $"  Корисний вантаж: {Payload}";
    }
}

// ==== Абстрактний будівельник ====
public abstract class RocketBuilder
{
    protected Rocket rocket = new();

    public abstract void SetName();
    public abstract void BuildStages();
    public abstract void BuildEngine();
    public abstract void BuildPayload();

    public Rocket GetRocket() => rocket;
}

// ==== Конкретні будівельники ====
public class CargoRocketBuilder : RocketBuilder
{
    public override void SetName() => rocket.Name = "Falcon Heavy";
    public override void BuildStages()
    {
        rocket.Stages.Add("Перший ступінь – багаторазовий");
        rocket.Stages.Add("Другий ступінь – одноразовий");
    }
    public override void BuildEngine() => rocket.Engine = "Merlin 1D";
    public override void BuildPayload() => rocket.Payload = "Вантажний модуль (до 60 тонн)";
}

public class TouristRocketBuilder : RocketBuilder
{
    public override void SetName() => rocket.Name = "Starship";
    public override void BuildStages()
    {
        rocket.Stages.Add("Super Heavy Booster");
        rocket.Stages.Add("Starship Orbital");
    }
    public override void BuildEngine() => rocket.Engine = "Raptor 2";
    public override void BuildPayload() => rocket.Payload = "Модуль для туристичних польотів";
}

public class SatelliteRocketBuilder : RocketBuilder
{
    public override void SetName() => rocket.Name = "Electron";
    public override void BuildStages()
    {
        rocket.Stages.Add("Перший ступінь – легкий композит");
        rocket.Stages.Add("Другий ступінь – орбітальний");
    }
    public override void BuildEngine() => rocket.Engine = "Rutherford Electric";
    public override void BuildPayload() => rocket.Payload = "Супутник на низьку орбіту";
}

// ==== Клас Director ====
public class RocketDirector
{
    private RocketBuilder builder;

    public RocketDirector(RocketBuilder builder)
    {
        this.builder = builder;
    }

    public Rocket Construct()
    {
        builder.SetName();
        builder.BuildStages();
        builder.BuildEngine();
        builder.BuildPayload();
        return builder.GetRocket();
    }
}

// ==== Програма ====
public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("ЛР9 — Патерн 'Будівельник' (варіант 10): створення космічної ракети\n");

        var director = new RocketDirector(new CargoRocketBuilder());
        var cargoRocket = director.Construct();
        Console.WriteLine(cargoRocket + "\n");

        director = new RocketDirector(new TouristRocketBuilder());
        var touristRocket = director.Construct();
        Console.WriteLine(touristRocket + "\n");

        director = new RocketDirector(new SatelliteRocketBuilder());
        var satelliteRocket = director.Construct();
        Console.WriteLine(satelliteRocket);
    }
}
