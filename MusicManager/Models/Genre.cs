using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicManager.Models
{
	public class Genre
	{
		[Key]
		[Column("Id")]
		public int Id { get; set; }
		[Column("Name")]
		public string Name { get; set; }

	}
}