using FluentMigrator.VersionTableInfo;

namespace bcms.austar.Models.Migrations {
	[VersionTableMetaData]
	public class AustarVersionTableMetaData : IVersionTableMetaData {
		public string SchemaName {
			get {
				return AustarModuleDescriptor.ModuleSchemaName;
			}
		}

		public string TableName {
			get {
				return "VersionInfo";
			}
		}

		public string ColumnName {
			get {
				return "Version";
			}
		}

		public string UniqueIndexName {
			get {
				return "uc_VersionInfo_Verion_" + AustarModuleDescriptor.ModuleName;
			}
		}
	}
}