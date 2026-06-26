using MovieApp.Dtos.Actors;
using MovieApp.Enums;
using MovieApp.Models;
using MovieApp.Results;

namespace MovieApp.Services;

public interface IActorService
{
	Task<PaginatedResult<ActorDto>> GetActors(string? name, int page, int itemsPerPage);
	Task<ActorDetailedDto?> GetActorById(int id);
	Task DeleteActor(int id);
	Task<UpdateActorResult> UpdateActor(int id, ActorUpdateDto updateData);
	Task<Actor> CreateActor(ActorCreationDto newActorData);
}