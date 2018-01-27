﻿using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class GameModel
	{
		int turn = 1;

		List<ShowConcept> allConcepts;
		List<ShowConcept> availConcepts;
		Dictionary<ShowConcept, int> developingConcepts = new Dictionary<ShowConcept, int>();

		List<Ad> allAds;
		List<Ad> availAds;

		List<Demographic> population;


		Demographic kidsDemographic = new Demographic ("Kids", 10000,
			new Dictionary<Timeslot, double>() {
				{ Timeslot.Morning, 0.3 },
				{ Timeslot.Afternoon, 0.5 },
				{ Timeslot.PrimeTime, 0.2 },
				{ Timeslot.Night, 0.1 }
			}
		);

		Demographic grownupsDemographic = new Demographic("Grownups", 20000, 
			new Dictionary<Timeslot, double>() {
				{Timeslot.Morning, 0.3},
				{Timeslot.Afternoon, 0.2},
				{Timeslot.PrimeTime, 0.6},
				{Timeslot.Night, 0.4}
			}
		);



		List<Show> availShows;

		Dictionary<Timeslot, Show> showProgram;

		Dictionary<Timeslot, Ad> adProgram;

		public GameModel ()
		{
			allConcepts = initConcepts();
			availConcepts = initAvailConcepts();
			developingConcepts = new Dictionary<ShowConcept, int>();

			allAds = initAds();
			availAds = initAvailAds();

			population = initPopulation();


			availShows = initAvailShows();

			showProgram = initProgram();

			adProgram = initAdProgram();
		}


		List<Demographic> initPopulation()
		{
			return new List<Demographic> { 
				kidsDemographic,
				grownupsDemographic,
			};
		}

		List<ShowConcept> initConcepts()
		{
			return new List<ShowConcept> { 
				new ShowConcept ("BlackMirror", "Scary neo-noir scifi", 100000, 5, 
					new Dictionary<Demographic, int> {
						{ kidsDemographic, 0 },
						{ grownupsDemographic, 10000 }
					}
				)
			};
		}


		List<ShowConcept> initAvailConcepts()
		{
			return null;
		}


		Dictionary<Timeslot, Show> initProgram()
		{
			return null;
		}

		List<Show> initAvailShows()
		{
			return null;
		}


		Dictionary<Timeslot, Ad> initAdProgram()
		{
			return null;
		}


		List<Ad> initAds()
		{
			return null;
		}

		List<Ad> initAvailAds()
		{
			return null;
		}



	}


	public enum Timeslot
	{
		Morning,
		Afternoon,
		PrimeTime,
		Night
	};

	public class Demographic 
	{
		public string name;
		public int size; 
		public Dictionary<Timeslot, double> timeslotPrefs;

		public Demographic(string name, int size, Dictionary<Timeslot, double> timeslotPrefs)
		{
			this.name = name;
			this.size = size;
			this.timeslotPrefs = timeslotPrefs;
		}

	}

	public class ShowConcept
	{
		public string name;
		public string flavor;
		public Dictionary<Demographic, int> demographicAppeal;
		public int price;
		public int duration;


		public ShowConcept(string name, String flavor, int price, int duration, Dictionary<Demographic, int> demographicAppeal)
		{
			this.name = name;
			this.flavor = flavor;
			this.demographicAppeal = demographicAppeal;
			this.price = price;
			this.duration = duration;
		}

	}

	public class Show {

		public ShowConcept concept;
		public float peak;
		public float longevity;

		public Show(ShowConcept concept, float peak, float longevity)
		{
			this.concept = concept;
			this.peak = peak;
			this.longevity = longevity;
		}

	}

	public class DemographicTarget 
	{
		public Demographic target;
		public int revenue;

		public DemographicTarget(Demographic target, int revenue) 	
		{
			this.target = target;
			this.revenue = revenue;
		}
	}

	public class Ad
	{
		string name;
		string flavor;
		public DemographicTarget primary;
		public int revenueOther;

		public Ad(DemographicTarget primary, int revenueOther)
		{
			this.primary = primary;
			this.revenueOther = revenueOther;
		}
	}


}

