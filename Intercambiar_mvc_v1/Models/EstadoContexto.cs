using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.Entity;

namespace Intercambiar_mvc_v1.Models
{
    public class EstadoContexto : DbContext
    {
        public EstadoContexto()
        {
            this.Database.Connection.ConnectionString = "Data Source=mssql.intercambiar.com.br;Initial Catalog=intercambiar;User ID=intercambiar;Password=carro1818;MultipleActiveResultSets=True;Application Name=EntityFramework";
        }

        public DbSet<Estado> Estado { get; set; }
   
    }
}