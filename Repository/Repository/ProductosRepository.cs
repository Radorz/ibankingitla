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

            return await base._context.ProductosUsers.Where(a=>(a.Idtipo==1)&&(a.Idusuario==id)).ToListAsync();
        }
        public async Task<List<ProductosUsers>> GetAllTarjetas(string id)
        {

            return await base._context.ProductosUsers.Where(a =>( a.Idtipo == 3) && (a.Idusuario == id)).ToListAsync();
        }
        public async Task<List<ProductosUsers>> GetAllPrestamos(string id)
        {

            return await base._context.ProductosUsers.Where(a => (a.Idtipo == 2) && (a.Idusuario == id)).ToListAsync();
        }





    }
}
