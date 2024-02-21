using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intercambiar_mvc_v1.Models
{

    public class LogLoginRepositorio
    {
        private LogLoginContexto db = new LogLoginContexto();

        public bool AddLogLogin(LogLogin LogLogin)
        {
            try
            {

                db.LogLogin.Add(LogLogin);
                db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }

        public LogLogin SelLogLogin(int Id_Login)
        {
            List<LogLogin> Lista;

            Lista = db.LogLogin.Where(T => T.Id_Login == Id_Login).ToList();
            if (Lista.Count > 0)
            {
                return Lista.First();
            }
            else
            {
                return null;
            }
        }

    }
}