using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApiFirst.Models;

namespace WebApiFirst.Controllers
{
	[ApiController]
	[Route("api/cities/{cityId:int}/pointsOfInterests")]
	public class PointsOfInterests : ControllerBase
	{
		private readonly ILogger<PointsOfInterests> _logger;

		public PointsOfInterests(ILogger<PointsOfInterests> logger)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[HttpGet()]
		[EndpointSummary("All points of interests of a city")]
		public ActionResult<IEnumerable<PointsOfInterestDto>> GetPointsOfInterests(int cityId)
		{
			try
			{
				// check if city exists
				var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

				if (city is null)
					return NotFound();

				return Ok(city.PointsOfInterests);
			}
			catch (Exception ex)
			{
				_logger.LogCritical("Something went critically wrong", ex);

				return StatusCode(500, "Wrong wrong wrong!");
			}
		}


		[HttpGet("{pointOfInterestId:int}", Name = "GetPof")]
		[EndpointSummary("Specific point of interest of a city")]
		public ActionResult<PointsOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
		{
			// check if city exists
			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

			if (city is null)
			{
				_logger.LogInformation($"City with id {cityId} wasn't found.");
				return NotFound();
			}

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


		[HttpPatch("{id:int}")]
		public ActionResult PartialUpdatePointOfInterest(int cityId, int id, JsonPatchDocument<PointsOfInterestsPartialUpdateDto> patchData)
		{
			// try to get city
			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

			if (city is null)
				return NotFound();

			// try to get the poi
			var poiFromStore = city.PointsOfInterests.FirstOrDefault(p => p.Id == id);
			if (poiFromStore is null)
				return NotFound();

			var pointsOfInterestsToPatch = new PointsOfInterestsPartialUpdateDto() { Name = poiFromStore.Name, Description = poiFromStore.Description };

			patchData.ApplyTo(pointsOfInterestsToPatch, ModelState);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			poiFromStore.Name = pointsOfInterestsToPatch.Name;
			poiFromStore.Description = pointsOfInterestsToPatch.Description;

			return NoContent();
		}


		[HttpDelete("{id:int}")]
		public ActionResult DeletePointOfInterest(int cityId, int id)
		{
			// try to get city
			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

			if (city is null)
				return NotFound();

			// try to get the poi
			var poi = city.PointsOfInterests.FirstOrDefault(p => p.Id == id);
			if (poi is null)
				return NotFound();


			city.PointsOfInterests.Remove(poi);

			return NoContent();

		}


	}
}
