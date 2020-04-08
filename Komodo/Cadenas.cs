using System;
using System.Collections.Generic;
using System.Text;

namespace Komodo
{

  public  class Cadenas
    {

        protected string bienvenida = "Información Importante";
        protected string mensajebienvenida = "\nDebe autorizar el uso de la camara para escanear el código QR," +
            "si actualmente su dispositivo no es compatible con esta versión de software la camara estará en blanco" +
            ",sin embargo no debe preocuparse;solo debe cerrar esta aplicación y volver " +
            "a iniciarla  e instalaremos los paquetes necesarios luego de que acepte el uso de la camara." +
            "\n \n Este paso solo se realizará una unica vez";
        protected string deacuerdo = "De acuerdo";
        protected string lectorqr = "Lector QR Komodo";
        protected string valorobtenido = "Valor Obtenido";
        protected string ok = "OK";

        protected string error = "Error";
        protected string mensajeerror = "Has ingresado un Qr errado";
        protected string qrtop = "Ubica el Código de barras frente al dispositivo";



        public string Bienvenida { get => bienvenida; set => bienvenida = value; }
        public string Mensajebienvenida { get => mensajebienvenida; set => mensajebienvenida = value; }
        public string Deacuerdo { get => deacuerdo; set => deacuerdo = value; }
        public string Lectorqr { get => lectorqr; set => lectorqr = value; }
        public string Valorobtenido { get => valorobtenido; set => valorobtenido = value; }
        public string Ok { get => ok; set => ok = value; }

        public string Error { get => error; set => error = value; }
        public string MensajeError { get => mensajeerror; set => mensajeerror = value; }
        public string QrTop { get => qrtop; set => qrtop = value; }





    }
}
