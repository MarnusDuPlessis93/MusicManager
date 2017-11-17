using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicManager.Models
{
	public class MusicLibrary
	{
		[Key]
		[Column("Id")]
		public int Id { get; set; }
		[Column("SongName")]
		public string SongName { get; set; }
		[Column("Album")]
		public string Album { get; set; }
		[Column("Artist")]
		public string Artist { get; set; }
		[Column("DateOfAlbum")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime? DateOfAlbum { get; set; }
		[Column("GenreId")]
		public int? GenreId { get; set; }
		[Column(TypeName = "image")]
		public byte[] AlbumArt { get; set; }
		[ForeignKey("GenreId")]
		public virtual Genre Genre { get; set; }

	}

	public class Song
	{
		public int Id { get; set; }
		public int MusicLibraryId { get; set; }
		[Column("SongByte", TypeName = "image")]
		public byte[] SongByte { get; set; }
		[ForeignKey("MusicLibraryId")]
		public virtual MusicLibrary MusicLibraries { get; set; }
	}
}