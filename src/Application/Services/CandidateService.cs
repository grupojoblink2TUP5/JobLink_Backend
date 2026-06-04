using Application.DTOs.Candidate.Request;
using Application.DTOs.Candidate.Response;
using Application.DTOs.User.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _repository;
        private readonly IUserRepository _userRepository;

        public CandidateService(ICandidateRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public List<CandidateResponse> GetAllCandidates()
        {
            return _repository
                .GetAll()
                .Select(candidate => new CandidateResponse(
                    candidate.Id,
                    candidate.UserId,
                    MapToUserResponse(candidate.User)
                ))
                .ToList();
        }

        public CandidateResponse? GetCandidateById(int id)
        {
            var candidate = _repository.GetById(id);

            if (candidate == null)
            {
                return null;
            }

            return new CandidateResponse(
                candidate.Id,
                candidate.UserId,
                MapToUserResponse(candidate.User)
            );
        }

        public CandidateResponse? GetCandidateByUserId(int userId)
        {
            var candidate = _repository.GetByUserId(userId);

            if (candidate == null)
            {
                return null;
            }

            return new CandidateResponse(
                candidate.Id,
                candidate.UserId,
                MapToUserResponse(candidate.User)
            );
        }

        public CandidateResponse CreateCandidate(CreateCandidateRequest request)
        {
            var user = _userRepository.GetById(request.UserId);

            if (user == null)
            {
                throw new NotFoundException($"User not found for id = {request.UserId}");
            }

            var candidate = new Candidate(user); // ✅

            _repository.Create(candidate);
            _repository.SaveChanges();

            return new CandidateResponse(
                candidate.Id,
                candidate.UserId,
                MapToUserResponse(candidate.User)
            );
        }

        public CandidateResponse UpdateCandidate(int id, UpdateCandidateRequest request)
        {
            var candidate = _repository.GetById(id);

            if (candidate == null)
            {
                throw new NotFoundException($"Candidate not found for id = {id}");
            }

            var user = _userRepository.GetById(request.UserId);

            if (user == null)
            {
                throw new NotFoundException($"User not found for id = {request.UserId}");
            }

            candidate.Update(user); // ✅

            _repository.Update(candidate);
            _repository.SaveChanges();

            return new CandidateResponse(
                candidate.Id,
                candidate.UserId,
                MapToUserResponse(candidate.User)
            );
        }

        public bool DeleteCandidate(int id)
        {
            var candidate = _repository.GetById(id);

            if (candidate == null)
            {
                return false;
            }

            _repository.Delete(candidate);
            _repository.SaveChanges();

            return true;
        }

        private UserResponse MapToUserResponse(User user)
        {
            return new UserResponse(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.RegistrationDate,
                user.Status,
                user.Role
            );
        }
    }
}

