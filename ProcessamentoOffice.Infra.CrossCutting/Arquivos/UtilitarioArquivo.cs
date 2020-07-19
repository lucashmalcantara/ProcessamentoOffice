using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProcessamentoOffice.Infra.CrossCutting.Arquivos
{
    public class UtilitarioArquivo
    {
        public static void ClonarArquivo(string arquivoOrigem, string arquivoDestino)
        {
            string diretorioDestiono = Path.GetDirectoryName(arquivoDestino);
            Directory.CreateDirectory(diretorioDestiono);
            File.Copy(arquivoOrigem, arquivoDestino);
        }
    }
}
