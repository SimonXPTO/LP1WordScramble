namespace WordScramble
{
    /// <summary>
    /// Represents the result of a game, including the word typed and the time 
    /// taken to complete it.
    /// </summary>
    public class GameResult
    {
        /// <summary>
        /// Gets the word that the player had to unscramble in the game.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> representing the word that the player had to 
        /// unscramble in the game.
        /// </value>
        public string Word { get; }

        /// <summary>
        /// Gets the time taken to guess the word.
        /// </summary>
        /// <value>
        /// A <see cref="double"/> representing the time in seconds taken to 
        /// guess the word.
        /// </value>
        public double TimeTaken { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameResult"/> class 
        /// with the specified word and time taken.
        /// </summary>
        /// <param name="word">The correct word unscrambled, of type 
        /// <see cref="string"/>.</param>
        /// <param name="timeTaken">The time taken to guess the word, of type 
        /// <see cref="double"/> (in seconds).</param>
        public GameResult(string word, double timeTaken)
        {
            Word = word;
            TimeTaken = timeTaken;
        }
    }
}