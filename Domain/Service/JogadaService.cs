using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jogo_da_Velha_Cs_3._0.Domain.Model;
using Jogo_da_Velha_Cs_3._0.Controllers;
using Jogo_da_Velha_Cs_3._0.Domain.Service;

namespace Jogo_da_Velha_Cs_3._0.Domain.Model
{
    public class JogadaService : IJogadaService
    {
        private readonly List<Jogo> jogos;
        private CondicaoService ResultadoStatus;
        public JogadaService()
        {
            jogos = new List<Jogo>();
            ResultadoStatus = new CondicaoService();
        }

        public Jogo Game()
        {
            Jogo jogo = new Jogo();
            jogos.Add(jogo);
            return jogo;
        }

        public Jogo LoadJogo(Jogo jogo)
        {
            if (jogo.Id == null)
            {
                throw new Exception("Id requisitado invalido!");
            }
            else
            {
                List<Jogo> JogoById = jogos.Where(j =>
                    (j.Id != null) && (j.Id.Equals(jogo.Id))).ToList();
                if (JogoById.Count > 0)
                {
                    return JogoById[0];
                }
                else
                {
                    return null;
                }

            }
        }

        public IReadOnlyList<Jogo> LoadJogos()
        {
            return jogos.AsReadOnly();
        }

        public Jogo PostJogada(Jogada jogada)
        {
            bool acao = false;
            if (jogada.Id == null)
            {
                throw new Exception("Id requisitado invalido!");
            }
            else
            {
                List<Jogo> JogoById = jogos.Where(j =>
                    (j.Id != null) && (j.Id.Equals(jogada.Id))).ToList();
                if (JogoById.Count > 0 && JogoById[0].Status == null || JogoById[0].Status == "Mudou")
                {
                    if (jogada.Position.X.Equals("0"))
                    {
                        switch (jogada.Position.Y)
                        { 
                            case "0":
                                if (JogoById[0].Position.PosicaoAll[0] == null) {
                                    JogoById[0].Position.PosicaoAll[0] = jogada.Player;
                                    acao = true;
                                }
                                break;
                            case "1":
                                if (JogoById[0].Position.PosicaoAll[1] == null) { 
                                    JogoById[0].Position.PosicaoAll[1] = jogada.Player;
                                    acao = true;
                                }
                                break;
                            case "2":
                                if (JogoById[0].Position.PosicaoAll[2] == null) {
                                    JogoById[0].Position.PosicaoAll[2] = jogada.Player;
                                    acao = true;
                                }
                                break;
                                }
                    }

                    else if (jogada.Position.X.Equals("1"))
                    {
                        switch (jogada.Position.Y)
                        {
                            case "0":
                                if (JogoById[0].Position.PosicaoAll[3] == null) { 
                                    JogoById[0].Position.PosicaoAll[3] = jogada.Player;
                                acao = true;
                                    }
                                    break;
                            case "1":
                                if (JogoById[0].Position.PosicaoAll[4] == null) { 
                                    JogoById[0].Position.PosicaoAll[4] = jogada.Player;
                                    acao = true;
                                }
                                break;
                            case "2":
                                if (JogoById[0].Position.PosicaoAll[5] == null) { 
                                    JogoById[0].Position.PosicaoAll[5] = jogada.Player;
                                    acao = true;
                                }
                                break;
            }
                    }

                    else if (jogada.Position.X.Equals("2"))
                    {
                        switch (jogada.Position.Y)
                        {
                            case "0":
                                if (JogoById[0].Position.PosicaoAll[6] == null) { 
                                    JogoById[0].Position.PosicaoAll[6] = jogada.Player;
                                    acao = true;
                                }
                                break;
                            case "1":
                                if (JogoById[0].Position.PosicaoAll[7] == null) { 
                                    JogoById[0].Position.PosicaoAll[7] = jogada.Player;
                                    acao = true;
                                }
                                break;
                            case "2":
                                if (JogoById[0].Position.PosicaoAll[8] == null) { 
                                    JogoById[0].Position.PosicaoAll[8] = jogada.Player;
                                    acao = true;
                                }
                                break;
                        }
                    }
                }
                if (acao)
                {
                    JogoById[0].LastTurn = jogada.Player;
                    JogoById[0].Status = ResultadoStatus.TestaVencedor(JogoById[0]);
                    if(JogoById[0].Status == null)
                        JogoById[0].Status = "Mudou";
                    return JogoById[0];
                }
                else if (JogoById[0].Status == "Mudou" )
                {
                    JogoById[0].Status = null;
                    return JogoById[0];
                }
                else if(JogoById[0].Status == "X" || JogoById[0].Status == "O")
                {
                    return JogoById[0];
                }
                else
                {
                    throw new Exception("Jogada Invalida!");
                }
            }
        }

    }
}
