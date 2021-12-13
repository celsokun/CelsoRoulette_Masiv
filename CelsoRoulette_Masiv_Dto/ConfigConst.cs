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
        public const string BETSAVE = "Apuesta Guardada.";
        public const string ERRORCLOSEROULETTE = "Ya la ruleta se encuentra cerrada.";
        public const string ERRORNOTEXITSROULETTE = "No existe la ruleta.";
        public const string ERRORBETCOLORORNUMBER = "Numeros y colores no validos.";
        public const string ERRORBETCOLORANDNUMBER = "Solo puede apostar por numeros o colores en cada apuesta.";
        public const string ERRORUSERID = "Error no se lee el usuario.";
        public const string CLOSEROULETTE = "Ruleta Cerrada.";
        public const string ERRORNOFOUDROULETTE = "No se encuentra la ruleta.";
    }
}
