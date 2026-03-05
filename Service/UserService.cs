using AutoMapper;
using DTOs;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using Zxcvbn;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;


        public UserService(IUserRepository userRepository, IPasswordService passwordService, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _mapper = mapper;
        }

        public async Task<UserReadDTO> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            return _mapper.Map<UserReadDTO>(user);
        }

        public async Task<UserReadDTO> AddUser(UserRegisterDTO userRegisterDto)
        {
            if (!_passwordService.IsPasswordStrong(userRegisterDto.Password))
            {
                throw new Exception("הסיסמה חלשה מדי. נסה לשלב אותיות, מספרים ותווים מיוחדים.");
            }

            if (await _userRepository.IsEmailExistsAsync(userRegisterDto.Email))
            {
                throw new Exception("כתובת האימייל כבר קיימת במערכת.");
            }

            User user = _mapper.Map<User>(userRegisterDto);
            User newUser = await _userRepository.AddUser(user);
            return _mapper.Map<UserReadDTO>(newUser);
        }

        public async Task<UserReadDTO> LoginUser(UserLoginDTO userLoginDto)
        {
            User userCredentials = _mapper.Map<User>(userLoginDto);
            User user = await _userRepository.LoginUser(userCredentials);
            return _mapper.Map<UserReadDTO>(user);
        }

        public async Task<bool> UpdateUser(int id, UserUpdateDTO userUpdateDto)
        {
            User user = _mapper.Map<User>(userUpdateDto);
            await _userRepository.UpdateUser(id, user);
            return true;
        }

    }
}
