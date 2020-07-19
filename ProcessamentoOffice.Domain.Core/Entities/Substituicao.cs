using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessamentoOffice.Domain.Core.Entities
{
    public class Substituicao
    {
        public string Chave { get; set; }
        public string  Valor { get; set; }
        public bool MatchCase { get; set; }

        public Substituicao(string chave, string valor, bool matchCase)
        {
            Chave = chave;
            Valor = valor;
            MatchCase = matchCase;
        }
    }
}
