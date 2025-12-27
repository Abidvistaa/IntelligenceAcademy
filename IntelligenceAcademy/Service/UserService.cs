using IntelligenceAcademy.Model;
using IntelligenceAcademy.Repository;
using IntelligenceAcademy.ViewModel;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static IntelligenceAcademy.Common.CustomError;

namespace IntelligenceAcademy.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User obj);
        Task UpdateAsync(int id, User obj);
        Task DeleteAsync(int id);
        Task<LoginViewModel> LoginAsync(LoginViewModel obj);
    }
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                var list = await _userRepository.GetAllAsync();

                if (list == null || !list.Any())
                    throw new AppCustomException("No users found.");

                return list;
            }
            catch(AppCustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve user list: ", ex);
            }
        }
        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);

                if (user == null)
                    throw new KeyNotFoundException($"User with ID {id} not found.");

                return user;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to retrieve user: ", ex);
            }
        }
        public async Task AddAsync(User obj)
        {
            try
            {
                var checkExistingUser = (await _userRepository.GetAllAsync())
                   .Where(x => x.Email == obj.Email).Any();

                if (checkExistingUser)
                    throw new AppCustomException($"This email is already exist.");

                obj.AccountType = "Custom";
                obj.ActionDate=DateTime.Now;

                await _userRepository.AddAsync(obj);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the user", ex);
            }
        }

        public async Task UpdateAsync(int id, User model)
        {
            try
            {
                var obj = await _userRepository.GetByIdAsync(id);

                if (obj == null)
                    throw new KeyNotFoundException($"User with ID {id} not found.");

                // Update properties
                obj.FirstName = model.FirstName;
                obj.LastName = model.LastName;
                obj.PhoneNumber = model.PhoneNumber;

                await _userRepository.UpdateAsync(obj);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the user.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var obj = await _userRepository.GetByIdAsync(id);
                if (obj == null)
                    throw new KeyNotFoundException($"User with ID {id} not found.");

                await _userRepository.DeleteAsync(id);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the user: ", ex);
            }
        }
        string GenerateJwtToken(string username)
        {
            var key = "26456dkj[i753(947kdl90rioe]utu9405u9]fg";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var token = new JwtSecurityToken(
                issuer: "IntelligenceAcademy",
                audience: "IntelligenceAcademy",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<LoginViewModel> LoginAsync(LoginViewModel obj)
        {

            try
            {
                var user = (await _userRepository.GetAllAsync()).Where(x => x.Email == obj.Email && x.Password == obj.Password).FirstOrDefault();

                if (user == null)
                    throw new AppCustomException("Invalid email or password");

                var token = GenerateJwtToken(user.Email);

                return new LoginViewModel
                {
                    Email = user.Email,
                    Token = token
                };
            }
            catch (AppCustomException)
            {
                throw; 
            }
            catch (Exception ex)
            {
                throw new Exception("Login failed due to a system error.", ex);
            }
        }
    }
}
