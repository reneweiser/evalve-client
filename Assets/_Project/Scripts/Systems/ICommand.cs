using System.Threading.Tasks;

namespace Evalve.Systems
{
    public interface ICommand
    {
        public Task Execute();
        public Task Reverse();
    }
}