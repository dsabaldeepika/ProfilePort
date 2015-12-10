
//using DealerPortalCRM.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DealerPortalCRM.Constants;


//namespace DealerPortalCRM.DAService
//{
//    public interface IApplication
//    {
//        Task<List<Application>> GetAllApplicationsAsync();

//        SalesDashboardViewModel getSalesDashBoard();


//        List<SalesDashboardDetailViewModel> getSalesDashBoardDetail();

//        // fill up with groups
//        List<SalesDashboardViewModel> getSalesDashBoard_Test(SalesDashboardGroupByEnum SalesDashBoardGroupBy, int? SalesRepID, int? DealerID);

//        // fill up with applications
//        List<SalesDashboardDetailViewModel> getSalesDashBoardDetail_Test(SalesDashboardGroupByEnum SalesDashBoardGroupBy, SalesDashboardStatusEnum SalesDashBoardStatus, int? SalesRepID, int? DealerID);


//        List<SalesDashboardViewModel> getSalesDashBoardBySalesRep();
//        List<SalesDashboardViewModel> getSalesDashBoardBySalesRep(int SalesRepID);
//        List<SalesDashboardViewModel> getSalesDashBoardBySalesRep(int SalesRepID, int ApplicationStatusID);

//        List<SalesDashboardViewModel> getSalesDashBoardByDealer();
//        List<SalesDashboardViewModel> getSalesDashBoardByDealer(int DealerID);
//        List<SalesDashboardViewModel> getSalesDashBoardByDealer(int DealerID, int ApplicationStatusID);

//        List<SalesDashboardViewModel> getSalesDashBoardByAll();

//        List<SalesDashboardViewModel> getSalesDashboardByDealerFilterBySalesRep(int SalesRepID);
//        List<SalesDashboardViewModel> getSalesDashboardByDealerFilterBySalesRep(int SalesRepID, int DealerID);


//        //


//        List<SalesDashboardDetailViewModel> getApplicationsBySalesRep(int SalesRepID);
//        List<SalesDashboardDetailViewModel> getApplicationsBySalesRep(int SalesRepID, int ApplicationStatusID);

      
//        List<SalesDashboardDetailViewModel> getApplicationsByDealer(int DealerID);
//        List<SalesDashboardDetailViewModel> getApplicationsByDealer(int DealerID, int ApplicationStatusID);

//        List<SalesDashboardDetailViewModel> getAllApplicationsByStatus(int ApplicationStatusID);

//        List<SalesDashboardDetailViewModel> getApplicationsByDealerFilterBySalesRep(int SalesRepID, int DealerID, int ApplicationStatus);







//    }

//}
