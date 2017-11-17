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

				var genreList = context.Genres.ToList();
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
				return context.MusicLibraries.Include("Genre").ToList();

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
				return context.MusicLibraries.First(o => o.Id == id);
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

		public void DeleteMusic(MusicLibrary musicLibrary)
		{
			using (var context = new BaseContext())
			{
				context.MusicLibraries.Remove(musicLibrary);
				context.SaveChanges();
			}
		}

	}
}