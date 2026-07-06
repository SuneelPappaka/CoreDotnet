using CoreDotnet.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CoreDotnet.Controllers
{
    public class TransientGuidController : Controller
    {
        /// <summary>
        ///ITransientGuidService  makes sure that a new instance of the service is created every time it is requested.
        ///ITransientGuidService, సేవ (service) కోసం అభ్యర్థన చేసిన ప్రతిసారీ దాని యొక్క కొత్త ఇన్‌స్టన్స్ (instance) సృష్టించబడేలా చూస్తుంది.
        /// </summary>

        private readonly ITransientGuidService _transientGuidService1;
        private readonly ITransientGuidService _transientGuidService2;

        /// <summary>
        /// it  will check if the service is already created, if not it will create a new instance and return the same instance for all requests.
        ///ఇది సేవ (service) ఇప్పటికే సృష్టించబడిందో లేదో తనిఖీ చేస్తుంది; ఒకవేళ లేకపోతే, ఇది ఒక కొత్త ఇన్‌స్టాన్స్‌ను సృష్టించి, అన్ని అభ్యర్థనలకూ అదే ఇన్‌స్టాన్స్‌ను అందిస్తుంది.
        /// </summary>

        private readonly IScopedGuidService _scopedGuidService1;
        private readonly IScopedGuidService _scopedGuidService2;

        /// <summary>
        /// ISingletonGuidService makes sure that a single instance of the service is created and shared across all requests and users for the lifetime of the application.
        /// ISingletonGuidService అనేది సర్వీస్ యొక్క ఒకే ఒక ఇన్స్టాన్స్ సృష్టించబడి, అప్లికేషన్ యొక్క జీవితకాలం పాటు అన్ని అభ్యర్థనలు మరియు వినియోగదారుల మధ్య పంచుకోబడుతుందని నిర్ధారిస్తుంది.
        /// </summary>

        private readonly ISingletonGuidService _singletonGuidService1;
        private readonly ISingletonGuidService _singletonGuidService2;

        public TransientGuidController(ITransientGuidService transientGuidService1,
            ITransientGuidService transientGuidService2,
            IScopedGuidService scopedGuidService1,
            IScopedGuidService scopedGuidService2,
            ISingletonGuidService singletonGuidService1,
            ISingletonGuidService singletonGuidService2)
        {
            _transientGuidService1 = transientGuidService1;
            _transientGuidService2 = transientGuidService2;
            _scopedGuidService1 = scopedGuidService1;
            _scopedGuidService2 = scopedGuidService2;
            _singletonGuidService1 = singletonGuidService1;
            _singletonGuidService2 = singletonGuidService2;
        }
        public IActionResult Index()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Transient Service 1: {_transientGuidService1.GetGuid()}");
            stringBuilder.AppendLine($"Transient Service 2: {_transientGuidService2.GetGuid()}");
            stringBuilder.AppendLine($"Scoped Service 1: {_scopedGuidService1.GetGuid()}");
            stringBuilder.AppendLine($"Scoped Service 2: {_scopedGuidService2.GetGuid()}");
            stringBuilder.AppendLine($"Singleton Service 1: {_singletonGuidService1.GetGuid()}");
            stringBuilder.AppendLine($"Singleton Service 2: {_singletonGuidService2.GetGuid()}");
            return Ok(stringBuilder.ToString());

////
//Transient Service 1: 5b99e1b3 - c3e8 - 4ef4 - 8e2e - 6889eaa8fa5e
//Transient Service 2: 6594ce87 - 0864 - 49d7 - a9dd - 53202179e613
//Scoped    Service 1: e46b12d5 - 8acf - 4a30 - 87e2 - a75c8d4058a4
//Scoped    Service 2: e46b12d5 - 8acf - 4a30 - 87e2 - a75c8d4058a4
//Singleton Service 1: 0fecd114 - 3735 - 4450 - 8445 - 3f4995a6b001
//Singleton Service 2: 0fecd114 - 3735 - 4450 - 8445 - 3f4995a6b001

        }
    }
}
