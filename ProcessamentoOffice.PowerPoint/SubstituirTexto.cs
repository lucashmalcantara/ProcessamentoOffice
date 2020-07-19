using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;
using ProcessamentoOffice.Domain.Core.Entities;
using ProcessamentoOffice.Infra.CrossCutting.Arquivos;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace ProcessamentoOffice.PowerPoint
{
    public class SubstituirTexto
    {
        public static void Substituir(string arquivoOrigem, string arquivoDestino, IEnumerable<Substituicao> substituicoes)
        {
            UtilitarioArquivo.ClonarArquivo(arquivoOrigem, arquivoDestino);

            using (PresentationDocument presentation =
                PresentationDocument.Open(arquivoDestino, isEditable: true))
            {
                foreach (Substituicao substituicao in substituicoes)
                {
                    TextReplacer.SearchAndReplace(
                        presentation,
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

            using (PresentationDocument presentation =
                PresentationDocument.Open(arquivoDestino, isEditable: true))
            {
                foreach (SlidePart slidePart in presentation.PresentationPart.SlideParts)
                {
                    XDocument xDocument = slidePart.GetXDocument();

                    foreach (SubstituicaoRegex substituicao in substituicoes)
                    {
                        // Replace content
                        IEnumerable<XElement> content = xDocument.Descendants(A.p);
                        quantidadeSubstituicoes += OpenXmlRegex.Replace(content, substituicao.Chave, substituicao.Valor, doReplacement: null);

                        // If you absolutely want to preserve compatibility with PowerPoint 2007, then you will need to strip the xml:space="preserve" attribute throughout.
                        // This is an issue for PowerPoint only, not Word, and for 2007 only.
                        // The side-effect of this is that if a run has space at the beginning or end of it, the space will be stripped upon loading, and content/layout will be affected.
                        xDocument.Descendants().Attributes(XNamespace.Xml + "space").Remove();

                        slidePart.PutXDocument();
                    }
                }
            }

            return quantidadeSubstituicoes;
        }
    }
}
