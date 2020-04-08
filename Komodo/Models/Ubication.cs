using System;
using System.Collections.Generic;
using System.Text;

namespace Komodo.Models
{
   public class Ubication
    {

      
        public int Id { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        //public string Consolidado
        //{
        //    get
        //    {
        //        return $"{Id}, {Description},{Phone},{Address},{Latitude},{Longitude}";
        //    }
        //}
    }
}
