﻿using System;
using System.Collections.Generic;

namespace GestionFC.Models.PlantillaPage
{
    public class GridPromotoresResponseModel
    {
        public Share.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public List<Progreso> Promotores { get; set; }
    }
}
