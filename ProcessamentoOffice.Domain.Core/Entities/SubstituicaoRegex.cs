using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ProcessamentoOffice.Domain.Core.Entities
{
    public class SubstituicaoRegex
    {
        public Regex Chave { get; set; }
        public string Valor { get; set; }
        public bool MatchCase { get; set; }

        public SubstituicaoRegex(Regex chave, string valor, bool matchCase)
        {
            Chave = chave;
            Valor = valor;
            MatchCase = matchCase;
        }
    }
}
