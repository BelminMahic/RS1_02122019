﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2019_12_02.EntityModels
{
    public class PopravniIspitStavka
    {
        public int Id { get; set; }
        public PopravniIspit PopravniIspit { get; set; }
        public int PopravniIspitId { get; set; }
        public OdjeljenjeStavka OdjeljenjeStavka { get; set; }
        public int OdjeljenjeStavkaId { get; set; }
        public bool? IsPristupio { get; set; }
        public int Bodovi { get; set; }
    }
}
