using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jogo_da_Velha_Cs_3._0.Domain.Model;
using Jogo_da_Velha_Cs_3._0.Controllers;

namespace Jogo_da_Velha_Cs_3._0.Domain.Model
{
    public class Posicoes
    {
        public Posicoes(){
            PosicaoAll = new string[9];
        }

        public string[] PosicaoAll { get; set; }
    }       
}
