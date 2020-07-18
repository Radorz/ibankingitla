using Database.Models;
using Microsoft.EntityFrameworkCore;
using Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class AdminRepository: RepositoryBase<Users, IbankingContext> 
    {
        public AdminRepository(IbankingContext context) : base(context)
        {

        }
        public async Task<int> GetCOUNTactive()
        {

            
              var user=  await base._context.Users.ToListAsync();
            return user.Where(a => a.Estado.Trim() == "Activo" && a.Tipo.Trim() == "cliente").Count();
        }
        public async Task<int> GetCOUNTinactive()
        {


            var user = await base._context.Users.ToListAsync();
            return user.Where(a => a.Estado.Trim() == "Inactivo" && a.Tipo.Trim() == "cliente").Count();
        }
        public async Task<bool> Deletenew(string id)
        {
            var entity = await _context.Users.FindAsync(id);
            if (entity == null)
            {

                return false;
            }
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
            return true;


        }
        public async Task<bool> statechange(string id)
        {
            var entity = await _context.Users.FindAsync(id);
            if (entity == null)
            {

                return false;
            }
            if (entity.Estado.Trim() == "Inactivo")
            {
                entity.Estado = "Activo";
                _context.Users.Update(entity);
                await _context.SaveChangesAsync();
                return true;

            }
            entity.Estado = "Inactivo";
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
            return true;


        }
        public async Task<Users> GetbyIdNew(string id)
        {
            return await _context.Set<Users>().FindAsync(id);
        }
    }
}
