﻿namespace CitishopNET.Shared
{
	public class PagedModel<TModel>
	{
		public int CurrentPage { get; set; }
		public int TotalItems { get; set; }
		public int TotalPages { get; set; }
		public IEnumerable<TModel> Items { get; set; } = null!;
	}
}