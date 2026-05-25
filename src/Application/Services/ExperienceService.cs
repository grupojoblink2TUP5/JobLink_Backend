using Application.DTOs.Experience.Request;
using Application.DTOs.Experience.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class ExperienceService : IExperienceService
    {
        private readonly IExperienceRepository _repository;

        public ExperienceService(IExperienceRepository repository)
        {
            _repository = repository;
        }

        public List<ExperienceResponse> GetAllExperiences()
        {
            return _repository
                .GetAll()
                .Select(experience => new ExperienceResponse(
                    experience.Id,
                    experience.CompanyName,
                    experience.Position,
                    experience.StartDate,
                    experience.EndDate,
                    experience.Description,
                    experience.CandidateId
                ))
                .ToList();
        }

        public ExperienceResponse GetExperienceById(int id)
        {
            var experience = _repository.GetById(id);

            if (experience == null)
            {
                throw new NotFoundException($"Experience not found for id = {id}");
            }

            return MapToResponse(experience);
        }

        public ExperienceResponse GetExperienceByCandidateId(int candidateId)
        {
            var experience = _repository.GetByCandidateId(candidateId);

            if (experience == null)
            {
                throw new NotFoundException($"Experience not found for candidate id = {candidateId}");
            }

            return MapToResponse(experience);
        }

        public ExperienceResponse CreateExperience(CreateExperienceRequest request)
        {
            var experience = new Experience(
                request.CompanyName!,
                request.Position!,
                request.StartDate,
                request.EndDate,
                request.Description!,
                request.CandidateId
            );

            _repository.Create(experience);

            _repository.SaveChanges();

            return MapToResponse(experience);
        }

        public ExperienceResponse UpdateExperience(int id, UpdateExperienceRequest request)
        {
            var experience = _repository.GetById(id);

            if (experience == null)
            {
                throw new NotFoundException($"Experience not found for id = {id}");
            }

            experience.Update(
                request.CompanyName,
                request.Position,
                request.StartDate,
                request.EndDate,
                request.Description
            );

            _repository.Update(experience);

            _repository.SaveChanges();

            return MapToResponse(experience);
        }

        public bool DeleteExperience(int id)
        {
            var experience = _repository.GetById(id);

            if (experience == null)
            {
                return false;
            }

            _repository.Delete(experience);

            _repository.SaveChanges();

            return true;
        }

        private static ExperienceResponse MapToResponse(Experience experience)
        {
            return new ExperienceResponse(
                experience.Id,
                experience.CompanyName,
                experience.Position,
                experience.StartDate,
                experience.EndDate,
                experience.Description,
                experience.CandidateId
            );
        }
    }
}

