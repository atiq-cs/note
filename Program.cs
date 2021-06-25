using System.Threading.Tasks;
using Statiq.App;
using Statiq.Common;
using Statiq.Web;

namespace iQubitNote
{
    public class Program
    {
        public static async Task<int> Main(string[] args) =>
            await Bootstrapper
                .Factory
                .CreateWeb(args)
                .DeployToGitHubPagesBranch("atiq-cs", "note", Config.FromSetting<string>("GITHUB_TOKEN"), "dev")
                .RunAsync();
    }
}
