namespace Session.Redis.Helpers.SessionProvider
{
    public static class SessionProvider
    {
        private static ISessionProvider _current;

        public static ISessionProvider Current
        {
            get
            {
                return _current ?? (_current = new HttpContextSessionProvider());
            }
        }
        }

    
}