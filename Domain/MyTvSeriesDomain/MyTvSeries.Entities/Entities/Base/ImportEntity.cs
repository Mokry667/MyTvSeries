namespace MyTvSeries.Domain.Entities.Base
{
    public class ImportEntity : AuditEntity
    {
        public bool IsImportEnabled { get; set; }

        public void EnableImport() => IsImportEnabled = true;
    }
}
