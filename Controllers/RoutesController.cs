using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jogo_da_Velha_Cs_3._0.Domain.Model;
using Jogo_da_Velha_Cs_3._0.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Jogo_da_Velha_Cs_3._0.Controllers
{
    
    [ApiController]
    [Route("game")]
    public class RoutesController : ControllerBase
    {
        private readonly IJogadaService jogadaService;
        public RoutesController(IJogadaService jogadaService)
        {
            this.jogadaService = jogadaService;
        }
        // POST game/{id}/movement
        [HttpPost]
        [Route("{id}/movement")]
        public Jogo PostJogada([FromBody] Jogada jogada)
        {
            return jogadaService.PostJogada(jogada);
        }
        // POST game
        [HttpPost]
        public Jogo Game()
        {
            return jogadaService.Game();
        }

        // POST loadjogos
        [HttpPost]
        [Route("loadjogos")]
        public IReadOnlyList<Jogo> LoadJogos()
        {
            return jogadaService.LoadJogos();
        }

        // POST loadJogo
        [HttpPost]
        [Route("load")]
        public Jogo LoadJogo([FromBody] Jogo load)
        {
            return jogadaService.LoadJogo(load);
        }
    }
}
