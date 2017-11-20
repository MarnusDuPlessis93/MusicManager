using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicManager.Models;
using System.IO;

namespace MusicManager.Services
{
	public class MusicService
	{
		public List<Genre> GetGenreList()
		{
			using (var context = new BaseContext())
			{

				var genreList = context.Genres.OrderBy(o => o.Name).ToList();
				return genreList;
			}
		}

		public bool AddGenres(List<Genre> GenreList)
		{
			using (var context = new BaseContext())
			{
				context.Genres.AddRange(GenreList);
				return true;
			}
		}

		public List<MusicLibrary> GetMusicLibraryList()
		{
			using (var context = new BaseContext())
			{
				return context.MusicLibraries.Include("Genre").Include("Song").ToList();

			}
		}

		public int AddMusicLibrary(MusicLibrary musicLibrary)
		{
			using (var context = new BaseContext())
			{
				context.MusicLibraries.Add(musicLibrary);
				context.SaveChanges();
				return musicLibrary.Id;

			}
		}

		public MusicLibrary GetMusicLibrary(int id)
		{
			using (var context = new BaseContext())
			{
				return context.MusicLibraries.Include("Song").First(o => o.Id == id);
			}
		}

		public void UpdateMusicLibrary(MusicLibrary musicLibrary)
		{
			using (var context = new BaseContext())
			{
				context.Entry(musicLibrary).State = System.Data.Entity.EntityState.Modified;
				context.SaveChanges();
			}
		}

		public void RemoveMusic(int Id)
		{
			using (var context = new BaseContext())
			{
				var musicLibrary = context.MusicLibraries.First(o => o.Id == Id);
				context.MusicLibraries.Remove(musicLibrary);
				context.SaveChanges();
			}
		}

		public int AddSong(Song song)
		{
			using (var context = new BaseContext())
			{
				context.Songs.Add(song);
				context.SaveChanges();

				return song.Id;
			}
		}

		public Song GetSong(int id)
		{
			using (var context = new BaseContext())
			{
				return context.Songs.First(o => o.Id == id);
			}
		}

		public void AddNewGenre(Genre genre)
		{
			using (var context = new BaseContext())
			{
				context.Genres.Add(genre);
				context.SaveChanges();
			}
		}

	}
}