using System;
namespace CelsoRoulette_Masiv_Dto
{
    public static class ConfigConst
    {
        public static string RouletteColeccion() { return Environment.GetEnvironmentVariable("RouletteColeccion"); }
        public static string ServerName() { return Environment.GetEnvironmentVariable("ServerName"); }
        public const string ERRORCREATEROULETTE = "Error al crear la ruleta.";
        public const string ERROROPENROULETTE = "Error al abrir la ruleta.";
        public const string OPENROULETTE = "Ruleta abierta.";
        public const string ERRORBETNUMBER = "Numero por fuera del rango.";
        public const string ERRORBETVALUE = "Apuesta por fuera del rango.";
    }
}
