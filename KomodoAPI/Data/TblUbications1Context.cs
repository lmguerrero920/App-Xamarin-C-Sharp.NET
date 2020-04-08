using KomodoAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KomodoAPI.Data
{
    public class TblUbications1Context : DbContext
    {

        public TblUbications1Context(DbContextOptions<TblUbications1Context> options) : base (options)
        {


        }

        public DbSet<TblUbications1> UbicationItems { get; set; }

    }
}
