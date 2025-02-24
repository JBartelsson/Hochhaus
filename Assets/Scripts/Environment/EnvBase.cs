public class EnvBase
    {
        protected Environment env;


        public EnvBase(Environment env)
        {
            this.env = env;
        }

        public Environment Env
        {
            get => env;
            set => env = value;
        }
    }
