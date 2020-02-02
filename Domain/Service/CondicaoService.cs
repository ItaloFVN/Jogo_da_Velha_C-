using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jogo_da_Velha_Cs_3._0.Controllers;
using Jogo_da_Velha_Cs_3._0.Domain.Model;
using Jogo_da_Velha_Cs_3._0.Domain.Service;

namespace Jogo_da_Velha_Cs_3._0.Domain.Service
{
    public class CondicaoService
    {
        public string vencedor;

        //Metodo que define se alguem ganhou a partida
        //Jogo -> Objeto JSON com as informações do jogo
        public string TestaVencedor(Jogo jogo)
        {
            // condicao -> Salva os resultados dos testes para vitoria
            vencedor = null;
            string condicao = TestaDiagonal(jogo);
            if (condicao != null)
            {
                return condicao;
            };

            condicao = TestaLinhas(jogo);
            if (condicao != null)
            {
                return condicao;
            };

            condicao = TestaColunas(jogo);
            if (condicao != null)
            {
                return condicao;
            };

            condicao = TestaEmpate(jogo);
            if (condicao != null)
            {
                return condicao;
            };

            return condicao;
        }

        //Metodo que define se alguem ganhou a partida pelas casas nas diagonais
        //Jogo -> Objeto JSON com as informações do jogo
        public string TestaDiagonal(Jogo jogo)
        {
            //condicao -> salva o numero de vezes que X|O aparecerem diagonalmente em um sentido
            int condicaoX = 0;
            int condicaoO = 0;

            //vencedor-> salva o vencedor da partida

            //testes para vitoria na diagonal da esquerda para a direita | de cima para baixo
            for (int i = 0; i <= 8; i=i+4)
            {
                if (jogo.Position.PosicaoAll[i] == "X")
                {
                    condicaoX++;
                }
            }
            for (int i = 0; i <= 8; i = i + 4)
            {
                if (jogo.Position.PosicaoAll[i] == "O")
                {
                    condicaoO++;
                }
            }
            if (condicaoX == 3)
            {
                vencedor = "X";
            }
            else if (condicaoO == 3)
            {
                vencedor = "O";
            }
            //testes para vitoria na diagonal da direita para esquerda | de cima para baixo
            else
            {
                condicaoX = 0;
                condicaoO = 0;

                for (int i = 2; i <= 6; i = i+2)
                {
                    if (jogo.Position.PosicaoAll[i] == "X")
                    {
                        condicaoX++;
                    }
                }
                for (int i = 2; i <= 6; i = i + 2)
                {
                    if (jogo.Position.PosicaoAll[i] == "O")
                    {
                        condicaoO++;
                    }
                }
                if (condicaoX == 3)
                {
                    vencedor = "X";
                }
                else if (condicaoO == 3)
                {
                    vencedor = "O";
                }
            }

            return vencedor;
        }

        //Metodo que define se alguem ganhou a partida a partir das linhas
        //Jogo -> Objeto JSON com as informações do jogo
        public string TestaLinhas(Jogo jogo)
        {
            //vencedor-> salva o vencedor da partida
            string vencedor = null;
            int condicaoX = 0;
            int condicaoO = 0;

            //testes para vitoria apartir das linhas
            int z = 0;
            for (int i = 0; i <= 2; i++)
            {
                for(int j = z;  j<z+3; j++)
                {
                    if (jogo.Position.PosicaoAll[j] == "X")
                        condicaoX++;
                }
                if(condicaoX == 3)
                {
                    vencedor = "X";
                }
                else
                {
                    condicaoX = 0;
                }
                z = z + 3;

            }
            z = 0;
            for (int i = 0; i <= 2; i++)
            {
                for (int j = z; j < z + 3; j++)
                {
                    if (jogo.Position.PosicaoAll[j] == "O")
                        condicaoO++;
                }
                if (condicaoO == 3)
                {
                    vencedor = "O";
                }
                else
                {
                    condicaoO = 0;
                }
                z = z + 3;
            }

            return vencedor;
        }

        //Metodo que define se alguem ganhou a partida a partir das colunas
        //Jogo -> Objeto JSON com as informações do jogo
        public string TestaColunas(Jogo jogo)
        {
            int condicaoX = 0;
            int condicaoO = 0;


            //testes para vitoria apartir das colunas
            int z = -1;
            for (var i = 0; i <= 2; i++)
            {
                z++;
                for(int j = z; j < z + 7; j=j+3)
                {
                    if (jogo.Position.PosicaoAll[j] == "X")
                    {
                        condicaoX++;
                    }
                }
                if (condicaoX == 3)
                {
                    vencedor = "X";
                }
                else
                {
                    condicaoX = 0;
                }
            }
            z = -1;
            for (var i = 0; i <= 2; i++)
            {
                z++;
                for (int j = z; j < z + 7; j = j + 3)
                {
                    if (jogo.Position.PosicaoAll[j] == "O")
                    {
                        condicaoO++;
                    }
                }
                if (condicaoO == 3)
                {
                    vencedor = "O";
                }
                else
                {
                    condicaoO = 0;
                }
            }

            return vencedor;
        }

        //Metodo que define se ocorreu empate
        //Jogo -> Objeto JSON com as informações do jogo
        public string TestaEmpate(Jogo jogo)
        {
            //condicao -> salva o numero de 'casas' ocupadas
            int condicao = 0;

            //teste para ver o numero de casas ocupadas
            for (var i = 0; i <= 2; i++) { 
                if (jogo.Position.PosicaoAll[i] != null)
                    condicao++;
            }
            if (condicao == 9)
            {
                return "Empate";
            }
            return null;
        }
    }
}
