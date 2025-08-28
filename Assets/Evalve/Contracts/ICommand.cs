using System.Threading.Tasks;

namespace Evalve.Contracts
{
    public interface ICommand
    {
        Task Execute();
    }
}