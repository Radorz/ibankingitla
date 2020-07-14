using Database.Models;
using Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public class AdminRepository: RepositoryBase<Users, IbankingContext> 
    {
        public AdminRepository(IbankingContext context) : base(context)
        {

        }
    }
}
