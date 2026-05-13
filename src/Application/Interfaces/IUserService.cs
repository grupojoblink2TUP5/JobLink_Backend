using Application.DTOs.User.Request;
using Application.DTOs.User.Response;

namespace Application.Interfaces;

public interface IUserService
{
    List<UserResponse> GetAllUsers();

    UserResponse? GetUserById(int id);

    UserResponse CreateUser(CreateUserRequest request);

    UserResponse UpdateUser(int id, UpdateUserRequest request);


    UserResponse AddUser(int id); //Para cambiar el status ool al usuario a true

    UserResponse RemoveUser(int id); //Para cambiar el status al usuario a false

}