namespace Domain.Entities
{
    public class Company
    {
        public int Id { get; private set; }

        public string BusinessName { get; private set; } = null!;
        public string Cuit { get; private set; } = null!;
        public string Industry { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public string Website { get; private set; } = null!;
        public string Location { get; private set; } = null!;

        public bool Approved { get; private set; }

        private Company() { } // EF Core

        public Company(
            string businessName,
            string cuit,
            string industry,
            string description,
            string website,
            string location
        )
        {
            BusinessName = businessName;
            Cuit = cuit;
            Industry = industry;
            Description = description;
            Website = website;
            Location = location;
            Approved = false;
        }

        public void Approve()
        {
            Approved = true;
        }

        public void Reject()
        {
            Approved = false;
        }

        public void Update(
            string? industry,
            string? description,
            string? website,
            string? location
        )
        {
            if (!string.IsNullOrEmpty(industry))
                Industry = industry;

            if (!string.IsNullOrEmpty(description))
                Description = description;

            if (!string.IsNullOrEmpty(website))
                Website = website;

            if (!string.IsNullOrEmpty(location))
                Location = location;
        }
    }
}