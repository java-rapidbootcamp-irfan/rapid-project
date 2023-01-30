using Asset_Management.Repository;
using Asset_Management.ViewModel;

namespace Asset_Management.Service
{
    public class RequestAssetService
    {
        private readonly ApplicationDbContext _context;
        public RequestAssetService(ApplicationDbContext context)
        {
            _context = context;
        }

        // method
        public RequestAssetResponse EntityToModelResponse(RequestAssetEntity entity)
        {
            RequestAssetResponse response = new RequestAssetResponse();
            response.Id = entity.Id;
            response.PicId = entity.PicId;

            var pic = _context.AccountEntities.Find(entity.PicId);
            response.FullName = pic.FullName;

            response.Specification = entity.Specification;
            response.RequestDate = entity.RequestDate;

            return response;
        }
        public RequestAssetDetailResponse EntityToModelDetail(RequestAssetEntity entity)
        {
            RequestAssetDetailResponse response = new RequestAssetDetailResponse();
            response.Id = entity.Id;
            response.PicId = entity.PicId;

            var pic = _context.AccountEntities.Find(entity.PicId);
            response.FullName = pic.FullName;

            response.Specification = entity.Specification;
            response.RequestDate = entity.RequestDate;

            var approvals = _context.ApprovalEntities.Where(x => x.RequestAssetId == entity.Id);

            foreach (ApprovalEntity approvalEntity in approvals)
            {
                response.approval.Add(ApprovalEntityToResponse(approvalEntity));
            }
            

            return response;
        }

        public RequestListResponse EntityToListResponse(RequestAssetEntity entity)
        {
            var list = new RequestListResponse();
            list.Id = entity.Id;
            list.PicId = entity.PicId;

            var pic = _context.AccountEntities.Find(entity.PicId);
            list.FullName = pic.FullName;

            list.Specification = entity.Specification;
            list.RequestDate = entity.RequestDate;

            var approvalList = _context.ApprovalEntities.Where(x => x.RequestAssetId == entity.Id).ToList();
            var approval = approvalList.Last();

            list.Status = approval.Status;

            return list;
        }

        public ApprovalResponse ApprovalEntityToResponse(ApprovalEntity entity)
        {
            ApprovalResponse response = new ApprovalResponse();
            response.Id = entity.Id;
            response.Status = entity.Status;
            response.Reason = entity.Reason;
            response.UpdatedDate = entity.Date;

            return response;
        }

        public RequestAssetReq EntityToModelReq(RequestAssetEntity entity)
        {
            RequestAssetReq req = new RequestAssetReq();
            req.Id = entity.Id;
            req.PicId = entity.PicId;
            req.Specification = entity.Specification;
            req.RequestDate = entity.RequestDate;
            return req;
        }

        public void ApprovalReqToEntity(ApprovalReq req, ApprovalEntity entity)
        {
            entity.Reason = req.Reason;
            entity.Date = DateTime.Now;
        }

        public ApprovalReq ApprovalEntityToReq(ApprovalEntity entity)
        {
            ApprovalReq req = new ApprovalReq();
            req.Id = entity.Id;
            req.RequestAssetId = entity.RequestAssetId;
            req.Status = entity.Status;
            req.Reason = entity.Reason;
            req.UpdatedDate = entity.Date;

            return req;
        }

        // CRUD + APPROVE/REJECT
        public List<RequestAssetResponse> GetRequests()
        {
            var result = new List<RequestAssetResponse>();
            foreach (RequestAssetEntity requestAssetEntity in _context.RequestAssetEntities.ToList())
            {
                result.Add(EntityToModelResponse(requestAssetEntity));
            }
            return result;
        }

        public List<RequestAssetResponse> GetRequestAssets()
        {
            var result = new List<RequestAssetResponse>();
            foreach (RequestAssetEntity requestAssetEntity in _context.RequestAssetEntities.ToList())
            {
                result.Add(EntityToModelResponse(requestAssetEntity));
            }
            return result;
        }

        public List<RequestListResponse> GetRequestList()
        {
            var result = new List<RequestListResponse>();
            foreach (RequestAssetEntity requestAssetEntity in _context.RequestAssetEntities.ToList())
            {
                result.Add(EntityToListResponse(requestAssetEntity));
            }
            return result;
        }

        public void CreateRequestAsset(RequestAssetReq req)
        {
            RequestAssetEntity request = new RequestAssetEntity();
            request.Id = req.Id;
            request.PicId = req.PicId;
            request.Specification = req.Specification;
            request.RequestDate = DateTime.Now;

            // read pic from database
            var pic = _context.AccountEntities.Find(req.PicId);
            request.Pic = pic;
            
            //save request asset
            _context.RequestAssetEntities.Add(request);
            _context.SaveChanges();

            // create approval
            ApprovalEntity approval = new ApprovalEntity();
            approval.RequestAsset = request;
            approval.RequestAssetId = request.Id;
            approval.Reason = "";
            approval.Status = "In Process";
            approval.Date = DateTime.Now;

            // save approval to database
            _context.ApprovalEntities.Add(approval);
            _context.SaveChanges();

        }

        public RequestAssetDetailResponse ReadRequestAsset(long? id)
        {
            var request = _context.RequestAssetEntities.Find(id);
            return EntityToModelDetail(request);

        }

        public void ApproveRequest(long? id)
        {
            RequestAssetEntity entity = _context.RequestAssetEntities.Find(id);
            ApprovalEntity approval = new ApprovalEntity();
            approval.RequestAsset = entity;
            approval.RequestAssetId = entity.Id;
            approval.Reason = "";
            approval.Status = "Approved";
            approval.Date = DateTime.Now;
            _context.ApprovalEntities.Add(approval);
            _context.SaveChanges();
        }

        public void RejectRequest(long? id)
        {
            var request = _context.RequestAssetEntities.Find(id);
            ApprovalEntity approval = new ApprovalEntity();
            approval.RequestAsset = request;
            approval.RequestAssetId = request.Id;
            approval.Reason = "";
            approval.Status = "Reject";
            approval.Date = DateTime.Now;
            
            _context.ApprovalEntities.Add(approval);
            _context.SaveChanges();

            //return ApprovalEntityToReq(approval);
        }

        

        /*public void UpdateApproval(ApprovalReq req)
        {
            var entity = _context.ApprovalEntities.Find(req.Id);
            ApprovalReqToEntity(req, entity);
            _context.ApprovalEntities.Update(entity);
            _context.SaveChanges();
        }*/
    }
}
