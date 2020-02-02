using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jogo_da_Velha_Cs_3._0.Domain.Model;
using Jogo_da_Velha_Cs_3._0.Controllers;

namespace Jogo_da_Velha_Cs_3._0.Domain.Model
{
    public class Jogada
    {
        public Posicao Position { get; set; }
        public Guid? Id { get; set; }
        public string Player { get; set; }
    }
}
