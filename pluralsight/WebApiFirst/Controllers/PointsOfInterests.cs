using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiFirst.Models;

namespace WebApiFirst
{
	[ApiController]
	[Route("api/cities/{cityId:int}/pointsOfInterests")]
	public class PointsOfInterests : ControllerBase
	{
		[HttpGet()]
		[EndpointSummary("All points of interests of a city")]
		public ActionResult<IEnumerable<PointsOfInterestDto>> GetPointsOfInterests(int cityId)
		{
			// check if city exists
			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

			if (city is null)
				return NotFound();

			return Ok(city.PointsOfInterests);
		}


		[HttpGet("{pointOfInterestId:int}", Name = "GetPof")]
		[EndpointSummary("Specific point of interest of a city")]
		public ActionResult<PointsOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
		{
			// check if city exists
			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

			if (city is null)
				return NotFound();

			var pointOfInterest = city.PointsOfInterests.FirstOrDefault(x => x.Id == pointOfInterestId);

			if (pointOfInterest is null)
				return NotFound();

			return Ok(pointOfInterest);
		}

		[HttpPost()]
		public ActionResult<PointsOfInterestDto> CreatePointOfInterest(int cityId, PointsOfInterestCreationDto newPofBody)
		{
			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

			if (city is null)
				return NotFound();

			int maxId = CitiesDataStore.Current.Cities.SelectMany(city => city.PointsOfInterests).Max(pof => pof.Id);
			int newItemid = maxId + 1;

			PointsOfInterestDto newItem = new() { Name = newPofBody.Name, Description = newPofBody.Description, Id = newItemid };

			city.PointsOfInterests.Add(newItem);

			return CreatedAtRoute("GetPof", new
			{
				cityId = city.Id,
				pointOfInterestId = newItemid
			}, newItem);

		}

		[HttpPut("{id:int}")]
		public ActionResult UpdatePointOfInterest(int cityId, int id, PointsOfInterestsFullUpdateDto updateData)
		{
			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
			if (city is null)
				return NotFound();


			var cityPoi = city.PointsOfInterests.FirstOrDefault(i => i.Id == id);
			if (cityPoi is null)
				return NotFound();

			cityPoi.Name = updateData.Name;
			cityPoi.Description = updateData.Description;

			return NoContent();
		}
	}
}
