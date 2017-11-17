﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicManager.Models;
using MusicManager.Services;
using System.Data.SqlClient;

namespace ImportGenres
{
	class Program
	{
		public static void Main(string[] args)
		{
			var text = System.IO.File.ReadAllLines(@"C:\Users\user\Desktop\Genre.txt");

			foreach (var genreName in text)
			{
				try
				{

					string connetionString = null;
					string sql = null;
					connetionString = "Data Source=.\\sql2008r2;Initial Catalog=MusicManager;Integrated Security=True;User ID=sa;Password=marnus;";
					using (SqlConnection cnn = new SqlConnection(connetionString))
					{
						sql = "insert into Genres ([Name]) values(@genreName)";
						cnn.Open();
						using (SqlCommand cmd = new SqlCommand(sql, cnn))
						{
							cmd.Parameters.AddWithValue("@genreName", genreName);
							cmd.ExecuteNonQuery();
						}
					}
				}
				catch (Exception ex)
				{

				}
			}

			System.Console.ReadKey();
		}
	}
}