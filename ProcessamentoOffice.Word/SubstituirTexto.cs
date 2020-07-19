using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;
using ProcessamentoOffice.Domain.Core.Entities;
using ProcessamentoOffice.Infra.CrossCutting.Arquivos;

namespace ProcessamentoOffice.Word
{
    public class SubstituirTexto
    {
        public static void Substituir(string arquivoOrigem, string arquivoDestino, IEnumerable<Substituicao> substituicoes)
        {
            UtilitarioArquivo.ClonarArquivo(arquivoOrigem, arquivoDestino);

            using (WordprocessingDocument document = WordprocessingDocument.Open(arquivoDestino, isEditable: true))
            {
                foreach (Substituicao substituicao in substituicoes)
                {
                    TextReplacer.SearchAndReplace(
                        document,
                        substituicao.Chave,
                        substituicao.Valor,
                        substituicao.MatchCase);
                }
            }
        }

        public static int SubstituirPorRegex(string arquivoOrigem, string arquivoDestino, IEnumerable<SubstituicaoRegex> substituicoes)
        {
            UtilitarioArquivo.ClonarArquivo(arquivoOrigem, arquivoDestino);

            int quantidadeSubstituicoes = 0;

            using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(arquivoDestino, isEditable: true))
            {
                XDocument xDocument = wordDocument.MainDocumentPart.GetXDocument();

                IEnumerable<XElement> content = xDocument.Descendants(W.p);

                foreach (SubstituicaoRegex substituicao in substituicoes)
                    quantidadeSubstituicoes += OpenXmlRegex.Replace(content, substituicao.Chave, substituicao.Valor, doReplacement: null);

                wordDocument.MainDocumentPart.PutXDocument();
            }

            return quantidadeSubstituicoes;
        }
    }
}
