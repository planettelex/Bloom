using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    public interface ISuiteStateRepository
    {
        bool SuiteStateExists();

        SuiteState GetSuiteState();

        void AddSuiteState(SuiteState suiteState);
    }
}
