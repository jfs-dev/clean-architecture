using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using clean_architecture.Application.Interfaces;
using clean_architecture.Application.Services;
using clean_architecture.Domain.Models;
using clean_architecture.Domain.Interfaces;
using clean_architecture.Infrastructure.Data;
using clean_architecture.Infrastructure.Repositories;
using clean_architecture.Shared.Helpers;
using clean_architecture.Shared.Validators;

var serviceProvider = new ServiceCollection()
    .AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("clean-architecture"))
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IUserService, UserService>()
    .AddScoped<IEmailValidator, EmailValidator>()
    .BuildServiceProvider();

var userService = serviceProvider.GetService<IUserService>() ?? throw new Exception ("Falha ao obter o serviço UserService!");
var emailValidator = serviceProvider.GetService<IEmailValidator>() ?? throw new Exception ("Falha ao obter o serviço EmailValidator!");

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Create User");
Console.WriteLine("-----------");

var createUserPeterParker = new User(emailValidator) { Name = "Peter Parker", Email = "peter.parker@marvel.com" };
var createUserBenParker = new User(emailValidator) { Name = "Ben Parker", Email = "ben.parker@marvel.com" };
var createUserMaryJane = new User(emailValidator) { Name = "Mary Jane", Email = "mary.jane@marvel.com" };

var createdUserPeterParker = userService.CreateUser(createUserPeterParker);
var createdUserBenParker = userService.CreateUser(createUserBenParker);
var createdUserMaryJane = userService.CreateUser(createUserMaryJane);

Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine($"User created - { createdUserPeterParker.Id } - { createdUserPeterParker.Name } - { createdUserPeterParker.Email }");
Console.WriteLine($"User created - { createdUserBenParker.Id } - { createdUserBenParker. Name } - { createdUserBenParker.Email }");
Console.WriteLine($"User created - { createdUserMaryJane.Id } - { createdUserMaryJane.Name } - { createdUserMaryJane.Email }");
Console.WriteLine("");

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Update User");
Console.WriteLine("-----------");

var updateUserMaryJane = new User(emailValidator) {Id = createdUserMaryJane.Id, Name = "Mary Jane Watson", Email = createdUserMaryJane.Email};
var updatedUserMaryJane = userService.UpdateUser(updateUserMaryJane);

Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine($"User updated - { updatedUserMaryJane.Id } - { updatedUserMaryJane.Name } - { updatedUserMaryJane.Email }");
Console.WriteLine("");

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Delete User");
Console.WriteLine("-----------");

if (userService.DeleteUser(createdUserBenParker.Id))
{
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine($"User deleted - { createdUserBenParker.Id } - { createdUserBenParker.Name } - { createdUserBenParker.Email }");
    Console.WriteLine("");
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Get Users");
Console.WriteLine("---------");
Console.ForegroundColor = ConsoleColor.Magenta;

var returnedUser = userService.GetUserById(createUserPeterParker.Id);
if (returnedUser is not null) Console.WriteLine($"Returned user - { returnedUser.Id } - { returnedUser.Name } - { returnedUser.Email }");
Console.WriteLine("");

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Get All Users");
Console.WriteLine("-------------");
Console.ForegroundColor = ConsoleColor.Magenta;

var returnedUsers = userService.GetAllUsers();

foreach (var currentUser in returnedUsers)
{
    Console.WriteLine($"{ currentUser.Id } - { StringHelper.Truncate(currentUser.Name, 5) } - { currentUser.Email }");
}