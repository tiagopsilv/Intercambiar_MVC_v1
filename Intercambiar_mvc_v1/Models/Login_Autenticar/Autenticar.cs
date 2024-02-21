using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using Facebook;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using Intercambiar_mvc_v1.Models.Login_Autenticar;

namespace Intercambiar_mvc_v1.Models
{
    public class Autenticar
    {

        private static byte[] chave = { };
        private static byte[] iv = { 12, 34, 56, 78, 90, 102, 114, 126 };

        private static string chaveCriptografia;

        private static string chaveCookie;

        ParamRepositorio Param = new ParamRepositorio();

        public string get_chaveCriptografia()
        {
            return chaveCookie;
        }

        public Autenticar()
        {
            chaveCriptografia = Param.ListaParam("chaveCriptografia");
            chaveCookie = Param.ListaParam("chaveCookie");
        }

        public Usuario GetUsuario(string _Email)
        {
            try
            {
                LoginRepositorio LoginRep = new LoginRepositorio();
                UsuarioRepositorio UsuarioRep = new UsuarioRepositorio();

                Login L_Novo = LoginRep.SelLoginEmail(_Email);
                return UsuarioRep.SelUsuario(L_Novo.ID);
            }
            catch (Exception)
            {
                return null;
            }

        }
        public string AutenticarUsuario(string _Email, string _Password)
        {
            //Criando o contexto de dados para autenticação
            //AdventureWorksEntities ContextoUsuario = new AdventureWorksEntities();

            try
            {

                LoginRepositorio Login = new LoginRepositorio();

                string SenhaCrip = HashValue(_Password).ToString();
                string Mensagem = "";

                Mensagem = Login.ValidaLogin(_Email, SenhaCrip);

                if (Mensagem == "")
                {

                    CriarCookie(_Email);

                    return Mensagem;
                }
                else
                {
                    return Mensagem;
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public bool CriarCookie(string _Email, string TokenFb = null)
        {
             
            try
            {
                LoginRepositorio Login = new LoginRepositorio();

                //Criando um objeto cookie
                HttpCookie UserCookie = new HttpCookie(chaveCookie);

                LogLoginRepositorio LogLogin = new LogLoginRepositorio();

                LogLogin LL = new LogLogin();

                Login L = Login.SelLoginEmail(_Email);

                LL.Id_Login = L.ID;
                LL.Log = DateTime.Now;
                if (TokenFb != null)
                {
                    LL.Token = TokenFb;
                    LL.LogFace = true;
                    LL.getInfo = false;
                }

                LogLogin.AddLogLogin(LL);

                LogLoginRepositorio LogLoginSel = new LogLoginRepositorio();

                LogLogin LSel = LogLoginSel.SelLogLogin(L.ID);

                Random rdn = new Random();

                string value = rdn.Next(100, 200) + "|" + _Email + "|"
                               + rdn.Next(100, 200) + "|"
                               + LSel.Log.Day.ToString() + "|"
                               + rdn.Next(100, 200) + "|"
                               + LSel.Log.Month.ToString() + "|"
                               + rdn.Next(100, 200) + "|"
                               + LSel.Log.Year.ToString() + "|"
                               + rdn.Next(100, 200) + "|"
                               + LSel.Log.ToLongTimeString() + "|"
                               + rdn.Next(100, 200) + "|"
                               + LSel.Log.Millisecond + "|"
                               + rdn.Next(100, 200) + "|";

                //Setando o ID do usuário no cookie
                UserCookie.Value = Criptografar(value);

                //Definindo o prazo de vida do cookie
                UserCookie.Expires = DateTime.Now.AddDays(1);

                //Adicionando o cookie no contexto da aplicação
                HttpContext.Current.Response.Cookies.Add(UserCookie);

                SqlConnection cn = new SqlConnection(@"Data Source=mssql.intercambiar.com.br;Initial Catalog=intercambiar;User ID=intercambiar;Password=carro1818;MultipleActiveResultSets=True;Application Name=EntityFramework");

                String  Sqlcon = "INSERT INTO [dbo].[LogController]([Id_Login],[Controller],[LogController]";
                        Sqlcon += ") VALUES (";
                        Sqlcon += L.ID.ToString();
                        Sqlcon += ", 'Login', getdate()";
                        Sqlcon += ")";

                SqlCommand cmd = new SqlCommand(Sqlcon, cn);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

         //Criptografa o Cookie
         public static string Criptografar(string valor)
         {
             DESCryptoServiceProvider des;
             MemoryStream ms;
             CryptoStream cs; byte[] input;

             try
             {
                 des = new DESCryptoServiceProvider();
                 ms = new MemoryStream();

                 input = Encoding.UTF8.GetBytes(valor); chave = Encoding.UTF8.GetBytes(chaveCriptografia.Substring(0, 8));

                 cs = new CryptoStream(ms, des.CreateEncryptor(chave, iv), CryptoStreamMode.Write);
                 cs.Write(input, 0, input.Length);
                 cs.FlushFinalBlock();

                 return Convert.ToBase64String(ms.ToArray());
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

         //Descriptografa o cookie
         public static string Descriptografar(string valor)
         {
             DESCryptoServiceProvider des;
             MemoryStream ms;
             CryptoStream cs; byte[] input;

             try
             {
                 des = new DESCryptoServiceProvider();
                 ms = new MemoryStream();

                 input = new byte[valor.Length];
                 input = Convert.FromBase64String(valor.Replace(" ", "+"));

                 chave = Encoding.UTF8.GetBytes(chaveCriptografia.Substring(0, 8));

                 cs = new CryptoStream(ms, des.CreateDecryptor(chave, iv), CryptoStreamMode.Write);
                 cs.Write(input, 0, input.Length);
                 cs.FlushFinalBlock();

                 return Encoding.UTF8.GetString(ms.ToArray());
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

        public bool LimparCookie(string Valor)
        {
            try
            {
                if (HttpContext.Current.Response.Cookies[Valor] != null)
                {
                    HttpCookie myCookie = new HttpCookie(Valor);
                    myCookie.Expires = DateTime.Now.AddDays(-1d);
                    HttpContext.Current.Response.Cookies.Add(myCookie);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public bool UsuarioLogado(string Value, string Controller, string ValidaUltContr)
        {
            //Criando um objeto cookie

            if (Value != null)
            {

                bool ResultProcudure = false;
                bool ResultGetInfoFace = false;

                CookieUsr C = GetCookie(Value);


                SqlConnection cn = new SqlConnection(@"Data Source=mssql.intercambiar.com.br;Initial Catalog=intercambiar;User ID=intercambiar;Password=carro1818;MultipleActiveResultSets=True;Application Name=EntityFramework");

                using (SqlCommand cmd = new SqlCommand("p_ValidaChaveCookie", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter parm = new SqlParameter("@Email", SqlDbType.VarChar, 500);
                    parm.Value = C.email;
                    parm.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm);

                    SqlParameter parm2 = new SqlParameter("@DataCookie", SqlDbType.VarChar, 50);
                    parm2.Value = C.Data.ToString("yyyy-MM-dd HH:mm:ss.fff tt");
                    parm2.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm2);

                    SqlParameter parm3 = new SqlParameter("@Controller", SqlDbType.VarChar, 150);
                    parm3.Value = Controller;
                    parm3.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm3);

                    SqlParameter parm4 = new SqlParameter("@IP", SqlDbType.VarChar, 30);
                    parm4.Value = GetUserIP();
                    parm4.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm4);

                    SqlParameter parm5 = new SqlParameter("@ValorIP", SqlDbType.VarChar, 800);
                    parm5.Value = GetLocation(GetUserIP());
                    parm5.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm5);

                    SqlParameter parm6 = new SqlParameter("@ValidaUltContr", SqlDbType.VarChar, 50);
                    parm6.Value = ValidaUltContr;
                    parm6.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm6);

                    SqlParameter parm7 = new SqlParameter("@Valido", SqlDbType.Bit);
                    parm7.Direction = ParameterDirection.Output; // This is important!
                    cmd.Parameters.Add(parm7);

                    SqlParameter parm8 = new SqlParameter("@GetInfoFace", SqlDbType.Bit);
                    parm8.Direction = ParameterDirection.Output; // This is important!
                    cmd.Parameters.Add(parm8);

                    cn.Open();
                    cmd.ExecuteNonQuery();

                    ResultProcudure = Convert.ToBoolean(parm7.Value);
                    ResultGetInfoFace = Convert.ToBoolean(parm8.Value);

                    cn.Close();
                }

                if (ResultProcudure == true)
                {
                    if (ResultGetInfoFace == false)
                    {
                        GravaInfFb(C.email);
                    }
                    return true;
                }
                else
                {
                    LimparCookie(chaveCookie);
                    return false;
                }


            }
            else
            {
                return false;
            }

        }


        public bool UsuarioLogado(string Value, string Controller)
        {
            //Criando um objeto cookie

            if (Value != null)
            {

                bool ResultProcudure = false;
                bool ResultGetInfoFace = false;

                CookieUsr C = GetCookie(Value);


                SqlConnection cn = new SqlConnection(@"Data Source=mssql.intercambiar.com.br;Initial Catalog=intercambiar;User ID=intercambiar;Password=carro1818;MultipleActiveResultSets=True;Application Name=EntityFramework");

                using (SqlCommand cmd = new SqlCommand("p_ValidaChaveCookie", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter parm = new SqlParameter("@Email", SqlDbType.VarChar, 500);
                    parm.Value = C.email;
                    parm.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm);

                    SqlParameter parm2 = new SqlParameter("@DataCookie", SqlDbType.VarChar, 50);
                    parm2.Value = C.Data.ToString("yyyy-MM-dd HH:mm:ss.fff tt");
                    parm2.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm2);

                    SqlParameter parm3 = new SqlParameter("@Controller", SqlDbType.VarChar, 150);
                    parm3.Value = Controller;
                    parm3.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm3);

                    SqlParameter parm4 = new SqlParameter("@IP", SqlDbType.VarChar, 30);
                    parm4.Value = GetUserIP();
                    parm4.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm4);

                    SqlParameter parm5 = new SqlParameter("@ValorIP", SqlDbType.VarChar, 800);
                    parm5.Value = GetLocation(GetUserIP());
                    parm5.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm5);

                    SqlParameter parm6 = new SqlParameter("@ValidaUltContr", SqlDbType.VarChar, 50);
                    parm6.Value = DBNull.Value;
                    parm6.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm6);

                    SqlParameter parm7 = new SqlParameter("@Valido", SqlDbType.Bit);
                    parm7.Direction = ParameterDirection.Output; // This is important!
                    cmd.Parameters.Add(parm7);

                    SqlParameter parm8 = new SqlParameter("@GetInfoFace", SqlDbType.Bit);
                    parm8.Direction = ParameterDirection.Output; // This is important!
                    cmd.Parameters.Add(parm8);

                    cn.Open();
                    cmd.ExecuteNonQuery();

                    ResultProcudure = Convert.ToBoolean(parm7.Value);
                    ResultGetInfoFace = Convert.ToBoolean(parm8.Value);

                    cn.Close();
                }

                if (ResultProcudure == true)
                {
                    if (ResultGetInfoFace == false)
                    {
                        GravaInfFb(C.email);
                    }
                    return true;
                }
                else
                {
                    LimparCookie(chaveCookie);
                    return false;
                }


            }
            else
            {
                return false;
            }

        }

        public bool UsuarioLogado(string Controller)
        {
  
            bool Resut = false;


            SqlConnection cn = new SqlConnection(@"Data Source=mssql.intercambiar.com.br;Initial Catalog=intercambiar;User ID=intercambiar;Password=carro1818;MultipleActiveResultSets=True;Application Name=EntityFramework");

            using (SqlCommand cmd = new SqlCommand("p_ValidaChaveCookie", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parm = new SqlParameter("@Email", SqlDbType.VarChar, 500);
                parm.Value = DBNull.Value;
                parm.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(parm);

                SqlParameter parm2 = new SqlParameter("@DataCookie", SqlDbType.VarChar, 50);
                parm2.Value = DBNull.Value;
                parm2.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(parm2);

                SqlParameter parm3 = new SqlParameter("@Controller", SqlDbType.VarChar, 150);
                parm3.Value = Controller;
                parm3.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(parm3);

                SqlParameter parm4 = new SqlParameter("@IP", SqlDbType.VarChar, 30);
                parm4.Value = GetUserIP();
                parm4.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(parm4);

                SqlParameter parm5 = new SqlParameter("@ValorIP", SqlDbType.VarChar, 800);
                parm5.Value = GetLocation(GetUserIP());
                parm5.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(parm5);

                SqlParameter parm6 = new SqlParameter("@ValidaUltContr", SqlDbType.VarChar, 50);
                parm6.Value = DBNull.Value;
                parm6.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(parm6);

                SqlParameter parm7 = new SqlParameter("@Valido", SqlDbType.Bit);
                parm7.Direction = ParameterDirection.Output; // This is important!
                cmd.Parameters.Add(parm7);

                SqlParameter parm8 = new SqlParameter("@GetInfoFace", SqlDbType.Bit);
                parm8.Direction = ParameterDirection.Output; // This is important!
                cmd.Parameters.Add(parm8);

                cn.Open();
                cmd.ExecuteNonQuery();

                cn.Close();
                Resut = true;
            }

            return Resut;
       
        }

        public bool GravaInfFb(string _Email)
        {

            try
            {

                LoginRepositorio Login = new LoginRepositorio();

                LogLoginRepositorio LogLogin = new LogLoginRepositorio();

                Login L = Login.SelLoginEmail(_Email);

                LogLogin LL = LogLogin.SelLogLogin(L.ID);

                if (L.Facebook == true)
                {

                    //detalhes do usuario
                    var UsurFB = new FacebookClient(LL.Token);

                    //detalhes do usuario
                    var request = UsurFB.Get("me");

                    dynamic data = JObject.Parse(request.ToString());

                    //Cadastrar Usuário
                    SqlConnection cn = new SqlConnection(@"Data Source=mssql.intercambiar.com.br;Initial Catalog=intercambiar;User ID=intercambiar;Password=carro1818;MultipleActiveResultSets=True;Application Name=EntityFramework");

                    using (SqlCommand cmd = new SqlCommand("p_Usuario_Add", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter parm = new SqlParameter("@IdLogin", SqlDbType.Int);
                        parm.Value = L.ID;
                        parm.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(parm);

                        SqlParameter parm2 = new SqlParameter("@Email", SqlDbType.VarChar);
                        parm2.Value = data.email.ToString();
                        parm2.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(parm2);

                        SqlParameter parm3 = new SqlParameter("@Nome", SqlDbType.VarChar, 500);
                        parm3.Value = data.name.ToString();
                        parm3.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(parm3);

                        SqlParameter parm4 = new SqlParameter("@locate", SqlDbType.VarChar, 50);
                        parm4.Value = data.locale.ToString();
                        parm4.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(parm4);

                        SqlParameter parm5 = new SqlParameter("@sexo", SqlDbType.Char, 1);
                        parm5.Value = ((data.gender.ToString() == "male") ? "M" : "F");
                        parm5.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(parm5);

                        SqlParameter parm6 = new SqlParameter("@Foto", SqlDbType.VarChar, 50);
                        parm6.Value = GetPictureUrl(data.id.ToString());
                        parm6.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(parm6);

                        cn.Open();
                        cmd.ExecuteNonQuery();
                        cn.Close();
                    }
                }

                //string schoo = "";

                //foreach (dynamic Edu in data.education)
                //{
                //    using (SqlCommand cmd = new SqlCommand("p_fb_school_Add", cn))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;
                //        SqlParameter parm = new SqlParameter("@IdName", SqlDbType.VarChar, 15);
                //        parm.Value = Edu.school.id.ToString();
                //        parm.Direction = ParameterDirection.Input;
                //        cmd.Parameters.Add(parm);

                //        SqlParameter parm2 = new SqlParameter("@IdLogin", SqlDbType.Int);
                //        parm2.Value = L.ID;
                //        parm2.Direction = ParameterDirection.Input;
                //        cmd.Parameters.Add(parm2);

                //        SqlParameter parm3 = new SqlParameter("@school", SqlDbType.VarChar, 500);
                //        parm3.Value = Edu.school.name.ToString();
                //        parm3.Direction = ParameterDirection.Input;
                //        cmd.Parameters.Add(parm3);

                //        SqlParameter parm4 = new SqlParameter("@type", SqlDbType.VarChar, 500);
                //        parm4.Value = ((Edu.type!=null) ? Edu.type.ToString(): "");
                //        parm4.Direction = ParameterDirection.Input;
                //        cmd.Parameters.Add(parm4);

                //        SqlParameter parm5 = new SqlParameter("@year", SqlDbType.VarChar, 10);
                //        parm5.Value = ((Edu.year != null) ? Edu.year.name.ToString() : "");
                //        parm5.Direction = ParameterDirection.Input;
                //        cmd.Parameters.Add(parm5);

                //        cn.Open();
                //        cmd.ExecuteNonQuery();
                //        cn.Close();
                //    }
                //}
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string HashValue(string value)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] hashBytes;
            using (HashAlgorithm hash = SHA1.Create())
                hashBytes = hash.ComputeHash(encoding.GetBytes(value));

            StringBuilder hashValue = new StringBuilder(hashBytes.Length * 3);
            foreach (byte b in hashBytes)
            {
                hashValue.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", b);
            }

            return hashValue.ToString();
        }

        
        public string GetFacebookLoginUrl(string Dis, string url)
        {
            dynamic parameters = new ExpandoObject();
            parameters.client_id = "1570231473265349";
            parameters.redirect_uri = "http://www.intercambiar.com.br/Social/RetornoFb";
            parameters.response_type = "code";
            if (Dis == "1")
                parameters.display = "page";
            else
                parameters.display = "popup";

            var extendedPermissions = "user_about_me, email ";
            parameters.scope = extendedPermissions;

            //Criando um objeto cookie
            HttpCookie UrlVoltaFBCookie = new HttpCookie("IntercambiarUrlVoltaFB");

            //Setando o ID do usuário no cookie
            UrlVoltaFBCookie.Value = url.ToString();

            //Definindo o prazo de vida do cookie
            UrlVoltaFBCookie.Expires = DateTime.Now.AddDays(1);

            //Adicionando o cookie no contexto da aplicação
            HttpContext.Current.Response.Cookies.Add(UrlVoltaFBCookie);

            var _fb = new FacebookClient();
            return _fb.GetLoginUrl(parameters).ToString();

        }

        public void RetornoFb(Uri url)
        {
            var _fb = new FacebookClient();
            FacebookOAuthResult oauthResult;

            _fb.TryParseOAuthCallbackUrl(url, out oauthResult);

            if (oauthResult.IsSuccess)
            {
                //Pega o Access Token "permanente"
                dynamic parameters = new ExpandoObject();
                parameters.client_id = "1570231473265349";
                parameters.redirect_uri = "http://www.intercambiar.com.br/Social/RetornoFb";
                parameters.client_secret = "9f3ddca472b0df089dced6ab374283d1";
                parameters.code = oauthResult.Code;

                dynamic result = _fb.Get("/oauth/access_token", parameters);

                var accessToken = result.access_token;

                //detalhes do usuario
                var UsurFB = new FacebookClient(accessToken);

                //detalhes do usuario
                var request = UsurFB.Get("me");

                dynamic data = JObject.Parse(request.ToString());

                AutenticarUsuarioFB(data, accessToken);

            }
            else
            {
                // tratar
            }

        }

        public bool AutenticarUsuarioFB(dynamic data, string accessToken)
        {
            try
            {
                LoginRepositorio Login = new LoginRepositorio();

                Login L = Login.SelLoginEmail(data.email.ToString());

                if (L != null)
                {
                    if (L.Facebook == false)
                    {
                        L.Facebook = true;
                        Login.UpdLogin(L);
                    }
                }
                else
                {
                    L = new Login();
                    L.email = data.email.ToString();
                    L.Facebook = true;
                    L.Recuperar = false;
                    Login.AddLogin(L);
                }

                CriarCookie(data.email.ToString(), accessToken);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetUserIP()
        {
            string strIP = String.Empty;
            HttpRequest httpReq = HttpContext.Current.Request;

            //test for non-standard proxy server designations of client's IP
            if (httpReq.ServerVariables["HTTP_CLIENT_IP"] != null)
            {
                strIP = httpReq.ServerVariables["HTTP_CLIENT_IP"].ToString();
            }
            else if (httpReq.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                strIP = httpReq.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            //test for host address reported by the server
            else if
            (
                //if exists
                (httpReq.UserHostAddress.Length != 0)
                &&
                //and if not localhost IPV6 or localhost name
                ((httpReq.UserHostAddress != "::1") || (httpReq.UserHostAddress != "localhost"))
            )
            {
                strIP = httpReq.UserHostAddress;
            }
            //finally, if all else fails, get the IP from a web scrape of another server
            else
            {
                WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
                using (WebResponse response = request.GetResponse())
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    strIP = sr.ReadToEnd();
                }
                //scrape ip from the html
                int i1 = strIP.IndexOf("Address: ") + 9;
                int i2 = strIP.LastIndexOf("</body>");
                strIP = strIP.Substring(i1, i2 - i1);
            }
            return strIP;
        }

        //Validações Cadastro

        public bool ValidaEmail(string Email)
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (rg.IsMatch(Email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetPictureUrl(string faceBookId)
        {
            WebResponse response = null;
            string pictureUrl = string.Empty;
            try
            {
                WebRequest request = WebRequest.Create(string.Format("https://graph.facebook.com/{0}/picture", faceBookId));
                response = request.GetResponse();
                pictureUrl = response.ResponseUri.ToString();
            }
            catch (Exception ex)
            {
                //? handle
            }
            finally
            {
                if (response != null) response.Close();
            }
            return string.Format("https://graph.facebook.com/{0}/picture", faceBookId);
        }

        public bool AdicionarUsuario(Usuario U, string Senha)
        {

            LoginRepositorio LoginRepo = new LoginRepositorio();
            Login L = new Login();
            L.email = U.Email;
            L.Facebook = false;
            L.Recuperar = false;
            L.Senha = HashValue(Senha).ToString();
            LoginRepo.AddLogin(L);

            Login L_Novo = LoginRepo.SelLoginEmail(U.Email);

            U.IdLogin = L_Novo.ID;

            UsuarioRepositorio U_rep = new UsuarioRepositorio();

            U_rep.AddUsuario(U);

            return true;
        }

        public CookieUsr GetCookie(string Value)
        {
            CookieUsr C = new CookieUsr();
            if (Value != null)
            {
                string val = Descriptografar(Value);

                bool set = false;
                int countLen = 0;

                //1
                string strEmail = "";
                //2
                string strDay = "";
                //3
                string strMonth = "";
                //4
                string strYear = "";
                //5
                string strToLongTimeString = "";
                //6
                string strMillisecond = "";


                for (int i = 0; i < val.Length; i++)
                {

                    if (set == true)
                    {
                        if (countLen == 1 && val.Substring(i, 1).ToString() != "|")
                        {
                            strEmail += val.Substring(i, 1).ToString();
                        }
                        else if (countLen > 1)
                        {
                            if (countLen == 2 && val.Substring(i, 1).ToString() != "|")
                            {
                                strDay += val.Substring(i, 1).ToString();
                            }
                            else if (countLen > 2)
                            {
                                if (countLen == 3 && val.Substring(i, 1).ToString() != "|")
                                {
                                    strMonth += val.Substring(i, 1).ToString();
                                }
                                else if (countLen > 3)
                                {
                                    if (countLen == 4 && val.Substring(i, 1).ToString() != "|")
                                    {
                                        strYear += val.Substring(i, 1).ToString();
                                    }
                                    else if (countLen > 4)
                                    {
                                        if (countLen == 5 && val.Substring(i, 1).ToString() != "|")
                                        {
                                            strToLongTimeString += val.Substring(i, 1).ToString();
                                        }
                                        else
                                        {
                                            if (countLen == 6 && val.Substring(i, 1).ToString() != "|")
                                            {
                                                strMillisecond += val.Substring(i, 1).ToString();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (val.Substring(i, 1).ToString() == "|")
                    {
                        if (set == false)
                        {
                            set = true;
                            countLen += 1;
                        }
                        else
                            set = false;
                    }
                }

                if (strDay.Length < 2)
                    strDay = "0" + strDay;

                if (strMonth.Length < 2)
                    strMonth = "0" + strMonth;

                DateTime DataCookie = DateTime.ParseExact(strYear + "-" + strMonth + "-" + strDay + " " + strToLongTimeString + "," + strMillisecond, "yyyy-MM-dd HH:mm:ss,fff",
                                                      System.Globalization.CultureInfo.InvariantCulture);

                C.email = strEmail;
                C.Data = DataCookie;

                return C;
            }
            else
                return null;
        }

        public Intercambiar_mvc_v1.Models.Perfil GetPerfil(Usuario U)
        {

            Intercambiar_mvc_v1.Models.Perfil P = new Intercambiar_mvc_v1.Models.Perfil();

            P.IdUsuario = U.Id;
            P.IdLogin = U.IdLogin;

            P.Pontuacao = 1;
            P.Estrelas = 1;
            P.Textos  = 0;
            P.Viagens = 0;
            P.Comentarios = 0;
            P.Gols = 0;

            return P;

        }

        private string GetLocation(string UserIP)
        {
            try
            {

                string valor = "";

                if (UserIP == "::1")
                {
                    valor = "LocalHost";
                }
                else
                {
                    string url = "http://freegeoip.net/json/" + UserIP.ToString();
                    WebClient client = new WebClient();
                    string jsonstring = client.DownloadString(url);

                    dynamic dynObj = JsonConvert.DeserializeObject(jsonstring);
                    valor += "Cod Pais:" + dynObj.country_code + "|";
                    valor += "Nome Pais:" + dynObj.country_name + "|";
                    valor += "Cod Regiao:" + dynObj.region_code + "|";
                    valor += "Nome Regiao:" + dynObj.region_name + "|";
                    valor += "Nome Cidade:" + dynObj.city + "|";
                    valor += "CEP:" + dynObj.zip_code + "|";
                    valor += "time_zone:" + dynObj.time_zone + "|";
                    valor += "latitude:" + dynObj.latitude + "|";
                    valor += "metro_code:" + dynObj.metro_code + "|";
                }

                return valor;
            }
            catch
            {
                return "Erro";
            }
        }


        public IList<Noticia_Relat> Sel_Noticias_Relat(int ID_Usuario)
        {
               IList<Noticia_Relat> R = new List<Noticia_Relat>();
            //Cadastrar Usuário
            SqlConnection cn = new SqlConnection(@"Data Source=mssql.intercambiar.com.br;Initial Catalog=intercambiar;User ID=intercambiar;Password=carro1818;MultipleActiveResultSets=True;Application Name=EntityFramework");

            using (SqlCommand cmd = new SqlCommand("p_Noticias_Relat", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parm = new SqlParameter("@ID_Usuario", SqlDbType.Int);
                parm.Value = ID_Usuario;
                parm.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(parm);
                cn.Open();
                SqlDataReader myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    Noticia_Relat U = new Noticia_Relat();
                    IDataRecord record = (IDataRecord)myReader;

                    U.id = Int32.Parse(record["Id"].ToString());
                    U.Titulo = record["Titulo"].ToString();
                    U.Imagem = record["Imagem"].ToString();
                    U.Acesso = Int32.Parse(record["Acesso"].ToString());
                    U.Comentario = Int32.Parse(record["Comentario"].ToString());
                    U.Novo = Int32.Parse(record["Novo"].ToString());

                    R.Add(U);

                };
                cn.Close();

            }

            return R;

        }


    }


}