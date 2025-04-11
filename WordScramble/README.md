

``` mermaid
classDiagram
    class `Game`
    class `GameResult`
    class `program`
    class `WordProvider`

    `program` --> `Game`
    `Game` o--> `GameResult`
    `Game` --> `WordProvider`

    class program{
        
    }

    class GameResult{
        +Word: string
        +TimeTaken: double   
    }

    class Game{
        -gameStats : GameResult[]
        -wordProvider: WordProvider

        +ShowMenu() void
        -StartGame() void
        -ShowGameStats() void
        -ShowBreakDownChart() void
    }

    class WordProvider{
        -Words: List
        -random: Random
        +GetRandomWord() string
        +GetScrambledWord() string
    }

    ```