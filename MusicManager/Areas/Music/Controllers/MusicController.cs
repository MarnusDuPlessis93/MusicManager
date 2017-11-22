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
using System.Media;
using System.Diagnostics;

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
				searchMusic = musicLibraryModelList.Where(o => o.SongName.ToLower().Contains(searchText.ToLower()) || o.Album.ToLower().Contains(searchText.ToLower()) || o.Artist.ToLower().Contains(searchText.ToLower())).ToList();
			}
			

			if(collection["SearchGenre"] != "")
			{
				var genreId =  Convert.ToInt16(collection["SearchGenre"]);

				searchMusic = searchMusic.Where(o => o.GenreId == genreId).ToList();
			}

			ViewBag.Genres = musicService.GetGenreList();


			return View(searchMusic);
		}

		[HttpPost]
		public ActionResult UpdateMusic(int id, string songName, string album, string artist, int genreId, string albumDate)
		{
			var musicService = new MusicService();
			var musicLibrary = musicService.GetMusicLibrary(id);
			musicLibrary.SongName = songName;
			musicLibrary.Album = album;
			musicLibrary.Artist = artist;
			musicLibrary.GenreId = genreId;
			if(albumDate != null)
			{
				musicLibrary.DateOfAlbum = Convert.ToDateTime(albumDate);
			}

			musicService.UpdateMusicLibrary(musicLibrary);

			var modelList = musicService.GetMusicLibraryList();

			return Json("");
		}

		public ActionResult ShowAlbumArt(int id)
		{
			var musicService = new MusicService();
			var musicLibrary = musicService.GetMusicLibrary(id);

			return File(musicLibrary.AlbumArt, "image/jpeg");
		}

		public ActionResult AddNew()
		{
			var musicService = new MusicService();

			var musicLibraryModel = new MusicLibrary();

			ViewBag.Genres = musicService.GetGenreList();

			ViewBag.ShowMessage = Session["ShowMessage"];

			Session["ShowMessage"] = "";

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

			int? songId = null;
			if (song != null)
			{
				var songModel = new Song();

				var getByteArray = ReadFully(song.InputStream);
				var getContent = Convert.ToBase64String(getByteArray);

				songModel.SongByte = getByteArray;

				songId = musicService.AddSong(songModel);
			}

			musicLibraryModel.SongId = songId;

			if (albumArt != null)
			{
				var getByteArray = ReadFully(albumArt.InputStream);
				var getContent = Convert.ToBase64String(getByteArray);

				musicLibraryModel.AlbumArt = ResizeImage(getByteArray);
			}

			var id = musicService.AddMusicLibrary(musicLibraryModel);

			Session["ShowMessage"] = collection["SongName"] + " successfully added!";
			return RedirectToAction("AddNew");
		}

		public byte[] ResizeImage(byte[] byteArray)
		{
			System.IO.MemoryStream myMemStream = new System.IO.MemoryStream(byteArray);
			System.Drawing.Image fullsizeImage = System.Drawing.Image.FromStream(myMemStream);
			System.Drawing.Image newImage = fullsizeImage.GetThumbnailImage(40, 40, null, IntPtr.Zero);
			System.IO.MemoryStream myResult = new System.IO.MemoryStream();
			newImage.Save(myResult, System.Drawing.Imaging.ImageFormat.Gif);

			return myResult.ToArray();
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

			musicService.RemoveMusic(Id);

			return RedirectToAction("Index", "Music");

		}

		public ActionResult DownloadSong(int id)
		{
			var musicService = new MusicService();

			var musicLibrary = musicService.GetMusicLibrary(id);

			return File(musicLibrary.Song.SongByte, "audio/mpeg", musicLibrary.SongName + ".mp3");
		}

		public ActionResult Manage()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Manage(FormCollection collection)
		{
			var musicService = new MusicService();

			var genreModel = new Genre();
			genreModel.Name = collection["NewGenre"];

			musicService.AddNewGenre(genreModel);

			return View();
		}

		public ActionResult About()
		{
			return View();
		}

		[HttpPost]
		public ActionResult DeleteSelected(string ids)
		{
			var musicService = new MusicService();

			var idArray = ids.Split('|');

			for(var i = 0; i < idArray.Count(); i++)
			{
				if(idArray[i] != "")
				{
					musicService.RemoveMusic(Convert.ToInt32(idArray[i]));
				}
			}

			return Json("");

		}

		public ActionResult PlaySong(int id)
		{
			var musicService = new MusicService();

			var musicLibrary = musicService.GetMusicLibrary(id);

			var bytes = musicLibrary.Song.SongByte;
			string name = Path.ChangeExtension(Path.GetRandomFileName(), ".mp3");
			string path = Path.Combine(Path.GetTempPath(), name);
			System.IO.File.WriteAllBytes(path, bytes);
			Process.Start(path);

			return null;
		}
	}
}