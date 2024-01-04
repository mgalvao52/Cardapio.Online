using Cardapio.Application.DTOs;
using Cardapio.DB.Entiites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cardapio.API
{
    public class UserService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public UserService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Login(TableDTO tableDTO)
        {
            UserEntity userEntity = await _userManager.Users.FirstOrDefaultAsync(s => s.TableNumber == tableDTO.Number);
            if (userEntity != null)
            {
                await _signInManager.SignInAsync(userEntity, true);
                return true;
            }
            return false;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IEnumerable<TableDTO>> GetTablesAsync()
        {
            var list = new List<TableDTO>();
            try
            {
                var users = await _userManager.Users.ToListAsync();
                foreach (var item in users)
                {
                    list.Add(new TableDTO { Number = item.TableNumber });
                }

            }
            catch (Exception)
            {
            }
            return list;
        }
    }
}
