using System;

namespace GarageAppV1.Shared;

public class VehicleUtils
{
	public static string GetIconByVehicleType(VehicleTypes type) =>
		 type switch
		 {
			 VehicleTypes.Car => "🚗",
			 VehicleTypes.Airplane => "✈️ ",
			 VehicleTypes.Boat => "🛥️ ",
			 VehicleTypes.Bus => "🚌",
			 VehicleTypes.Motorcycle => "🛵",
			 _ => "🤷"
		 };

}
