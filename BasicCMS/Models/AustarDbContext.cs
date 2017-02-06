using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BasicCMS.Models {
	public class AustarDbContext : DbContext {
		//public DbSet<University> Universities { get; set; }
		//public DbSet<Classification> Classifications { get; set; }
		public DbSet<MultiChoice> MultiChoices { get; set; }
		public DbSet<StudentRegister> StudentRegisters { get; set; }
		public DbSet<StudentLanguage> StudentLanguages { get; set; }
		public AustarDbContext() : this("DefaultConnection") { }
		public AustarDbContext(string connString) : base(connString) {
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<AustarDbContext, Migrations.Configuration>("DefaultConnection"));
		}
		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
			modelBuilder.HasDefaultSchema("austar");
		}
	}
	//public class University {
	//	public virtual Guid Id { get; set; }
	//	public virtual string Name { get; set; }
	//	public virtual string ShortName { get; set; }
	//	public virtual string StreetAddress { get; set; }
	//	public virtual string PostCode { get; set; }
	//	public virtual string WebsiteUrl { get; set; }
	//}
	public class HostRegister {
		public virtual Guid Id { get; set; }
		[Column(TypeName = "datetime2")]
		public virtual DateTime CreatedDate { get; set; }
		public virtual Guid UserId { get; set; }
		public virtual Guid? GenderId { get; set; }
		//[ForeignKey("GenderId")]
		//public virtual Classification Gender { get; set; }
		public virtual DateTime? BirthDate { get; set; }
		public virtual string Occupation { get; set; }
		public virtual string HomePhone { get; set; }
		public virtual string MobilePhone { get; set; }
		public virtual string HomeAddress { get; set; }
		public virtual string HomeUnitNbr { get; set; }
		public virtual string HomeStreetNbr { get; set; }
		public virtual string HomeStreetName { get; set; }
		public virtual string HomeCity { get; set; }
		public virtual string HomeState { get; set; }
		public virtual string HomePostCode { get; set; }
		public virtual bool HomeAsPostal { get; set; }
		public virtual string PostalAddress { get; set; }
		public virtual string PostalUnitNbr { get; set; }
		public virtual string PostalStreetNbr { get; set; }
		public virtual string PostalStreetName { get; set; }
		public virtual string PostalCity { get; set; }
		public virtual string PostalState { get; set; }
		public virtual string PostalPostCode { get; set; }
		public virtual Guid? DwellingTypeId { get; set; }
		//[ForeignKey("DwellingTypeId")]
		//public virtual Classification DwellingType { get; set; }
		public virtual Guid? FlooringMainTypeId { get; set; }
		//[ForeignKey("FlooringMainTypeId")]
		//public virtual Classification FlooringMainType { get; set; }
		public virtual Guid? InternetAvailableId { get; set; }
		//[ForeignKey("InternetAvailableId")]
		//public virtual Classification InternetAvailable { get; set; }
		public virtual Guid? SmokeDetectorId { get; set; }
		//[ForeignKey("SmokeDetectorId")]
		//public virtual Classification SmokeDetector { get; set; }
		public virtual ICollection<MultiChoice> MultiChoosens { get; set; }
		public virtual Guid? NearestBusStopId { get; set; }
		//[ForeignKey("NearestBusStopId")]
		//public virtual Classification NearestBusStop { get; set; }
		public virtual Guid? NearestTrainStopId { get; set; }
		//[ForeignKey("NearestTrainStopId")]
		//public virtual Classification NearestTrainStop { get; set; }
		public virtual Guid? NearestFerryStopId { get; set; }
		//[ForeignKey("NearestFerryStopId")]
		//public virtual Classification NearestFerryStop { get; set; }
		public virtual Guid? BedroomsId { get; set; }
		//[ForeignKey("BedroomsId")]
		//public virtual Classification Bedrooms { get; set; }
		public virtual Guid? AvailableBedroomsId { get; set; }
		//[ForeignKey("AvailableBedroomsId")]
		//public virtual Classification AvailableBedrooms { get; set; }
		public virtual Guid? BathroomsId { get; set; }
		//[ForeignKey("BathroomsId")]
		//public virtual Classification Bathrooms { get; set; }
		public virtual Guid? LaundryId { get; set; }
		//[ForeignKey("LaundryId")]
		//public virtual Classification Laundry { get; set; }
	}
	public class StudentRegister {
		public virtual Guid Id { get; set; }
		[Column(TypeName = "datetime2")]
		public virtual DateTime CreatedDate { get; set; }
		public virtual Guid UserId { get; set; }
		public virtual Guid? GenderId { get; set; }
		//[ForeignKey("GenderId")]
		//public virtual Classification Gender { get; set; }
		public virtual DateTime? BirthDate { get; set; }
		public virtual int TimeZoneId { get; set; }
		public virtual bool? HasBeenHomestayStudent { get; set; }
		public virtual string Experience { get; set; }
		public virtual string Interests { get; set; }
		public virtual string InfoToShare { get; set; }
		public virtual string FavouriteFood { get; set; }
		public virtual Guid? EthnicityId { get; set; }
		//[ForeignKey("EthnicityId")]
		//public virtual Classification Ethnicity { get; set; }
		public virtual Guid? ReligionId { get; set; }
		//[ForeignKey("ReligionId")]
		//public virtual Classification Religion { get; set; }
		public virtual ICollection<StudentLanguage> StudentLanguages { get; set; }
		public virtual bool? LiveWithPets { get; set; }
		public virtual bool? TravelInsurance { get; set; }
		public virtual string InsuranceProvider { get; set; }
		public virtual string InsuranceNbr { get; set; }
		public virtual DateTime? InsuranceExpiredDate { get; set; }
		public virtual byte[] InsurancePolicyCopy { get; set; }
		public virtual bool? GuardianshipRequested { get; set; }
		public virtual Guid? GuardianshipId { get; set; }
		//[ForeignKey("GuardianshipId")]
		//public virtual Classification Guardianship { get; set; }
		public virtual string OtherGuardianship { get; set; }
		public virtual bool? AirportPickup { get; set; }
		public virtual DateTime? ArrivalDate { get; set; }
		public virtual string Airline { get; set; }
		public virtual string FlightNbr { get; set; }
		public virtual bool? LiveWithChildrenUnder10 { get; set; }
		public virtual bool? LiveWithChildrenUnder17 { get; set; }
		public virtual string ReasonForPreference { get; set; }
		public virtual string OtherForPreference { get; set; }
		public virtual Guid? LiveWithSmokerId { get; set; }
		//[ForeignKey("LiveWithSmokerId")]
		//public virtual Classification LiveWithSmoker { get; set; }
		public virtual bool? HasDietary { get; set; }
		public virtual bool? HasAllergy { get; set; }
		public virtual bool? HasMedication { get; set; }
		public virtual string MedicationDetails { get; set; }
		public virtual bool? Disability { get; set; }
		public virtual string DisabilityDetails { get; set; }
		public virtual Guid? SmokeId { get; set; }
		//[ForeignKey("SmokeId")]
		//public virtual Classification Smoke { get; set; }
		[InverseProperty("Student")]
		public virtual ICollection<MultiChoice> MultiChoosens { get; set; }
		public virtual Guid? HearFromId { get; set; }
		//[ForeignKey("HearFromId")]
		//public virtual Classification HearFrom { get; set; }
		public virtual string OtherReferral { get; set; }
		public virtual Guid? WhyChoosingId { get; set; }
		//[ForeignKey("WhyChoosingId")]
		//public virtual Classification WhyChoosing { get; set; }
		public virtual string OtherChoosingReason { get; set; }
		public virtual Guid? UniversityId { get; set; }
		public virtual string UniversityAddress { get; set; }
	}
	public class MultiChoice {
		public virtual Guid Id { get; set; }
		public virtual Guid StudentId { get; set; }
		[ForeignKey("StudentId")]
		public virtual StudentRegister Student { get; set; }
		public virtual string SchemeCode { get; set; }
		public virtual Guid OptionId { get; set; }
		//[ForeignKey("OptionId")]
		//public virtual Classification Option { get; set; }
		public virtual bool Selected { get; set; }
		public virtual string MoreInfo { get; set; }
	}
	public class StudentLanguage {
		public virtual Guid Id { get; set; }
		public virtual Guid StudentId { get; set; }
		[ForeignKey("StudentId")]
		public virtual StudentRegister Student { get; set; }
		public virtual Guid LanguageId { get; set; }
		//[ForeignKey("LanguageId")]
		//public virtual Classification Language { get; set; }
		public virtual Guid ProficiencyId { get; set; }
		//[ForeignKey("ProficiencyId")]
		//public virtual Classification Proficiency { get; set; }
	}
	//public class Classification {
	//	public virtual Guid Id { get; set; }
	//	public virtual string Name { get; set; }
	//	public virtual string Description { get; set; }
	//	public virtual string SchemeCode { get; set; }
	//	public virtual string MoreInfoDescription { get; set; }
	//	public virtual bool NeedMoreInfo { get; set; }
	//}
}