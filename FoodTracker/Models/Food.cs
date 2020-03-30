﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTracker.Models
{
	public class Food
	{
		public int ID { get; set; }

		[Required]
		[MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
		public string Name { get; set; }
		public string Description { get; set; }
		
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{yyyy/MM/dd}")]
		[Display(Name = "Best Before")]
		public DateTime? BestBefore { get; set; }		

		[Required]
		[Display(Name = "Category")]
		public int CategoryId { get; set; }

		[ForeignKey("CategoryId")]
		public virtual Category Category { get; set; }

		[Required]
		[Display(Name = "SubCategory")]
		public int SubCategoryId { get; set; }

		[ForeignKey("SubCategoryId")]
		public virtual SubCategory SubCategory { get; set; }

		[Required]
		[Display(Name = "owner_name")]
		public string OwnerName { get; set; }

		[ForeignKey("OwnerName")]
		public virtual ApplicationUser ApplicationUser { get; set; }

		public bool IsInCart { get; set; }

		public QuantityLeft QuantityLeft { get; set; }
		public int Unit { get; set; }
		public MeasType Measurement { get; set; }
		public Priority Priority { get; set; }
	}
	public enum QuantityLeft { Semennyi = 0, Keves = 1, Adagnyi = 2, Sok = 3 }
	public enum Priority { Magas = 0, Közepes =1, Alacsony =2}
	public enum MeasType
	{
		kg,
		dkg,
		g,
		l,
		dl,
		ml,
		db,
		adag
	}

	public class BestBeforeValidAttirbute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			DateTime d = Convert.ToDateTime(value);
			return d >= DateTime.Now;
		}
	}
}

