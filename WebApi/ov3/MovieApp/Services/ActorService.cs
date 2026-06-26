using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Dtos.Actors;
using MovieApp.Dtos.Movies;
using MovieApp.Enums;
using MovieApp.Models;
using MovieApp.Results;

namespace MovieApp.Services;

public class ActorService(AppDbContext context) : IActorService
{
	private AppDbContext _context = context;

	public async Task<Actor> CreateActor(ActorCreationDto newActorData)
	{
		var res = await _context.Actors.AddAsync(new Actor()
		{
			ImageURL = newActorData.ImageURL,
			Name = newActorData.Name,
			BirthDate = newActorData.BirthDate
		});

		await _context.SaveChangesAsync();

		var actorDb = res.Entity;

		return actorDb;
	}

	public async Task DeleteActor(int id)
	{
		await _context.Actors
			.Where(a => a.Id == id)
			.ExecuteDeleteAsync();
	}

	public async Task<ActorDetailedDto?> GetActorById(int id)
	{
		var actor = await _context.Actors
			.Where(a => a.Id == id)
			.Select(a => new ActorDetailedDto(
				a.Id,
				a.Name,
				a.ImageURL,
				a.BirthDate,
				a.Movies.Select(m => new MovieDto(
					m.Id,
					m.Title,
					m.Year,
					m.ImageURL,
					m.Genre.Name,
					m.Genre.ChildrenSafe
				)).ToList()
			)).FirstOrDefaultAsync();

		return actor;
	}

	public async Task<PaginatedResult<ActorDto>> GetActors(string? name, int page, int itemsPerPage)
	{
		var query = _context.Actors.AsQueryable();

		if (!string.IsNullOrWhiteSpace(name))
		{
			query = query.Where(a => EF.Functions.Like(a.Name, $"%{name}%"));
		}

		var numberOfActors = await query.CountAsync();
		var actors = await query
			.Skip(itemsPerPage * (page - 1))
			.Take(itemsPerPage)
			.Select(a => new ActorDto(a.Id, a.Name, a.ImageURL, a.BirthDate))
			.ToListAsync();

		return new PaginatedResult<ActorDto>()
		{
			Data = actors,
			Pagination = new(page, itemsPerPage, numberOfActors)
		};
	}

	public async Task<UpdateActorResult> UpdateActor(int id, ActorUpdateDto updateData)
	{
		// checks
		var actor = await _context.Actors
			.Include(a => a.Movies)
			.FirstOrDefaultAsync(a => a.Id == id);

		if (actor is null)
			return UpdateActorResult.ActorNotFound;

		var uniqueUpdateMovies = updateData.Movies.Distinct().ToList();
		var moviesFromDb = await _context.Movies
			.Where(m => uniqueUpdateMovies.Contains(m.Id))
			.ToListAsync();
		if (uniqueUpdateMovies.Count != moviesFromDb.Count)
			return UpdateActorResult.InvalidMovies;

		// update data
		actor.Movies.Clear();
		foreach (Movie movie in moviesFromDb)
		{
			actor.Movies.Add(movie);
		}

		actor.BirthDate = updateData.BirthDate;
		actor.Name = updateData.Name;
		actor.ImageURL = updateData.ImageURL;

		await _context.SaveChangesAsync();

		return UpdateActorResult.Success;
	}
}