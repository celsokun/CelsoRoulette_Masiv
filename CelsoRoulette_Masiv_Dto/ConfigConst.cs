using System;
namespace CelsoRoulette_Masiv_Dto
{
    public static class ConfigConst
    {
        public static string RouletteColeccion() { return Environment.GetEnvironmentVariable("RouletteColeccion"); }
        public static string ServerName() { return Environment.GetEnvironmentVariable("ServerName"); }
        public const string ERRORCREATEREOULETTE = "Error al crear la ruleta.";
    }
}
