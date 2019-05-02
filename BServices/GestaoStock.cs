using Interop.GcpBE900;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BServices
{
    public class GestaoStock:Geral
    {
        public GestaoStock(object BSO, object plat) : base(BSO, plat)
        {
        }

        public void geraDocStock( string tipodoc,DateTime data, string strTipoArmazemDestino, 
            ref string strErro, ref string str_Aviso)
        {

            string str_query;

            GcpBEDocumentoStock objDocStock = new GcpBEDocumentoStock();
            string movStock;
            string armDestino = "";
            string unidadeVenda;

            DataTable dt;

            //try
            //{

            //    objDocStock = new GcpBEDocumentoStock();
            //    objDocStock.set_Tipodoc(tipodoc);
            //    objDocStock.set_Serie(bso.Comercial.Series.DaSerieDefeito("S", tipodoc, data));

            //    bso.Comercial.Stocks.PreencheDadosRelacionados(objDocStock);

            //    objDocStock.set_DataDoc(data);

            //    for (int i = 1; i <= objVenda.get_Linhas().NumItens; i++)
            //    {

            //        if (bso.Comercial.Artigos.Existe(objVenda.get_Linhas()[i].get_Artigo()))
            //        {
            //            if (armDestino.Length == 0)
            //            {
            //                switch (strTipoArmazemDestino)
            //                {
            //                    case "Cliente":
            //                        {
            //                            armDestino = daArmazemCliente(objVenda.get_Entidade());
            //                            objDocStock.set_ArmazemOrigem(objVenda.get_Linhas()[i].get_Armazem());
            //                            break;
            //                        }

            //                    default:
            //                        {
            //                            armDestino = objVenda.get_Linhas()[i].get_Armazem();
            //                            objDocStock.set_ArmazemOrigem(daArmazemCliente(objVenda.get_Entidade()));
            //                            break;
            //                        }
            //                }
            //            }

            //            unidadeVenda = Convert.ToString(
            //                bso.Comercial.Artigos.DaValorAtributo(objVenda.get_Linhas()[i].get_Artigo(), "UnidadeVenda")
            //            );

            //            movStock = Convert.ToString(
            //                bso.Comercial.Artigos.DaValorAtributo(objVenda.get_Linhas()[i].get_Artigo(), "MovStock")
            //            );

            //            if (movStock == "S")
            //            {
            //                bso.Comercial.Stocks.AdicionaLinha(objDocStock, objVenda.get_Linhas()[i].get_Artigo(),
            //                    "", objVenda.get_Linhas()[i].get_Quantidade(), armDestino, 0,
            //                    0, objVenda.get_Linhas()[i].get_Lote());

            //                int numeroItens = objDocStock.get_Linhas().NumItens;

            //                objDocStock.get_Linhas()[numeroItens].set_IdLinhaOrigemCopia(objVenda.get_Linhas()[i].get_IdLinha());
            //                objDocStock.get_Linhas()[numeroItens].set_ModuloOrigemCopia("V");
            //                objDocStock.get_Linhas()[numeroItens].set_Unidade(objVenda.get_Linhas()[i].get_Unidade());
            //                objDocStock.get_Linhas()[numeroItens].set_FactorConv(objVenda.get_Linhas()[i].get_FactorConv());
            //                objDocStock.get_Linhas()[numeroItens].set_DataStock(objVenda.get_Linhas()[i].get_DataStock());
            //            }
            //        }

            //    }

            //    if (objDocStock.get_Linhas().NumItens > 0)
            //    {
            //        bso.Comercial.Stocks.Actualiza(objDocStock);

            //        str_Aviso += Environment.NewLine +
            //            string.Format("Foi criado/editado o documento {0} {1}/{2}",
            //            objDocStock.get_Tipodoc(), objDocStock.get_NumDoc(), objDocStock.get_Serie());
            //    }


            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    ReleaseObj(objDocStock);
            //}


        }

    }
}
