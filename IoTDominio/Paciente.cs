﻿using System;
using System.Collections.Generic;

namespace IoTDominio
{
    public class Paciente
    {
        public int IdPaciente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public List<Cardio> Cardio { get; set; }
        public List<Oxigeno> Oxigeno { get; set; }
    }
}
