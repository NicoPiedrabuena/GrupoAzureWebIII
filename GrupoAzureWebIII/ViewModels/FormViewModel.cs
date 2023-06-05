﻿using System.ComponentModel.DataAnnotations;

namespace GrupoAzureWebIII.ViewModels
{
    public class FormViewModel
    {
        [Required]
        public string mensaje { get; set; }
        public bool publicarTwitter { get; set; }
        public bool publicarEmail { get; set; }

        public string apodo { get; set; }
        public string user { get; set; }


    }
}
