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
    public class ProductosRepository : RepositoryBase<ProductosUsers, IbankingContext>
    {
        public ProductosRepository(IbankingContext context) : base(context)
        {

        }
        public async Task<int> GetCOUNT()
        {

            return await base._context.ProductosUsers.CountAsync();
        }

        public async Task<List<ProductosUsers>> GetAllCuentas(string id)
        {

            return await base._context.ProductosUsers.Where(a => (a.Idtipo == 1) && (a.Idusuario == id)).ToListAsync();
        }
        public async Task<List<ProductosUsers>> GetAllCuentaForVerify()
        {

            return await base._context.ProductosUsers.Where(a => (a.Idtipo == 1)).ToListAsync();
        }
        public async Task<List<ProductosUsers>> GetAllTarjetas(string id)
        {

            return await base._context.ProductosUsers.Where(a => (a.Idtipo == 3) && (a.Idusuario == id)).ToListAsync();
        }
        public async Task<List<ProductosUsers>> GetAllPrestamos(string id)
        {

            return await base._context.ProductosUsers.Where(a => (a.Idtipo == 2) && (a.Idusuario == id)).ToListAsync();
        }


        public async Task<ProductosUsers> DeleteNew(string id)
        {
            var entity = await _context.Set<ProductosUsers>().FindAsync(id);
            if (entity == null)
            {

                return entity;
            }
            _context.Set<ProductosUsers>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;


        }
        public async Task<ProductosUsers> GetbyIdnew(string id)
        {
            return  _context.Set<ProductosUsers>().FirstOrDefault(a=> a.Id.Trim()==id.ToString().Trim());
        }
        public async Task<List<ProductosUsers>> deleteallproductuser (string id)
        {
             var products = await base._context.ProductosUsers.Where(a => a.Idusuario.Trim() == id.Trim()).ToListAsync();
            foreach(var product in products)
            {

                _context.Set<ProductosUsers>().Remove(product);


            }
            await _context.SaveChangesAsync();

            return products;
        }
       

    }
}
