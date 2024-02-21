using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Intercambiar_mvc_v1.Models
{
    public class UsuarioRepositorio
    {
        private UsuarioContexto db = new UsuarioContexto();

        public bool AddUsuario(Usuario Usuario)
        {
            try
            {


                //Cadastrar Usuário
                SqlConnection cn = new SqlConnection(@"Data Source=mssql.intercambiar.com.br;Initial Catalog=intercambiar;User ID=intercambiar;Password=carro1818;MultipleActiveResultSets=True;Application Name=EntityFramework");

                using (SqlCommand cmd = new SqlCommand("p_Usuario_Add", cn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter parm = new SqlParameter("@IdLogin", SqlDbType.Int);
                    parm.Value = Usuario.IdLogin;
                    parm.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm);

                    SqlParameter parm2 = new SqlParameter("@Email", SqlDbType.VarChar);
                    parm2.Value = Usuario.Email;
                    parm2.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm2);

                    
                    SqlParameter parm3 = new SqlParameter("@Nome", SqlDbType.VarChar, 500);
                    parm3.Value = Usuario.Nome;
                    parm3.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm3);
                    
                    SqlParameter parm4 = new SqlParameter("@locate", SqlDbType.VarChar, 50);
                    parm4.Value = Usuario.locale ;
                    parm4.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm4);

                    SqlParameter parm5 = new SqlParameter("@sexo", SqlDbType.Char, 1);
                    parm5.Value = Usuario.sexo;
                    parm5.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm5);

                    SqlParameter parm6 = new SqlParameter("@Endereco", SqlDbType.VarChar, 500);
                    parm6.Value = Usuario.Endereco;
                    parm6.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm6);

                    SqlParameter parm7 = new SqlParameter("@CEP", SqlDbType.VarChar, 500);
                    parm7.Value = Usuario.CEP;
                    parm7.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm7);

                    SqlParameter parm8 = new SqlParameter("@Cidade", SqlDbType.VarChar, 500);
                    parm8.Value = Usuario.Cidade;
                    parm8.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm8);
     
                    SqlParameter parm9 = new SqlParameter("@Estado", SqlDbType.VarChar, 2);
                    parm9.Value = Usuario.Estado;
                    parm9.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm9);

                    SqlParameter parm10 = new SqlParameter("@Fone", SqlDbType.VarChar, 50);
                    parm10.Value = Usuario.Fone;
                    parm10.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm10);

                    SqlParameter parm11 = new SqlParameter("@Celular", SqlDbType.VarChar, 50);
                    parm11.Value = Usuario.Celular;
                    parm11.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm11);

                    SqlParameter parm12 = new SqlParameter("@Escolaridade", SqlDbType.VarChar, 50);
                    parm12.Value = Usuario.Escolaridade;
                    parm12.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm12);
					
                    SqlParameter parm13 = new SqlParameter("@Primeiro_Nome", SqlDbType.VarChar, 50);
                    parm13.Value = Usuario.Primeiro_Nome;
                    parm13.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm13);
                
                    SqlParameter parm14 = new SqlParameter("@Sobrenome", SqlDbType.VarChar, 50);
                    parm14.Value = Usuario.Sobrenome;
                    parm14.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm14);
					
                    SqlParameter parm15 = new SqlParameter("@DtNasc", SqlDbType.VarChar, 50);
                    parm15.Value = Usuario.DtNasc;
                    parm15.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm15);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }


                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }

        public bool CadastroUsuario()
        {
            return true;
        }

        public Usuario SelUsuario(int idLogin)
        {
            //List<Usuario> Lista;



            //Lista = db.Usuario.Where(T => T.IdLogin == idLogin).ToList();
            //if (Lista.Count > 0)
            //{
            //    return Lista.First();
            //}
            //else
            //{
            //    return null;
            //}

            Usuario U = new Usuario();
            //Cadastrar Usuário
            SqlConnection cn = new SqlConnection(@"Data Source=mssql.intercambiar.com.br;Initial Catalog=intercambiar;User ID=intercambiar;Password=carro1818;MultipleActiveResultSets=True;Application Name=EntityFramework");

            using (SqlCommand cmd = new SqlCommand("p_Usuario_SEL", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parm = new SqlParameter("@IdLogin", SqlDbType.Int);
                parm.Value = idLogin;
                parm.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(parm);
                cn.Open();
                SqlDataReader myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    IDataRecord record = (IDataRecord)myReader;
                    
                    U.Id = Int32.Parse(record["Id"].ToString());
                    U.IdLogin = Int32.Parse(record["IdLogin"].ToString());
                    U.Email = record["Email"].ToString();
                    U.Nome = record["Nome"].ToString();
                    U.Endereco = record["Endereco"].ToString();
                    U.CEP = record["CEP"].ToString();
                    U.Cidade = record["Cidade"].ToString();
                    U.Estado = record["Estado"].ToString();
                    U.Fone = record["Fone"].ToString();
                    U.Celular = record["Celular"].ToString();
                    U.Escolaridade = record["Escolaridade"].ToString();
                    U.Foto = record["Foto"].ToString();
                    U.Facebooku = record["Facebooku"].ToString();
                    U.sexo = record["sexo"].ToString();
                    U.locale = record["locale"].ToString();
                    U.dtCadastro = DateTime.Parse(record["dtCadastro"].ToString());
                    U.dtAtualizacao = DateTime.Parse(record["dtAtualizacao"].ToString());
                    U.Primeiro_Nome = record["Primeiro_Nome"].ToString();
                    U.Sobrenome = record["Sobrenome"].ToString();
                    U.DtNasc = DateTime.Parse(record["DtNasc"].ToString());

                };
                cn.Close();

            }

            return U;


        }

    }
}