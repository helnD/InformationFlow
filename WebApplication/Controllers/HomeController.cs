using System.Collections.Generic;
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
            
            return View();
        }

        public IActionResult Calculation()
        {
            var graph = new OrientedGraph(new RelationsAdapter(Request.Query).Adapt());

            var adjMatrix = graph.ToMatrix();

            var graphInfo = new GraphInfo(graph);
            var taktsOfCreation = graphInfo.TactsOfCreation();
            var taktsOfExtinction = graphInfo.TactsOfExtinction();
            var taktsOfStore = graphInfo.TactsOfStore();
            var inputs = graphInfo.Inputs;
            var outputs = graphInfo.Outputs;
            var power = graphInfo.Power;

            var powers = graphInfo.MatricesToZero;
            var bMatrix = new BMatrix(adjMatrix);
            
            var matrixAdapter = new MatrixAdapter();
            var graphInfoAdapter = new GraphInfoAdapter();

            return Json(new
            {
                aMatrices = matrixAdapter.AdaptAdjacencyMatrixWithPowers(powers),
                graphInfo = graphInfoAdapter.AdaptGraphInfo(power, inputs, outputs),
                tactsTable = graphInfoAdapter.AdaptTacts(taktsOfCreation, taktsOfExtinction, taktsOfStore),
                bMatrix = matrixAdapter.AdaptBMatrix(bMatrix)
            });
        }

    }
}