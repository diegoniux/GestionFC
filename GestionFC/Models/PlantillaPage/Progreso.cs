﻿using System;
namespace GestionFC.Models.PlantillaPage
{
    public class Progreso
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Foto { get; set; }
        public string Genero { get; set; }
        public string ColorIndicadorMeta { get; set; }
        public string SaldoVirtual { get; set; }
        public string SaldoCantadoFCT { get; set; }
        public string SaldoAcumulado { get; set; }
        public string PorcentajeSaldoAcumulado { get; set; }
        public string PorcentajeSaldoVirtual { get; set; }
        public string PorcentajeSaldoVirtualDesc { get; set; }
        public int FCTInactivos { get; set; }
        public int TramitesCertificados { get; set; }
    }
}
