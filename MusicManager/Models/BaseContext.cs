using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MusicManager.Models
{
	public class BaseContext : DbContext
	{
		public BaseContext() : base("context")
		{

		}

		public DbSet<Genre> Genres { get; set; }
		public DbSet<MusicLibrary> MusicLibraries { get; set; }
		public DbSet<Song> Songs{ get; set; }

	}
}