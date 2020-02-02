using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jogo_da_Velha_Cs_3._0.Domain.Model;
using Jogo_da_Velha_Cs_3._0.Controllers;

namespace Jogo_da_Velha_Cs_3._0.Domain.Model
{
    public interface IJogadaService
    {
        Jogo PostJogada(Jogada jogada);

        IReadOnlyList<Jogo> LoadJogos();

        Jogo LoadJogo(Jogo jogo);

        Jogo Game(); 
    }
}
