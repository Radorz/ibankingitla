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
    public class BeneficiariosRepository : RepositoryBase<Beneficiarios, IbankingContext> 
    {
        public BeneficiariosRepository(IbankingContext context) : base(context)
        {

        }
        
    }
}
