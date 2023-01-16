namespace OnlineShop.Messaging.Service.Utils
{
    internal class SimpleLock
    {
        private int _locked;

        public bool TryEnter()
        {
            return Interlocked.CompareExchange(ref _locked, 1, 0) == 0;
        }

        public void Exit()
        {
            _locked = 0;
        }
    }
}
