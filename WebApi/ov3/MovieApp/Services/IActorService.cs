using MovieApp.Dtos.Actors;
using MovieApp.Enums;
using MovieApp.Models;

namespace MovieApp.Services;

public interface IActorService
{
	Task<IEnumerable<ActorDto>> GetActors(string? name);
	Task<ActorDetailedDto?> GetActorById(int id);
	Task DeleteActor(int id);
	Task<UpdateActorResult> UpdateActor(int id, ActorUpdateDto updateData);
	Task<Actor> CreateActor(ActorCreationDto newActorData);
}