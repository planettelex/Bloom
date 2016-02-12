using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public interface ILabelRepository
    {
        Label GetLabel(IDataSource dataSource, Guid labelId);

        List<Label> ListLabels(IDataSource dataSource);

        void AddLabel(IDataSource dataSource, Label label);

        void AddLabelPersonnel(IDataSource dataSource, LabelPersonnel labelPersonnel);

        void DeleteLabel(IDataSource dataSource, Label label);
    }
}
