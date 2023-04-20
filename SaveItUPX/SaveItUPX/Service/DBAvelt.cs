using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace SaveItUPX.Service
{
    internal class DBSAveIt
    {
        private static ISettings AppSettings => CrossSettings.Current;

        public static string Login
        {
            get => AppSettings.GetValueOrDefault(nameof(Login), "");

            set => AppSettings.AddOrUpdateValue(nameof(Login), value);

        }

        public static string NomedoUsuario
        {
            get => AppSettings.GetValueOrDefault(nameof(NomedoUsuario), "");

            set => AppSettings.AddOrUpdateValue(nameof(NomedoUsuario), value);

        }

        public static string RecCodigo
        {
            get => AppSettings.GetValueOrDefault(nameof(RecCodigo), "");

            set => AppSettings.AddOrUpdateValue(nameof(RecCodigo), value);
        }

        public static string Local
        {
            get => AppSettings.GetValueOrDefault(nameof(Local), "");

            set => AppSettings.AddOrUpdateValue(nameof(Local), value);
        }

        public static string NomeImagem
        {
            get => AppSettings.GetValueOrDefault(nameof(NomeImagem), "");

            set => AppSettings.AddOrUpdateValue(nameof(NomeImagem), value);
        }

        public static string Contato01
        {
            get => AppSettings.GetValueOrDefault(nameof(Contato01), "");

            set => AppSettings.AddOrUpdateValue(nameof(Contato01), value);
        }

        public static string Contato02
        {
            get => AppSettings.GetValueOrDefault(nameof(Contato02), "");

            set => AppSettings.AddOrUpdateValue(nameof(Contato02), value);
        }

        public static string Contato03
        {
            get => AppSettings.GetValueOrDefault(nameof(Contato03), "");

            set => AppSettings.AddOrUpdateValue(nameof(Contato03), value);
        }

        public static string Telefone01
        {
            get => AppSettings.GetValueOrDefault(nameof(Telefone01), "");

            set => AppSettings.AddOrUpdateValue(nameof(Telefone01), value);
        }

        public static string Telefone02
        {
            get => AppSettings.GetValueOrDefault(nameof(Telefone02), "");

            set => AppSettings.AddOrUpdateValue(nameof(Telefone02), value);
        }

        public static string Telefone03
        {
            get => AppSettings.GetValueOrDefault(nameof(Telefone03), "");

            set => AppSettings.AddOrUpdateValue(nameof(Telefone03), value);
        }
    }
}
