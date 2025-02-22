﻿using Database.Models;
using Microsoft.EntityFrameworkCore;
using Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class PagosRepository: RepositoryBase<Pagos, IbankingContext> 
    {
        public PagosRepository(IbankingContext context) : base(context)
        {

        }
        public async Task<int> GetCOUNTtoday()
        {

            
              var pagos= await  base._context.Pagos.FromSqlRaw("select * from Pagos where cast(Fecha as date) = cast(getdate() as date)").ToListAsync();
            return pagos.Where(a => a.Id == a.Id).Count();
        }
        public async Task<int> GetCOUNTall()
        {


            var user = await base._context.Pagos.ToListAsync();
            return user.Where(a =>  a.Id == a.Id).Count();
        }
    }
}
