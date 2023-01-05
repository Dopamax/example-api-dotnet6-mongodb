using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using SchoolERP.Models.FeesPayment;
using SchoolERP.Services.FeesPayment;

namespace SchoolERP.Controllers.FeesPayment
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeesPaymentController : ControllerBase
    {
        private readonly FeesPaymentService _feesPaymentService;

        public FeesPaymentController(FeesPaymentService feesPaymentService)
        {
            _feesPaymentService = feesPaymentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(FeesPaymentModel feesPaymentModel)
        {
            await _feesPaymentService.CreateAsync(feesPaymentModel);

            return CreatedAtAction(nameof(Get), new { _id = feesPaymentModel._id }, feesPaymentModel);
        }

        [HttpGet]
        public async Task<List<FeesPaymentModel>> Get() =>
        await _feesPaymentService.GetAsync();

        [HttpGet("getInstallmentOfOneFeePayment/{id}/{installmentId}/{itemMasterId}")]
        public async Task<bool> checkIfInstallmentDetailIsPaid([FromRoute] string id, [FromRoute] string installmentId, [FromRoute] string itemMasterId)
        {
            return await _feesPaymentService.checkIfInstallmentDetailIsPaid(id,installmentId,itemMasterId);
        }
    }
}
