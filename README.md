# XadrezGame / Chess Game in C#

![image](https://github.com/sidarkelse/XadrezGame---ChessGame/assets/48395891/0a447806-73c1-4a09-aab6-c3f245a45555)


## Visão Geral / Overview

- Este é um simples jogo de xadrez implementado em C#, onde os jogadores podem desfrutar do jogo clássico de xadrez entre si.
- This is a simple chess game implemented in C#, where players can enjoy the classic game of chess against each other.

## Funcionalidades / Features

- Jogabilidade clássica de xadrez: O jogo segue as regras básicas do xadrez, onde os jogadores devem mover suas peças de acordo com as regras de movimento de cada peça, capturar peças adversárias e tentar dar xeque-mate no rei adversário.
 - Classic chess gameplay: The game follows the basic rules of chess, where players must move their pieces according to each piece's movement rules, capture opponent pieces, and try to checkmate the opponent's king.
- Regras de xadrez básicas implementadas: O jogo inclui várias regras básicas do xadrez, como:
 - Basic chess rules implemented: The game includes various basic chess rules, such as:
  - **Roque (Castling)**
    - *Português:* Permite que o rei e uma das torres movam-se simultaneamente, sob certas condições.
    - *English:* Allows the king and one of the rooks to move together under certain conditions.
  - **En Passant**
    - *Português:* Um movimento especial disponível apenas para peões, que ocorre após um peão adversário avançar duas casas a partir da posição inicial.
    - *English:* A special move available only to pawns, which occurs after an opponent pawn moves two squares from its starting position.
  - **Regra das 50 Jogadas (50-move Rule)**
    - *Português:* Se nenhuma peça for capturada e nenhum peão for movido em 50 movimentos consecutivos, o jogo é considerado um empate.
    - *English:* If no piece is captured and no pawn is moved in 50 consecutive moves, the game is considered a draw.
  - **Promoção de Peões (Pawn Promotion)**
    - *Português:* Quando um peão alcança a oitava fileira, ele pode ser promovido a qualquer outra peça, exceto um rei.
    - *English:* When a pawn reaches the eighth rank, it can be promoted to any other piece except a king.
  - **Afogamento (Stalemate)**
    - *Português:* O jogo termina em empate quando o rei de um jogador não está em xeque, mas o jogador não tem nenhum movimento legal disponível.
    - *English:* The game ends in a draw when a player's king is not in check, but the player has no legal moves available.

## Como Jogar / How to Play

1. 1. - Clone o repositório para a sua máquina local:
   - Clone the repository to your local machine:
   --- git clone https://github.com/sidarkelse/XadrezGame---ChessGame
  
2. - Abra a pasta onde você clonou o projeto e execute o arquivo XadrezChess.Sln.

Open the folder where you cloned the project and run the XadrezChess.Sln file.

3. - Assim que o seu Visual Studio abrir com o projeto, navegue até o projeto ChessUI e clique com o botão direito nele. Em seguida, escolha a opção Set as Startup Project para iniciar o projeto com ChessUI. Após iniciar o projeto, ele será executado sem erros, pronto para ser jogado ou testado.

Once your Visual Studio opens with the project, navigate to the ChessUI project and right-click on it. Then, select the option Set as Startup Project to start the project with ChessUI. After starting the project, it will run without errors, ready to be played or tested.




