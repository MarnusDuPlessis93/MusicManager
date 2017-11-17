using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicManager.Services;
using MusicManager.Models;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace MusicManager.Areas.Music.Controllers
{
	public class MusicController : Controller
	{
		public ActionResult Index()
		{
			var musicService = new MusicService();
			var musicLibraryModelList = musicService.GetMusicLibraryList();

			ViewBag.Genres = musicService.GetGenreList();

			return View(musicLibraryModelList);
		}

		[HttpPost]
		public ActionResult Index(FormCollection collection)
		{
			var musicService = new MusicService();
			var musicLibraryModelList = musicService.GetMusicLibraryList();

			var searchText = collection["SearchSong"];

			var searchMusic = musicLibraryModelList;
			if (searchText != "")
			{
				searchMusic = musicLibraryModelList.Where(o => o.SongName.Contains(searchText) || o.Album.Contains(searchText) || o.Artist.Contains(searchText)).ToList();
			}
			

			if(collection["SearchGenre"] != "")
			{
				var genreId =  Convert.ToInt16(collection["SearchGenre"]);

				searchMusic = searchMusic.Where(o => o.GenreId == genreId).ToList();
			}

			ViewBag.Genres = musicService.GetGenreList();


			return View(searchMusic);
		}

		public ActionResult UpdateMusic(int id, string songName, string album, string artist, int genreId, string albumDate)
		{
			var musicService = new MusicService();
			var musicLibrary = musicService.GetMusicLibrary(id);
			musicLibrary.SongName = songName;
			musicLibrary.Album = album;
			musicLibrary.Artist = artist;
			musicLibrary.GenreId = genreId;
			if(albumDate != string.Empty)
			{
				musicLibrary.DateOfAlbum = Convert.ToDateTime(albumDate);
			}

			musicService.UpdateMusicLibrary(musicLibrary);

			return null;
		}

		public ActionResult ShowAlbumArt(int id)
		{
			var musicService = new MusicService();
			var musicLibrary = musicService.GetMusicLibrary(id);

			var destRect = new Rectangle(0, 0, 50, 50);
			var destImage = new Bitmap(50,50);

			if (musicLibrary.AlbumArt != null)
			{
				using (var ms = new MemoryStream(musicLibrary.AlbumArt))
				{

					var albumArt = Image.FromStream(ms);
					destImage.SetResolution(albumArt.HorizontalResolution, albumArt.VerticalResolution);
					using (var graphics = Graphics.FromImage(albumArt))
					{
						graphics.CompositingMode = CompositingMode.SourceCopy;
						graphics.CompositingQuality = CompositingQuality.HighQuality;
						graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
						graphics.SmoothingMode = SmoothingMode.HighQuality;
						graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

						using (var wrapMode = new ImageAttributes())
						{
							wrapMode.SetWrapMode(WrapMode.TileFlipXY);
							graphics.DrawImage(albumArt, destRect, 0, 0, albumArt.Width, albumArt.Height, GraphicsUnit.Pixel, wrapMode);

							var converter = new ImageConverter();

							var returnImageByteArray = (byte[])converter.ConvertTo(destImage, typeof(byte[]));

							return File(returnImageByteArray, "image/jpg");
						}
					}

				}
			}

			return null;

			
			
		}

		public ActionResult AddNew()
		{
			var musicService = new MusicService();

			var musicLibraryModel = new MusicLibrary();

			ViewBag.Genres = musicService.GetGenreList();

			return View(musicLibraryModel);
		}

		[HttpPost]
		public ActionResult AddNew(FormCollection collection, HttpPostedFileBase albumArt, HttpPostedFileBase song)
		{
			var musicService = new MusicService();

			var musicLibraryModel = new MusicLibrary();

			musicLibraryModel.SongName = collection["SongName"];
			musicLibraryModel.Album = collection["Album"];
			musicLibraryModel.Artist = collection["Artist"];
			if(collection["GenreId"] != "")
			{
				musicLibraryModel.GenreId = Convert.ToInt16(collection["GenreId"]);
			}

			if(collection["DateOfAlbum"] != "")
			{
				musicLibraryModel.DateOfAlbum = Convert.ToDateTime(collection["DateOfAlbum"]);
			}

			if(albumArt != null)
			{
				var getByteArray = ReadFully(albumArt.InputStream);
				var getContent = Convert.ToBase64String(getByteArray);

				musicLibraryModel.AlbumArt = getByteArray;
			}

			var id = musicService.AddMusicLibrary(musicLibraryModel);

			if (song != null)
			{
				var songModel = new Song();
				songModel.MusicLibraryId = id;
				var getByteArray = ReadFully(song.InputStream);
				var getContent = Convert.ToBase64String(getByteArray);

				songModel.SongByte = getByteArray;
			}

			

			return RedirectToAction("AddNew");
		}

		public static byte[] ReadFully(Stream input)
		{
			byte[] buffer = new byte[input.Length];

			using (MemoryStream ms = new MemoryStream())
			{
				int read;
				while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
				{
					ms.Write(buffer, 0, read);
				}

				return ms.ToArray();
			}
		}

		public ActionResult Delete(int Id)
		{
			var musicService = new MusicService();

			var musicLibrary = musicService.GetMusicLibrary(Id);

			musicService.DeleteMusic(musicLibrary);

			return RedirectToAction("Index", "Music");
		}





	}
}