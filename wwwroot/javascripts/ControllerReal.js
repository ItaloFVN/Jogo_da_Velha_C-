var turnoJogador = null,
    app = angular.module('app', []);


var controle = function ($scope, $http) {
    $scope.modalJogos = false;
    $scope.jogos = [];
    $scope.statusJogo = false;
    $scope.id = '';
    $scope.jogador = "";
    $scope.labelId = "Codigo do Jogo";
    $scope.labelJogador = "Jogador";
    $scope.resultado = '';
    $scope.linha0 = [
        { class: "casa", positionX: "0", positionY: "0", evento: "realizaJogada($event)" },
        { class: "casa", positionX: "0", positionY: "1", evento: "realizaJogada($event)" },
        { class: "casa", positionX: "0", positionY: "2", evento: "realizaJogada($event)" }
    ]
    $scope.linha1 = [
        { class: "casa", positionX: "1", positionY: "0", evento: "realizaJogada($event)" },
        { class: "casa", positionX: "1", positionY: "1", evento: "realizaJogada($event)" },
        { class: "casa", positionX: "1", positionY: "2", evento: "realizaJogada($event)" }
    ]
    $scope.linha2 = [
        { class: "casa", positionX: "2", positionY: "0", evento: "realizaJogada($event)" },
        { class: "casa", positionX: "2", positionY: "1", evento: "realizaJogada($event)" },
        { class: "casa", positionX: "2", positionY: "2", evento: "realizaJogada($event)" }
    ]


    $scope.criarJogo = function () {
        $http({
            method: 'POST',
            url: 'https://localhost:44326/game'
        }).then(function successo(response) {
            carregaJogo(response.data);
        }, function erro(response) {
            alert('Erro de conexão: Codigo ' + JSON.stringify(response.status));
        });
    }

    $scope.realizaJogada = function ($event) {
        if ($scope.id != '') {
            var params = {
                "Id": $scope.id,
                "Player": $scope.jogador,
                "Position": {
                    "X": $event.target.attributes.positionX.value,
                    "Y": $event.target.attributes.positionY.value
                }
            }
            $http({
                method: 'POST',
                url: 'https://localhost:44326/game/' + $scope.id + '/movement',
                dataType: 'json',
                data: params,
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function successo(response) {
                //Jogada realizada com sucesso
                if (response.status == 200) {
                    console.log(response.data);
                    if (response.data.status == "Mudou") {
                        carregaJogada(params, $event);
                    }
                    if (response.data.status == 'X' ||
                        response.data.status == 'O' ||
                        response.data.status == "Empate") {
                        if ($scope.statusJogo == false)
                            carregaJogada(params, $event);
                        terminaJogo(response.data)
                    }
                }

            }, function erro(response) {
                alert('Erro: Codigo ' + JSON.stringify(response.status));
            });
        }
        else {
            alert("Codigo de jogo não inserido!");
        }
    }
    //Hide or show
    $scope.alteraModal = function () {
        if ($scope.modalJogos == true) {
            $scope.modalJogos = false
        } else {
            $scope.modalJogos = true
        }
    }

    //retorna o valor da modal
    $scope.mostraModal = function () {
        return $scope.modalJogos;
    }

    //busca um jogo pelo id
    $scope.recebeArquivo = function ($event) {
        $http({
            method: 'POST',
            url: 'https://localhost:44326/game/load',
            dataType: 'json',
            data: { id: $scope.id },
            headers: {
                "Content-Type": "application/json"
            }
        }).then(function successo(response) {
            console.log(response.data);
            carregaArquivo(response.data);
        }, function erro(response) {
            alert('Erro de conexão: Codigo ' + JSON.stringify(response.status));
        });
    }

    //Resultado do jogo
    $scope.exibirResultado = function () {
        return $scope.statusJogo;
    }

    //Zera as casas ao iniciar/carregar jogo
    $scope.zeraJogo = function () {
        var casas = document.querySelectorAll(".casa");
        console.log(casas);
        for (const casa of casas) {
            casa.style.background = '';
        }
        $scope.resultado = "";
        $scope.statusJogo = false;
    }

    //Busca os jogos na memoria
    $scope.buscaJogos = function () {
        $http({
            method: 'POST',
            url: 'https://localhost:44326/loadjogos',
        }).then(function successo(response) {
            for (let index = 0; index < response.data.jogos.length; index++) {
                $scope.jogos.push(response.data.jogos[index]);

            }
            console.log(response.data);
        }, function erro(response) {
            alert('Erro de conexão: Codigo ' + JSON.stringify(response.status));
        });
    }

    //Exibe o Id/Jogador do jogo atual
    function carregaJogo(resultado) {
        $scope.id = resultado.id;
        $scope.jogador = resultado.firstPlayer;
        turnoJogador = resultado.firstPlayer;
    }

    //Altera o fundo da casa
    function carregaJogada(params, event) {
        var imagemFundo = angular.element(event.target),
            fig = "url(images/" + params.Player + ".jpg)";


        imagemFundo.css({ 'background': fig });

        if (params.Player == "X") {
            turnoJogador = "O"
        } else {
            turnoJogador = "X"
        }

        $scope.jogador = turnoJogador;
    }

    //Encerra o jogo de acordo com o winner
    function terminaJogo(params) {
        //console.log(params);
        $scope.statusJogo = true;
        if (params.status == "Empate")
            $scope.resultado = params.status
        else
            $scope.resultado = "O jogador " + params.status + " venceu!";
    }

    //Alert para erros
    function alerta(params) {
        alert(params.message);
    }

    //Altera as casas para um jogo carregado
    function carregaArquivo(params) {
        var casas = document.querySelectorAll(".casa"),
            posicoes = params.position.posicaoAll;

        for (let index = 0; index < casas.length; index++) {
            if (posicoes[index] == 'X')
                casas[index].style.background = "url(images/X.jpg)";
            else if (posicoes[index] == 'O')
                casas[index].style.background = "url(images/O.jpg)";
            else
                casas[index].style.background = '';
        }
        if (params.status == 'X' || params.status == 'O') {
            $scope.statusJogo = true;
            $scope.jogador = "";
            $scope.resultado = "O jogador " + params.status + " venceu!";
        } else if (params.status == "Empate") {
            $scope.statusJogo = true;
            $scope.resultado = "Empate!";
        } else {
            $scope.statusJogo = false;
            $scope.resultado = "";
            if (params.lastTurn == 'X')
                $scope.jogador = 'O';
            else
                $scope.jogador = 'X';
        }
    }
}
app.controller('controller', function ($scope, $http) {
    controle($scope, $http);
});


app.directive('modal', function () {
    return {
        templateUrl: './modal.html'
    }
});

app.directive('hashTag', function () {
    return {
        templateUrl: './hashTag.html',
    }
})

app.directive('info', function () {
    return {
        templateUrl: './informacoes.html',
    }
})
