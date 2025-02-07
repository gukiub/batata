﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace teste.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(150, ErrorMessage = "Maximo de caracteres excedido")]
        [MinLength(2, ErrorMessage = "Minimo de 2 caracteres")]
        [DisplayName("Nome do Cliente")]
        [Required(ErrorMessage = "Preencher campo nome")]
        public string Nome { get; set; }

        [MaxLength(150, ErrorMessage = "Maximo de caracteres excedido")]
        [MinLength(2, ErrorMessage = "Minimo de 2 caracteres")]
        [DisplayName("Nome do Sobrenome")]
        [Required(ErrorMessage = "Preencher campo sobrenome")]
        public string SobreNome { get; set; }
        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }
        [MaxLength(150, ErrorMessage = "Maximo de caracteres excedido")]
        [MinLength(2, ErrorMessage = "Minimo de 2 caracteres")]
        [DisplayName("Nome do E-mail")]
        [Required(ErrorMessage = "Preencher campo E-mail")]
        [EmailAddress(ErrorMessage = "E-mail em formato invalido")]
        public string Email { get; set; }

    }
}