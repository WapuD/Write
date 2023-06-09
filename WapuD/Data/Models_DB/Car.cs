using System;
using System.Collections.Generic;

namespace WapuD.Data.Models_DB;

public partial class Car
{
    public int IdCar { get; set; }

    public string NameCar { get; set; } = null!;

    public int CostCar { get; set; }

    public string StatusCar { get; set; } = null!;

    public int YearCar { get; set; }

    public int RunCar { get; set; }

    public string TypeCar { get; set; } = null!;

    public string ColorCar { get; set; } = null!;

    public string EngineCar { get; set; } = null!;

    public int TaxCar { get; set; }

    public string GearboxCar { get; set; } = null!;

    public string GearCar { get; set; } = null!;

    public string ConditionCar { get; set; } = null!;

    public int OwnersCar { get; set; }

    public string OwnerCar { get; set; } = null!;

    public string Vin { get; set; } = null!;
}
