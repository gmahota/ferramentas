using Interop.ErpBS900;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belutecnica
{
    public class Geral : IDisposable
    {
        public StdBSInterfPub plat { get; set; }
        public ErpBS bso { get; set; }

        public Geral(ErpBS bso)
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            this.bso = bso;
        }

        public Geral(dynamic bso, dynamic pso)
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            this.bso = bso;
            this.plat = pso;
        }

        public Geral(string userPrimavera, string passUserPrimavera, string empresa, int tipoEmpPRI)
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            AbrirMotorPrimavera(userPrimavera, passUserPrimavera, empresa, tipoEmpPRI);
        }

        public ErpBS MotorERP()
        {
            return bso;
        }

        /// <summary>
        /// Método para resolução das assemblies.
        /// </summary>
        /// <param name="sender">Application</param>
        /// <param name="args">Resolving Assembly Name</param>
        /// <returns>Assembly</returns>
        static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string assemblyFullName;
            System.Reflection.AssemblyName assemblyName;
            string PRIMAVERA_COMMON_FILES_FOLDER = PrimaveraConstHelper.pastaConfigV900;//pasta dos ficheiros comuns especifica da versão do ERP PRIMAVERA utilizada.
            assemblyName = new System.Reflection.AssemblyName(args.Name);
            assemblyFullName = System.IO.Path.Combine(System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86), PRIMAVERA_COMMON_FILES_FOLDER), assemblyName.Name + ".dll");
            if (System.IO.File.Exists(assemblyFullName))
                return System.Reflection.Assembly.LoadFile(assemblyFullName);
            else
                return null;
        }

        public Boolean AbrirMotorPrimavera(string userPrimavera, string passUserPrimavera, string empresa, int tipoEmpPRI)
        {
            try
            {
                StdBSConfApl objAplConf = new StdBSConfApl();
                StdPlatBS Plataforma = new StdPlatBS();
                ErpBS MotorLE = new ErpBS();

                EnumTipoPlataforma objTipoPlataforma = new EnumTipoPlataforma();
                objTipoPlataforma = EnumTipoPlataforma.tpEmpresarial;

                objAplConf.Instancia = "Default";
                objAplConf.AbvtApl = "ERP";
                objAplConf.PwdUtilizador = passUserPrimavera;
                objAplConf.Utilizador = userPrimavera;
                objAplConf.LicVersaoMinima = "9.00";

                StdBETransaccao objStdTransac = new StdBETransaccao();

                try
                {
                    Plataforma.AbrePlataformaEmpresa(ref empresa, ref objStdTransac, ref objAplConf, ref objTipoPlataforma, "");
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                if (Plataforma.Inicializada)
                {

                    plat = Plataforma.InterfacePublico;

                    bool blnModoPrimario = true;

                    MotorLE.AbreEmpresaTrabalho(tipoEmpPRI == 0 ? EnumTipoPlataforma.tpEmpresarial : EnumTipoPlataforma.tpProfissional,
                        ref empresa, ref userPrimavera, ref passUserPrimavera, ref objStdTransac, "Default", ref blnModoPrimario);
                    MotorLE.set_CacheActiva(true);

                    bso = MotorLE;

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable ConsultaSQLDatatable(string querySql)
        {

            try
            {
                DataTable dt = new DataTable();

                string connectionString = plat.BaseDados.DaConnectionStringNET(plat.BaseDados.DaNomeBDdaEmpresa(bso.Contexto.CodEmp),
                    "Default");

                SqlConnection con = new SqlConnection(connectionString);

                SqlDataAdapter da = new SqlDataAdapter(querySql, con);

                SqlCommandBuilder cb = new SqlCommandBuilder(da);

                da.Fill(dt);

                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string GetParameter(string Name)
        {
            try
            {
                string result = "";
                string query = string.Format("select * from TDU_Parametros where CDU_Parametro = '{0}' ", Name);
                DataTable dt = ConsultaSQLDatatable(query);

                if (dt.Rows.Count > 0)
                {
                    result = dt.Rows[0]["CDU_Valor"].ToString();
                }
                else
                {
                    new Exception("O parametro {0} não se encontra configurado na tabela de TDU_Parametros");
                }

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public int ExecutaQuery(string querySQL)
        {
            try
            {
                DataTable dt = new DataTable();

                string connectionString = plat.BaseDados.DaConnectionStringNET(plat.BaseDados.DaNomeBDdaEmpresa(bso.Contexto.CodEmp),
                    "Default");

                SqlConnection con = new SqlConnection(connectionString);

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.Connection = con;
                command.CommandText = querySQL;
                con.Open();

                return command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public object F4Event(string query, string Coluna, string nomeTabela)
        {
            try
            {
                return plat.Listas.GetF4SQL(nomeTabela, query, Coluna);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Connection AbrirLigacaoXLS(string CaminhoExcel)
        {
            Connection oCon = new Connection();
            oCon = new Connection();
            oCon.ConnectionTimeout = 30;
            oCon.Open(("Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                            + (CaminhoExcel + ";Extended Properties=\"Excel 8.0;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text\"")));
            return oCon;
        }

        public void FecharLigacaoXLS(ref Connection ligacao)
        {
            ligacao.Close();
        }

        public void AddLinhaVazia(DataTable dt)
        {
            try
            {
                DataRow dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable daListaTabela(string tabela, int maximo = 0, string campos = "", string filtros = "", string juncoes = "", string ordenacao = "")
        {
            try
            {
                string strSql = "select ";

                DataTable dt;

                if (maximo > 0) strSql = strSql + string.Format("Top {0} ", maximo);

                if ((campos.Length > 0))
                {
                    strSql = (strSql + campos);
                }
                else
                {
                    strSql = (strSql + " * ");
                }

                strSql = (strSql + (" from " + tabela));
                if ((juncoes.Length > 0))
                {
                    strSql = (strSql + (" " + juncoes));
                }

                if ((filtros.Length > 0))
                {
                    strSql = (strSql + (" where " + filtros));
                }

                if ((ordenacao.Length > 0))
                {
                    strSql = (strSql + (" order by " + ordenacao));
                }

                dt = this.ConsultaSQLDatatable(strSql);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void escreveLog(string pastaLog, string name, string logMessage)
        {
            string ficheiro;
            try
            {
                ficheiro = pastaLog;

                using (StreamWriter w = File.AppendText(ficheiro + "\\" + string.Format("log_{0}.log", name)))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void escreveLog(string logMessage)
        {
            string ficheiro;
            try
            {
                ficheiro = GetParameter("Log_PastaErro");

                using (StreamWriter w = File.AppendText(ficheiro + "\\" + string.Format("log_{0}.txt", DateTime.Now.ToString("ddMMyy"))))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AvisoPoliticaComercial(string mensaguem, ref string strAviso)
        {
            try
            {
                if (!strAviso.Contains(mensaguem))
                    strAviso += mensaguem + "\n";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreditoSuspenso(string cliente)
        {
            string tipoCredito;
            try
            {
                tipoCredito = bso.Comercial.Clientes.DaValorAtributo(cliente, "TipoCredito").ToString();

                bso.Comercial.Clientes.ActualizaValorAtributo(cliente, "Cdu_CredSusp", tipoCredito);
                bso.Comercial.Clientes.ActualizaValorAtributo(cliente, "TipoCredito", "1");
                bso.Comercial.Clientes.ActualizaValorAtributo(cliente, "LimiteCredValor", false);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro metodo credito Suspenso para o cliente " + cliente + " devido à: " + ex.Message);
            }
        }

        public void CreditoSuspenso_DepoisDeGravar(string cliente)
        {
            string tipoCredito;

            try
            {
                tipoCredito = bso.Comercial.Clientes.DaValorAtributo(cliente, "Cdu_CredSusp").ToString();

                if (tipoCredito == "2")
                    bso.Comercial.Clientes.ActualizaValorAtributo(cliente, "TipoCredito", tipoCredito);

                bso.Comercial.Clientes.ActualizaValorAtributo(cliente, "LimiteCredValor", true);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro metodo CreditoSuspenso_DepoisDeGravar para o cliente " + cliente + " devido à: " + ex.Message);
            }
        }

        public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), logMessage);
            }
            catch (Exception ex)
            {
            }
        }

        public void ReleaseObj(object objecto)
        {
            try
            {
                if (objecto != null)
                {
                    while (System.Runtime.InteropServices.Marshal.ReleaseComObject(objecto) > 0)
                    {

                    }

                    objecto = null;
                    GC.Collect();
                }
            }
            catch
            {

            }
        }

        public void Dispose()
        {

            //_empresaErp.Dispose();
            //_comercial.Dispose();

        }



    }
}
