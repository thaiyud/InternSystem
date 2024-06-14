using InternSystem.Domain.Entities.BaseEntities;

namespace InternSystem.Domain.Entities
{
    public class Dashboard
    {
        public int Id { get; set; }
        public int ReceivedCV { get; set; }
        public int Interviewed { get; set; }
        public int Passed { get; set; }
        public int Interning { get; set; }
        public int Interned { get; set; }
    }
}
