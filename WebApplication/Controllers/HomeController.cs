using Domain.Entities;
using Domain.UseCase;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Adapters;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {

            var graph = new OrientedGraph(new RelationsAdapter(Request.Query).Adapt());

            var adjMatrix = graph.ToMatrix();
            var adjArrayMatrix = adjMatrix.ToArray();

            var graphInfo = new GraphInfo(graph);
            var taktsOfCreation = graphInfo.TactsOfCreation();
            var taktsOfExtinction = graphInfo.TactsOfExtinction();
            var taktsOfStore = graphInfo.TactsOfStore();
            var inputs = graphInfo.Inputs;
            var outputs = graphInfo.Outputs;
            
            var bMatrix = new BMatrix(adjMatrix).ToArray();
            
            return View();
        }

    }
}