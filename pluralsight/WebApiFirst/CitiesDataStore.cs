using System;
using WebApiFirst.Models;

namespace WebApiFirst;

public class CitiesDataStore
{
	public List<CityDto> Cities { get; set; }

	public static CitiesDataStore Current { get; } = new CitiesDataStore();

	public CitiesDataStore()
	{

		Cities = new List<CityDto>(){
			new CityDto(){
				Id = 1,
				Name = "DD1",
				Description = "Xx",
				PointsOfInterests = new List<PointsOfInterestDto>()
				{
					new()
					{
						Id= 1,
						Name = "PP1",
						Description = "dPP1"
					},
					new(){
						Id= 2,
						Name = "PP2",
						Description = "dPP2"
					}
				}
			},
			new CityDto(){Id = 2, Name = "DD2", Description = "Xx"},
			new CityDto(){
				Id = 3,
				Name = "DD3",
				Description = "Xx",
				PointsOfInterests = new List<PointsOfInterestDto>()
				{
					new()
					{
						Id= 1,
						Name = "PP12",
						Description = "dPP12"
					},
					new(){
						Id= 2,
						Name = "PP22",
						Description = "dPP22"
					}
				}
			}
		};

	}
}
