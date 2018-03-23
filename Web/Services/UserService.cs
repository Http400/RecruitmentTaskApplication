using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories;
using System;
using DAL.Extensions;
using Web.Services.Interfaces;
using Web.Utils;
using System.Security.Principal;
using System.Collections.Generic;
using Web.Models;

namespace Web.Services
{
    public class UserService : IUserService
    {
        private readonly IEntityBaseRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IEntityBaseRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public string ValidateUser(string email, string password)
        {
            var user = _userRepository.GetSingleByEmail(email);
            if (user != null && isUserValid(user, password))
            {
                var token = JwtManager.GenerateToken(user);
                return token;
            }
            else
                return null;
        }

        public User CreateUser(SignUpDTO signUpData)
        {
            var existingUser = _userRepository.GetSingleByEmail(signUpData.Email);

            if (existingUser != null)
            {
                throw new UserAlreadyExistsException();
            }

            var passwordSalt = Encryption.CreateSalt();

            var user = new User()
            {
                Salt = passwordSalt,
                Email = signUpData.Email,
                HashedPassword = Encryption.EncryptPassword(signUpData.Password, passwordSalt),
                Name = signUpData.Name,
                Surname = signUpData.Surname,
                PhoneNumber = signUpData.PhoneNumber,
                Address = signUpData.Address
            };

            _userRepository.Add(user);
            _unitOfWork.Commit();

            return user;
        }

        public int GetUserId(IIdentity identity)
        {
            string email = identity.Name;
            if (!String.IsNullOrWhiteSpace(email))
            {
                var userId = _userRepository.GetSingleByEmail(email).ID;
                return userId;
            }
            else
            {
                throw new Exception("Passed empty email.");
            }
        }

        public List<UserDTO> GetUsers()
        {
            var users = _userRepository.GetAll();
            var result = new List<UserDTO>();

            foreach (var user in users)
            {
                result.Add(new UserDTO()
                {
                    ID = user.ID,
                    Email = user.Email,
                    Name = user.Name,
                    Surname = user.Surname,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address
                });
            }

            return result;
        }

        private bool isPasswordValid(User user, string password)
        {
            return string.Equals(Encryption.EncryptPassword(password, user.Salt), user.HashedPassword);
        }

        private bool isUserValid(User user, string password)
        {
            if (isPasswordValid(user, password))
            {
                return true;
                //return user.EmailConfirmed;
            }

            return false;
        }
    }
}