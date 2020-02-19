namespace Comlib.Mapper
{
    public class ObjectMapperFactory
    {
        private ObjectMapperFactory()
        {
        }

        private static readonly object locker = new object();
        private static IObjectMapper _objectMapper = null;

        public static IObjectMapper ObjectMapper
        {
            get
            {
                if (_objectMapper == null)
                {
                    lock (locker)
                    {
                        return new ObjectAutoMapper();
                    }
                }
                return _objectMapper;
            }
        }
    }
}