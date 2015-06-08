using System;
using SQLite;
namespace Tasky
{
	public class Task
	{
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int Id { get; set; }

		public string Name { get; set; }

		public string Notes { get; set; }

		public bool Done { get; set; }

		public DateTime DateCreated { get; set; }

	}
}

