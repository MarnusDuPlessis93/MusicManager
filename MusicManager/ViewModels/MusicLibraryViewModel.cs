using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicManager.ViewModels
{
	public class MusicLibrary
	{
		public int Id { get; set; }
		public string SongName { get; set; }
		public string Album { get; set; }
		public string Artist { get; set; }
		public DateTime? DateOfAlbum { get; set; }
		public int? GenreId { get; set; }
		public string SongPath { get; set; }
		public byte[] AlbumArt { get; set; }

	}
}
