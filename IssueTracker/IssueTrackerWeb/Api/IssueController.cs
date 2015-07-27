using IssueTrackerDomain.Services;
using IssueTrackerModels;
using IssueTrackerWeb.Infrastructure;
using System.Collections.Generic;
using System.Web.Http;

namespace IssueTrackerWeb.Api
{
    public class IssueController : BaseApiController
    {
        [AuthApi]
        public IEnumerable<IssueSummaryModel> Summary(IssueSearchCriteriaModel criteria)
        {
            return Service<IssueService>().GetIssueSummaries(criteria);
        }

        [AuthApi]
        public IssueSummaryModel Summary(int id)
        {
            return Service<IssueService>().GetIssueSummary(id);
        }

        [AuthApi]
        public IEnumerable<IssueDetailsModel> Get(IssueSearchCriteriaModel criteria = null,
            bool includeSummary = false, bool includeUser = false, bool includeIssue = false)
        {
            return Service<IssueService>().GetIssueDetails(
                criteria, includeSummary, includeUser, includeIssue);
        }

        [AuthApi]
        public IssueDetailsModel Get(int id,
            bool includeSummary = false, bool includeUser = false, bool includeIssue = false)
        {
            return Service<IssueService>().GetIssueDetails(
                id, includeSummary, includeUser, includeIssue);
        }

        [AuthApi]
        public int Post([FromBody]IssueDetailsModel issueDetails)
        {
            return Service<IssueService>().CreateIssue(issueDetails, CurrentUserId);
        }

        [AuthApi]
        public void Put([FromBody]IssueDetailsModel issueDetails)
        {
            Service<IssueService>().UpdateIssue(issueDetails);
        }

        [AuthApi]
        public void Delete(int id)
        {
            Service<IssueService>().DeleteIssue(id);
        }
    }
}
