using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    public interface ISuiteStateRepository
    {
        SuiteState GetSuiteState();

        void AddSuiteState(SuiteState suiteState);
    }
}
