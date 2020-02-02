using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jogo_da_Velha_Cs_3._0.Domain.Model;
using Jogo_da_Velha_Cs_3._0.Controllers;
using Jogo_da_Velha_Cs_3._0.Domain.Service;

namespace Jogo_da_Velha_Cs_3._0.Domain.Model
{
    public class Jogo
    {
        public Jogo()
        {
            Id = Guid.NewGuid();
            Random rnd = new Random();
            FirstPlayer = rnd.Next(10) % 2 == 0 ? "X" : "O";
            Position = new Posicoes();
            Status = null;
        }
        public Guid? Id { get; set; }
        public string FirstPlayer { get; set; }
        public string LastTurn { get; set; }
        public string Status { get; set; }

        public Posicoes Position { get; set; } 


    }
}
