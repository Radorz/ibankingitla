using Database.Models;
using Microsoft.EntityFrameworkCore;
using Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ProductosUsersRepository : RepositoryBase<ProductosUsers, IbankingContext>
    {
        public ProductosUsersRepository(IbankingContext context) : base(context)
        {

        }
        public async Task<int> GetCOUNT()
        {

            return await base._context.ProductosUsers.CountAsync();
 }







}
}
