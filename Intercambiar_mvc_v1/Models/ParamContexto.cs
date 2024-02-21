using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Intercambiar_mvc_v1.Models
{
    public class ParamContexto : DbContext
    {
        public ParamContexto()
        {
            this.Database.Connection.ConnectionString = "Data Source=mssql.intercambiar.com.br;Initial Catalog=intercambiar;User ID=intercambiar;Password=carro1818;MultipleActiveResultSets=True;Application Name=EntityFramework";
        }

        public DbSet<Param> Param { get; set; }
    }
}