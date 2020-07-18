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
    public class TransferenciasRepository: RepositoryBase<Transacciones, IbankingContext> 
    {
        public TransferenciasRepository(IbankingContext context) : base(context)
        {

        }
        public async Task<int> GetCOUNTtoday()
        {

            
              var pagos= await  base._context.Transacciones.FromSqlRaw("select * from Transacciones where cast(Fecha as date) = cast(getdate() as date)").ToListAsync();
            return pagos.Where(a => a.Id == a.Id).Count();
        }
        public async Task<int> GetCOUNTall()
        {


            var user = await base._context.Transacciones.ToListAsync();
            return user.Where(a =>  a.Id == a.Id).Count();
        }
    }
}
