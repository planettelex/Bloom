using System.Collections.Generic;
using Bloom.Domain.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Services
{
    public interface IFiltersetService
    {
        void AddFilterToExpression(Filterset filterset, IFilter filter, string filterAgainst);

        void FlipExpressionOperatorAt(Filterset filterset, int elementNumber);

        void MoveExpressionElement(Filterset filterset, int fromElementNumber, int toElementNumber);

        void AddParenthesis(Filterset filterset);

        List<Album> ApplyFilterset(List<Album> albums, Filterset filterset);

        List<Song> ApplyFilterset(List<Song> songs, Filterset filterset);
    }
}
