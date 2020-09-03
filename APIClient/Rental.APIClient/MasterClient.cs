using Microsoft.AspNetCore.JsonPatch;
using Rental.Schema.Common;
using Rental.Schema.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rental.APIClient
{
    public partial class ApiClient
    {
        /// <summary>
        /// Create Common Master
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Message<FieldMaster>> SaveCommonMasterDetails(FieldMaster model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "internal/masters"));
            return await PostAsync<FieldMaster>(requestUrl, model);
        }
        /// <summary>
        /// Get All Master Records
        /// </summary>
        /// <returns></returns>
        public async Task<Message<List<FieldMaster>>> GetAllGroupRecords()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "internal/masters/all/groups"));
            return await GetAsync<List<FieldMaster>>(requestUrl);
        }

        /// <summary>
        ///Get Records By Name 
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public async Task<Message<FieldMaster>> GetMasterDetailsByName(Dictionary<string,string> collection)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"internal/masters/records"),CreateQueryStringParams(collection));
            return await GetAsync<FieldMaster>(requestUrl);
        }
        /// <summary>
        ///Get Records By Id 
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public async Task<Message<FieldMaster>> GetMasterDetailsById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"internal/masters/{id}"));
            return await GetAsync<FieldMaster>(requestUrl);
        }
        /// <summary>
        /// Update field records by name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchList"></param>
        /// <returns></returns>
        public async Task<Message<FieldMaster>> UpdateFieldMaster(int id, JsonPatchDocument<FieldMaster> patchList)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"internal/masters/records/{id}"));
            return await PatchAsync<FieldMaster, JsonPatchDocument<FieldMaster>>(requestUrl, patchList);
        }
        /// <summary>
        /// Delete field records by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Message<FieldMaster>> DeleteFieldRecords(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"internal/masters/records/{id}"));
            return await DeleteAsync<FieldMaster>(requestUrl);
        }
    }

}
