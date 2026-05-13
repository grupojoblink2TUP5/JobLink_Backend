using Application.DTOs.User.Request;
using Application.DTOs.User.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public List<UserResponse> GetAllUsers()
        {
            return _repository
                .GetAll()
                .Select(user => new UserResponse(
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.RegistrationDate,
                    user.Status,
                    user.Role
                ))
                .ToList();
        }

        public UserResponse GetUserById(int id)
        {
            var user = _repository.GetById(id);

            if (user == null)
            {
                throw new NotFoundException($"User not found for id = {id}");
            }

            return MapToResponse(user);
        }

        public UserResponse CreateUser(CreateUserRequest request)
        {
            var user = new User(
                request.FirstName!,
                request.LastName!,
                request.Email!,
                request.Password!,
                request.Role!
            );

            _repository.Create(user);

            _repository.SaveChanges();

            return MapToResponse(user);
        }

        public UserResponse UpdateUser(int id, UpdateUserRequest request)
        {
            var user = _repository.GetById(id);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.Update(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password
            );

            _repository.Update(user);

            _repository.SaveChanges();

            return MapToResponse(user);
        }

        public UserResponse RemoveUser(int id)
        {
            var user = _repository.GetById(id);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.Deactivate();

            _repository.Update(user);

            _repository.SaveChanges();

            return MapToResponse(user);
        }

        public UserResponse AddUser(int id)
        {
            var user = _repository.GetById(id);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.Activate();

            _repository.Update(user);

            _repository.SaveChanges();

            return MapToResponse(user);
        }

        private static UserResponse MapToResponse(User user)
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