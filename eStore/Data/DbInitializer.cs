using BusinessObject.Data;
using BusinessObject;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }

            if (!_roleManager.RoleExistsAsync(SD.AdminRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.AdminRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.UserRole)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new IdentityUser
                {
                    UserName = "admin@estore.com",
                    Email = "admin@estore.com",
                    EmailConfirmed = true
                }, "admin@@").GetAwaiter().GetResult();

                IdentityUser user = _db.Users.SingleOrDefault(x => x.Email == "admin@estore.com");
                _userManager.AddToRoleAsync(user, SD.AdminRole).GetAwaiter().GetResult();
            }
            return;
        }
    }
}
