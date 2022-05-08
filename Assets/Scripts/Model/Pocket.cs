namespace Model
{
    /// <summary>
    /// Кошелек с монетами
    /// </summary>
    public class Pocket
    {
        private int _silverCoins;
        private int _goldenCoins;

        /// <summary>
        /// Возвращает количество серебрянных монет
        /// </summary>
        public int SilverCoins => _silverCoins;
        
        /// <summary>
        /// Возвращает количество золотых монет
        /// </summary>
        public int GoldenCoins => _goldenCoins;

        public void PutSilverCoin()
        {
            _silverCoins++;
        }

        public void PutGoldenCoin()
        {
            _goldenCoins++;
        }
    }
}