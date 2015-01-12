using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    public class TimeSignature
    {
        public int Beats { get; set; }

        public NoteLength NoteLength { get; set; }

        public override string ToString()
        {
            return Beats + "/" + (int)NoteLength;
        }
    }
}
