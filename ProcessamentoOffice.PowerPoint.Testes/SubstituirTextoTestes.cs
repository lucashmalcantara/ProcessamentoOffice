using ProcessamentoOffice.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xunit;

namespace ProcessamentoOffice.PowerPoint.Testes
{
    public class SubstituirTextoTestes
    {
        [Fact]
        public void Substituir()
        {
            var substituicoes = new List<Substituicao>();
            substituicoes.Add(new Substituicao(chave: "@Titulo", valor: "Meu título", matchCase: false));
            substituicoes.Add(new Substituicao(chave: "@Subtitulo", valor: "Meu subtítulo", matchCase: false));

            string arquivoOrigem = @".\Recursos\Templates\TesteSubstituicao.pptx";
            string diretorioDestino = @".\Resultados\Substituir";
            string arquivoDestino = $@"{diretorioDestino}\{DateTime.Now:yyyy.MM.dd HH.mm.ss} - Teste Substituir.pptx";

            SubstituirTexto.Substituir(arquivoOrigem, arquivoDestino, substituicoes);
        }

        [Fact]
        public void SubstituirPorRegex()
        {
            var substituicoes = new List<SubstituicaoRegex>();
            substituicoes.Add(new SubstituicaoRegex(chave: new Regex("@Titulo"), valor: "Meu título", matchCase: false));
            substituicoes.Add(new SubstituicaoRegex(chave: new Regex("@Subtitulo"), valor: "Meu subtítulo", matchCase: false));

            string arquivoOrigem = @".\Recursos\Templates\TesteSubstituicao.pptx";
            string diretorioDestino = @".\Resultados\SubstituirPorRegex";
            string arquivoDestino = $@"{diretorioDestino}\{DateTime.Now:yyyy.MM.dd HH.mm.ss} - Teste SubstituirPorRegex.pptx";

            SubstituirTexto.SubstituirPorRegex(arquivoOrigem, arquivoDestino, substituicoes);
        }
    }
}
