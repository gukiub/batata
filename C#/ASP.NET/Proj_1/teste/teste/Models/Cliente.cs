﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace teste.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}