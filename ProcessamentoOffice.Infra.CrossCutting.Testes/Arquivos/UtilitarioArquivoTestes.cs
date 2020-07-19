using ProcessamentoOffice.Infra.CrossCutting.Arquivos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace ProcessamentoOffice.Infra.CrossCutting.Testes.Arquivos
{
    public class UtilitarioArquivoTestes
    {
        [Fact]
        public void ClonarArquivoTeste()
        {
            string arquivoOrigem = @".\Recursos\TesteClonarArquivo.txt";
            string diretorioDestino = @".\Resultados\ClonarArquivoTeste";

            var directoryInfo = new DirectoryInfo(diretorioDestino);
            if (directoryInfo.Exists)
            {
                directoryInfo.Delete(recursive: true);
                directoryInfo.Create();
            }

            string arquivoDestino = $@"{diretorioDestino}\Teste ClonarArquivoTeste.txt";

            UtilitarioArquivo.ClonarArquivo(arquivoOrigem, arquivoDestino);

            int quantidadeArquivos = directoryInfo.GetFiles().Count();
            const int quantidadeArquivosEsperada = 1;

            Assert.Equal(quantidadeArquivosEsperada,quantidadeArquivos);
        }
    }
}
