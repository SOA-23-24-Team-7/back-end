namespace Explorer.Stakeholders.API.Dtos
{
    public class ClubResponseWithOwnerDto
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
